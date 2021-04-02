using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private static ISocketMessageChannel QuizChannel { get; set; }
        private static object ListLock { get; set; } = new();

        internal static async Task PrepareQuiz(ISocketMessageChannel channel, IUser quizMaster)
        {
            try
            {
                QuizChannel = channel;
                lock (ListLock)
                {
                    CurrentQuiz.CurrentQuizUsers = new List<QuizUser>();
                    CurrentQuiz.CurrentQuizMaster = new DiscordUser
                    {
                        Id = quizMaster.Id.ToString(),
                        Name = quizMaster.Username,
                    };
                }

                PrepMessage = await QuizChannel.SendMessageAsync(
                    "<@&827310534352175135> Macht euch bereit ein Quiz beginnt bald! Wenn ihr mitspielen wollt reagiert auf diese Nachricht mit <:RainerSchlau:759174717155311627>");

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

                var message = "Das Quiz startet! Und dabei isch:" + Environment.NewLine;

                lock (ListLock)
                {
                    foreach (var quizUser in CurrentQuiz.CurrentQuizUsers)
                    {
                        message +=
                            $"<@{quizUser.Id}> Mit {quizUser.QuizWonTotal} gewonnen Quizes und {quizUser.QuizPointsTotal} Quizpunkten!{Environment.NewLine}";
                    }
                }

                message += $"Und dem guden: <@{CurrentQuiz.CurrentQuizMaster.Id}> als Quizmaster!";

                await QuizChannel.SendMessageAsync(message);

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

                var message = "Das Quiz ist beendet hier der Punktestand!:" + Environment.NewLine;
                lock (ListLock)
                {
                    var listSort = CurrentQuiz.CurrentQuizUsers.OrderByDescending(m => m.CurrentQuizPoints).ToList();

                    var i = 1;
                    foreach (var quizUser in listSort)
                    {
                        message += $"{i}. <@{quizUser.Id}> mit {quizUser.CurrentQuizPoints} Punkten! {Environment.NewLine}";
                        i++;
                    }

                    CurrentQuiz.CurrentQuizUsers = new List<QuizUser>();
                    CurrentQuiz.CurrentQuizMaster = new DiscordUser();
                }

                await QuizChannel.SendMessageAsync(message);

                CurrentQuiz.QuizState = QuizState.NotRunning;
                await OgerBot.Client.SetGameAsync("ob Haider vorm Tor stehen", type: ActivityType.Watching);
            }
            catch (Exception ex)
            {
                Log.Error(ex, nameof(StopQuiz));
            }
        }

        private static async Task ReactionAddedPrep(Discord.Cacheable<Discord.IUserMessage, ulong> message, ISocketMessageChannel channel, SocketReaction reaction)
        {
            try
            {
                if(CurrentQuiz.QuizState != QuizState.PrepPhase) return;
                if (channel.Id != QuizChannel.Id) return;
                if (message.Id != PrepMessage.Id) return;
                if (reaction.Emote.Name != "RainerSchlau") return;

                var user = await channel.GetUserAsync(reaction.UserId);

                var pointsTotal = await DataBase.GetQuizPointsTotal(user.Id);
                var winsTotal = await DataBase.GetTimesQuizWonTotal(user.Id);

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

        private static async Task ReactionAddedRunning(Discord.Cacheable<Discord.IUserMessage, ulong> message, ISocketMessageChannel channel, SocketReaction reaction)
        {
            try
            {
                if (CurrentQuiz.QuizState != QuizState.Running) return;
                if (channel.Id != QuizChannel.Id) return;
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

        private static async Task ReactionRemovedRunning(Discord.Cacheable<Discord.IUserMessage, ulong> message, ISocketMessageChannel channel, SocketReaction reaction)
        {
            try
            {
                if (CurrentQuiz.QuizState != QuizState.Running) return;
                if (channel.Id != QuizChannel.Id) return;
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
