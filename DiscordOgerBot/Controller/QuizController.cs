using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using DiscordOgerBot.Globals;
using DiscordOgerBot.Models;
using Serilog;

namespace DiscordOgerBot.Controller
{
    internal static class QuizController
    {

        private static RestUserMessage PrepMessage { get; set; }
        private static object ListLock { get; } = new();

        internal static async Task PrepareQuiz(ISocketMessageChannel channel, IUser quizMaster)
        {
            try
            {
                CurrentQuiz.CurrentQuizChannel = channel;
                lock (ListLock)
                {
                    CurrentQuiz.CurrentQuizUsers = new List<QuizUser>();
                    CurrentQuiz.CurrentQuizMaster = new DiscordUser
                    {
                        Id = quizMaster.Id.ToString(),
                        Name = quizMaster.Username,
                    };
                }

                PrepMessage = await CurrentQuiz.CurrentQuizChannel.SendMessageAsync(
                    $"<@&827310534352175135> **Macht euch bereit ein Quiz beginnt bald!** {Environment.NewLine} Wenn ihr mitspielen wollt reagiert auf diese Nachricht mit <:RainerSchlau:759174717155311627>");

                OgerBot.Client.ReactionAdded += ReactionAddedPrep;
                CurrentQuiz.QuizState = QuizState.PrepPhase;
                await OgerBot.Client.SetGameAsync("wer beim Quiz dabei ist", type: ActivityType.Watching);
            }
            catch (Exception ex)
            {
                Log.Error(ex, nameof(PrepareQuiz));
            }
        }

        internal static async Task StartQuiz()
        {
            try
            {
                OgerBot.Client.ReactionAdded -= ReactionAddedPrep;
                CurrentQuiz.QuizState = QuizState.Running;

                var message = $"**Das Quiz startet!** {Environment.NewLine}{Environment.NewLine} Und dabei isch:" + Environment.NewLine;

                lock (ListLock)
                {
                    foreach (var quizUser in CurrentQuiz.CurrentQuizUsers)
                    {
                        message +=
                            $"<@{quizUser.Id}> Mit {quizUser.QuizWonTotal} gewonnen Quizes und {quizUser.QuizPointsTotal} Quiz-Punkte!{Environment.NewLine}";
                    }
                }

                message += $"{Environment.NewLine}Und dem guden: <@{CurrentQuiz.CurrentQuizMaster.Id}> als Quizmaster!";

                await CurrentQuiz.CurrentQuizChannel.SendMessageAsync(message);

                OgerBot.Client.ReactionAdded += ReactionAddedRunning;
                OgerBot.Client.ReactionRemoved += ReactionRemovedRunning;
                await OgerBot.Client.SetGameAsync("ein Quiz", type: ActivityType.Playing);
            }
            catch (Exception ex)
            {
                Log.Error(ex, nameof(StartQuiz));
            }
        }

        internal static async Task StopQuiz()
        {
            try
            {
                OgerBot.Client.ReactionAdded -= ReactionAddedRunning;
                OgerBot.Client.ReactionAdded -= ReactionRemovedRunning;
                CurrentQuiz.QuizState = QuizState.EndPhase;

                var message = $"**Das Quiz ist beendet** {Environment.NewLine}{Environment.NewLine}Hier der Punktestand!:" + Environment.NewLine;
                const int maxPoints = 7;
                lock (ListLock)
                {
                    var listSort = CurrentQuiz.CurrentQuizUsers.OrderByDescending(m => m.CurrentQuizPoints).ToList();
                    var pointCount = CurrentQuiz.CurrentQuizUsers.Count > maxPoints ? maxPoints : CurrentQuiz.CurrentQuizUsers.Count;

                    var i = 1;
                    foreach (var quizUser in listSort)
                    {
                        var points = (pointCount - i) < 0 ? 0 : (pointCount - i);
                        DataBase.IncreaseQuizPointsTotal(ulong.Parse(quizUser.Id), points);

                        if (i == 1)
                            DataBase.IncreaseQuizWonTotal(ulong.Parse(quizUser.Id));

                        message += $"{i}. <@{quizUser.Id}> mit {quizUser.CurrentQuizPoints} Punkten! Und bekommt somit {points} Quiz-Punkte => {DataBase.GetQuizPointsTotal(ulong.Parse(quizUser.Id))} Punkte {Environment.NewLine}";
                        i++;
                    }

                    CurrentQuiz.CurrentQuizUsers = new List<QuizUser>();
                    CurrentQuiz.CurrentQuizMaster = new DiscordUser();
                }

                await CurrentQuiz.CurrentQuizChannel.SendMessageAsync(message);

                CurrentQuiz.QuizState = QuizState.NotRunning;
                await OgerBot.Client.SetGameAsync("ob Haider vorm Tor stehen", type: ActivityType.Watching);
            }
            catch (Exception ex)
            {
                Log.Error(ex, nameof(StopQuiz));
            }
        }

