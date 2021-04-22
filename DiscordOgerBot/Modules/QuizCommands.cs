using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordOgerBot.Controller;
using DiscordOgerBot.Globals;

namespace DiscordOgerBot.Modules
{
    [Group("quiz")]
    public class QuizCommands : ModuleBase<SocketCommandContext>
    {
        [Command("prep")]
        public async Task PrepareQuiz()
        {
            if (!(Context.User is SocketGuildUser user)) return;

            if (user.Roles.All(m => m.Id != 826886898114363432))
            {
                await Context.Message.ReplyAsync("Du bist nicht der Gwiss Masder du Spaggn!");
                return;
            }

            if (CurrentQuiz.QuizState != QuizState.NotRunning)
            {
                await Context.Message.ReplyAsync("Ein Quiz läuft bereits! Beende das alte erste um ein neues zu starten!");
                return;
            }

            await QuizController.PrepareQuiz(Context.Channel, user);

        }

        [Command("start")]
        public async Task StartQuiz()
        {
            if (!(Context.User is SocketGuildUser user)) return;

            if (user.Roles.All(m => m.Id != 826886898114363432))
            {
                await Context.Message.ReplyAsync("Du bist nicht der Gwiss Masder du Spaggn!");
                return;
            }

            if (CurrentQuiz.QuizState != QuizState.PrepPhase)
            {
                await Context.Message.ReplyAsync("Kein Quiz in der Prep. Phase! Bitte starte erst eins oder beende dein jetziges");
                return;
            }

            await QuizController.StartQuiz();
        }

        [Command("stop")]
        public async Task StopQuiz()
        {
            if (!(Context.User is SocketGuildUser user)) return;

            if (user.Roles.All(m => m.Id != 826886898114363432))
            {
                await Context.Message.ReplyAsync("Du bist nicht der Gwiss Masder du Spaggn!");
                return;
            }

            if (CurrentQuiz.QuizState != QuizState.Running)
            {
                await Context.Message.ReplyAsync("Es läuft derzeit kein Quiz bitte starte erst eins!");
                return;
            }

            await QuizController.StopQuiz();
        }

        [Command("points")]
        public async Task GetCurrentPoints()
        {
            if (!(Context.User is SocketGuildUser user)) return;
            if(Context.Channel.Id != CurrentQuiz.CurrentQuizChannel.Id) return;
            if (CurrentQuiz.QuizState != QuizState.Running)
            {
                await Context.Message.ReplyAsync("Es läuft derzeit kein Quiz!");
                return;
            }

            var quizUser = CurrentQuiz.CurrentQuizUsers.FirstOrDefault(m => m.Id == Context.User.Id.ToString());
            var quizMaster = CurrentQuiz.CurrentQuizMaster;

            if (quizUser == null && quizMaster.Id != Context.User.Id.ToString())
            {
                await Context.Message.ReplyAsync("Du spielst beim aktuellen Quiz nicht mit!");
                return;
            }

            await QuizController.GetCurrentPoints();

        }

        [Command("rank")]
        public async Task GetUserRank()
        {
            var allUsers = DataBase.GetAllUsers();
            var currentUser = allUsers.FirstOrDefault(m => m.Id == Context.User.Id.ToString());
            if (currentUser == null || currentUser.QuizPointsTotal < 1)
            {
                await Context.Message.ReplyAsync("Du hast noch keine Quizpunkte!");
                return;
            }

            allUsers = allUsers
                .Where(m => m.QuizPointsTotal > 0)
                .OrderByDescending(m => m.QuizPointsTotal)
                .ToList();

            var rank = 1 + allUsers.TakeWhile(user => user.Id != currentUser.Id).Count();

            var pointsString = currentUser.QuizPointsTotal == 1 ? "Punkt" : "Punkten";
            var quizString = currentUser.QuizWonTotal == 1 ? "gewonnenem Quiz" : "gewonnene Quizze";

            await Context.Message.ReplyAsync($"Du bist derzeit auf **Platz {rank}!** {Environment.NewLine} Mit {currentUser.QuizPointsTotal} {pointsString} und {currentUser.QuizWonTotal} {quizString}!");
        }

        [Command("abort")]
        public async Task AbortQuiz()
        {
            if (!(Context.User is SocketGuildUser user)) return;

            if (user.Roles.All(m => m.Id != 826886898114363432))
            {
                await Context.Message.ReplyAsync("Du bist nicht der Gwiss Masder du Spaggn!");
                return;
            }

            if (CurrentQuiz.QuizState == QuizState.NotRunning)
            {
                await Context.Message.ReplyAsync("Es läuft derzeit kein Quiz!");
                return;
            }

            await QuizController.AbortQuiz();

            await Context.Message.ReplyAsync("Quiz abgebrochen! Keine Punkte werden gezählt!");
        }

        [Command("help")]
        public async Task SendHelp()
        {
            var rand = new Random();
            var embed = new EmbedBuilder
            {
                Title = "❓Quiz Hilfe❓"
            };
            var buildEmbed = embed

                .WithDescription($"Der Quizmaster kann ein Quiz vorbereiten, starten & stoppen. {Environment.NewLine}" +
                                 $"Wärend der Vorbereitsungsphase können sich Spieler für die Quizrunde eintragen. {Environment.NewLine}" +
                                 $"Wärend der Spielphase kann der Quizmaster mit dem ✅ Emote richtige Antworten markieren und werten lassen. {Environment.NewLine}" +
                                 $"Wird das ✅ Emote vom Quizmaster entfernt wird die Antwort nicht gezählt. {Environment.NewLine}" +
                                 $"Nachdem der Quizmaster das Spiel beended hat werden die Punkte & Plätze der Spieler angezeigt. {Environment.NewLine}" +
                                 $"Spieler bekommen, je nach Platz, Quiz-Punkte auf ihr Konto gutgeschrieben, dies führt zu einem globalem Ranking.")

                .AddField("Commands für jeden",
                    $"**og quiz rank** Zeigt deinen globalen Rang an, falls du einen hast. {Environment.NewLine}" +
                    $"**og quiz points** Zeigt den Punktestand des aktuellen Quizes an. {Environment.NewLine}" +
                    $"**og quiz help** Zeigt diese Hilfe an.")


                .AddField("Commands für Quizmaster",
                    $"**og quiz prep** Bereitet ein neues Spiel vor. {Environment.NewLine}" +
                    $"**og quiz start** Startet das vorbereitete Quiz. {Environment.NewLine}" +
                    $"**og quiz stop** Stop das aktuelle Quiz und wertet die Punkte aus. {Environment.NewLine}" +
                    $"**og quiz abort** Stop das aktuelle Quiz **Punkte werden nicht gewertet.**")

                .AddField("Links", "[Github](https://github.com/MoriPastaPizza/DiscordOgerBotWeb) | " +
                                   "[Lade den Bot auf deinen Server ein!](https://discord.com/api/oauth2/authorize?client_id=761895612291350538&permissions=383040&scope=bot) | " +
                                   "[DrachenlordKoreaDiscord](https://discord.gg/jNkTrsZvW3)")

                .WithAuthor(Context.Client.CurrentUser)
                .WithFooter(footer =>
                    footer.Text =
                        OgerBot.FooterDictionary[rand.Next(OgerBot.FooterDictionary.Count)])
                .WithColor(Color.Red)
                .WithCurrentTimestamp()
                .Build();

            await Context.Channel.SendMessageAsync(embed: buildEmbed);
        }
    }
}
