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

                OgerBot.Client.ReactionAdded += ReactionAddedPrep;
                OgerBot.Client.ReactionAdded += ReactionAddedRunning;
                OgerBot.Client.ReactionRemoved += ReactionRemovedRunning;

                PrepMessage = await CurrentQuiz.CurrentQuizChannel.SendMessageAsync(
                    $"<@&827310534352175135> **Macht euch bereit ein Quiz beginnt bald!** {Environment.NewLine} Wenn ihr mitspielen wollt reagiert auf diese Nachricht mit <:RainerSchlau:759174717155311627>");

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
                CurrentQuiz.QuizState = QuizState.Running;

                var message = $"**Das Quiz startet!** {Environment.NewLine}{Environment.NewLine} Und dabei isch:" + Environment.NewLine;

                lock (ListLock)
                {
                    foreach (var quizUser in CurrentQuiz.CurrentQuizUsers)
                    {
                        message +=
                            $"<@{quizUser.Id}> Mit {quizUser.QuizWonTotal} gewonnenen Quiz/es und {quizUser.QuizPointsTotal} Quiz-Punkt/e!{Environment.NewLine}";
                    }
                }

                message += $"{Environment.NewLine}Und dem guden: <@{CurrentQuiz.CurrentQuizMaster.Id}> als Quizmaster!";

                await CurrentQuiz.CurrentQuizChannel.SendMessageAsync(message);


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
                CurrentQuiz.QuizState = QuizState.NotRunning;

                var message = $"**Das Quiz ist beendet** {Environment.NewLine}{Environment.NewLine}Hier der Endstand:" + Environment.NewLine;
                const int maxPoints = 6;
                lock (ListLock)
                {
                    var listSort = CurrentQuiz.CurrentQuizUsers.OrderByDescending(m => m.CurrentQuizPoints).ToList();
                    var maxPointsFlattend = CurrentQuiz.CurrentQuizUsers.Count > maxPoints ? maxPoints : CurrentQuiz.CurrentQuizUsers.Count;

                    var rank = 0;
                    var pointsLastUser = -1;
                    foreach (var quizUser in listSort)
                    {

                        if (quizUser.CurrentQuizPoints == pointsLastUser)
                        {
                            rank--;
                        }

                        var points = (maxPointsFlattend - rank) < 0 ? 0 : (maxPointsFlattend - rank);
                        DataBase.IncreaseQuizPointsTotal(ulong.Parse(quizUser.Id), points);

                        if (rank == 0)
                            DataBase.IncreaseQuizWonTotal(ulong.Parse(quizUser.Id));

                        message += $"{rank + 1}. <@{quizUser.Id}> mit {quizUser.CurrentQuizPoints} Punkt/en! Und bekommt somit {points} Quiz-Punkt/e => {DataBase.GetQuizPointsTotal(ulong.Parse(quizUser.Id))} Punkt/e {Environment.NewLine}";

                        pointsLastUser = quizUser.CurrentQuizPoints;
                        rank++;
                    }

                    CurrentQuiz.CurrentQuizUsers = new List<QuizUser>();
                    CurrentQuiz.CurrentQuizMaster = new DiscordUser();
                }

                await CurrentQuiz.CurrentQuizChannel.SendMessageAsync(message);

                OgerBot.Client.ReactionAdded -= ReactionAddedPrep;
                OgerBot.Client.ReactionAdded -= ReactionAddedRunning;
                OgerBot.Client.ReactionRemoved -= ReactionRemovedRunning;

                await OgerBot.Client.SetGameAsync("ob Haider vorm Tor stehen", type: ActivityType.Watching);
            }
            catch (Exception ex)
            {
                Log.Error(ex, nameof(StopQuiz));
            }
        }

        internal static async Task AbortQuiz()
        {
            try
            {
                lock (ListLock)
                {
                    CurrentQuiz.CurrentQuizUsers = new List<QuizUser>();
                    CurrentQuiz.CurrentQuizMaster = new DiscordUser();
                    CurrentQuiz.QuizState = QuizState.NotRunning;
                }

                OgerBot.Client.ReactionAdded -= ReactionAddedPrep;
                OgerBot.Client.ReactionAdded -= ReactionAddedRunning;
                OgerBot.Client.ReactionRemoved -= ReactionRemovedRunning;

                await OgerBot.Client.SetGameAsync("ob Haider vorm Tor stehen", type: ActivityType.Watching);
            }
            catch (Exception ex)
            {
                Log.Error(ex, nameof(AbortQuiz));
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
                    var rank = 1;
                    var pointsLastUser = -1;
                    foreach (var quizUser in listSort)
                    {
                        if (quizUser.CurrentQuizPoints == pointsLastUser)
                        {
                            rank--;
                        }

                        message += $"{rank}. <@{quizUser.Id}> mit {quizUser.CurrentQuizPoints} Punkt/en! {Environment.NewLine}";

                        pointsLastUser = quizUser.CurrentQuizPoints;
                        rank++;
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