        internal static async Task GetCurrentPoints()
        {
            try
            {
                var message = "Aktueller Punktestand:" + Environment.NewLine;
                lock (ListLock)
                {
                    var listSort = CurrentQuiz.CurrentQuizUsers.OrderByDescending(m => m.CurrentQuizPoints).ToList();
                    var i = 1;
                    foreach (var quizUser in listSort)
                    {
                        message += $"{i}. <@{quizUser.Id}> mit {quizUser.CurrentQuizPoints} Punkten! {Environment.NewLine}";
                        i++;
                    }
                }
                await CurrentQuiz.CurrentQuizChannel.SendMessageAsync(message);
            }
            catch (Exception ex)
            {
                Log.Error(ex, nameof(GetCurrentPoints));
            }
        }

        private static async Task ReactionAddedPrep(Cacheable<IUserMessage, ulong> message, ISocketMessageChannel channel, SocketReaction reaction)
        {
            try
            {
                if(CurrentQuiz.QuizState != QuizState.PrepPhase) return;
                if (channel.Id != CurrentQuiz.CurrentQuizChannel.Id) return;
                if (message.Id != PrepMessage.Id) return;
                if (reaction.Emote.Name != "RainerSchlau") return;

                var user = await channel.GetUserAsync(reaction.UserId);

                var pointsTotal = DataBase.GetQuizPointsTotal(user.Id);
                var winsTotal = DataBase.GetTimesQuizWonTotal(user.Id);

                lock (ListLock)
                {
                    CurrentQuiz.CurrentQuizUsers.Add(new QuizUser
                    {
                        Id = user.Id.ToString(),
                        Name = user.Username,
                        CurrentQuizPoints = 0,
                        QuizPointsTotal = pointsTotal,
                        QuizWonTotal = winsTotal,
                    });
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, nameof(ReactionAddedPrep));
            }
        }

        private static async Task ReactionAddedRunning(Cacheable<IUserMessage, ulong> message, ISocketMessageChannel channel, SocketReaction reaction)
        {
            try
            {
                if (CurrentQuiz.QuizState != QuizState.Running) return;
                if (channel.Id != CurrentQuiz.CurrentQuizChannel.Id) return;
                if (reaction.Emote.Name != "✅") return;
                if (!(await channel.GetUserAsync(reaction.UserId) is SocketGuildUser reactionUser)) return;
                if (reactionUser.Roles.All(m => m.Id != 826886898114363432)) return;

                var messageDownload= await message.GetOrDownloadAsync();
                var messageUserId = messageDownload.Author.Id;

                lock (ListLock)
                {
                    var userQuiz = CurrentQuiz.CurrentQuizUsers.FirstOrDefault(m => m.Id == messageUserId.ToString());
                    if (userQuiz == null) return;

                    userQuiz.CurrentQuizPoints++;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, nameof(ReactionAddedRunning));
            }
        }

        private static async Task ReactionRemovedRunning(Cacheable<IUserMessage, ulong> message, ISocketMessageChannel channel, SocketReaction reaction)
        {
            try
            {
                if (CurrentQuiz.QuizState != QuizState.Running) return;
                if (channel.Id != CurrentQuiz.CurrentQuizChannel.Id) return;
                if (reaction.Emote.Name != "✅") return;
                if (!(await channel.GetUserAsync(reaction.UserId) is SocketGuildUser reactionUser)) return;
                if (reactionUser.Roles.All(m => m.Id != 826886898114363432)) return;

                var messageDownload = await message.GetOrDownloadAsync();
                var messageUserId = messageDownload.Author.Id;

                lock (ListLock)
                {
                    var userQuiz = CurrentQuiz.CurrentQuizUsers.FirstOrDefault(m => m.Id == messageUserId.ToString());
                    if (userQuiz == null) return;
                    userQuiz.CurrentQuizPoints--;
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex, nameof(ReactionRemovedRunning));
            }
        }
    }
}
