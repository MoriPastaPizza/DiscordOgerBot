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

            await Context.Message.ReplyAsync($"Du bist derzeit auf Platz {rank}! Mit {currentUser.QuizPointsTotal} Punkten und {currentUser.QuizWonTotal} Gewonnen Quizes!");
        }

        [Command("reset")]
        [RequireOwner]
        public async Task ResetPoints()
        {
            DataBase.ResetQuizDatabase();
            await Context.Message.ReplyAsync("Quiz Datenbank gelöscht!");
        }
    }
}
