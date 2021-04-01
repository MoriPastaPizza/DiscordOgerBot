using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            await QuizController.PrepareQuiz(Context.Channel);

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
    }
}
