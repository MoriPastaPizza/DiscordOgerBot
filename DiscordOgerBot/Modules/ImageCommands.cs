using System;
using System.IO;
using System.Threading.Tasks;
using Discord.Commands;

namespace DiscordOgerBot.Modules
{
    public class ImageCommands : ModuleBase<SocketCommandContext>
    {
        private readonly string _imagePath = Path.GetFullPath(
            Path.Combine(AppContext.BaseDirectory, "../DiscordOgerBot/Images"));

        [Command("derjeniche gif")]
        [Alias("nichtderjeniche","nicht derjeniche")]
        public async Task SendDerjeniche()
        {
            await Context.Channel.SendFileAsync(_imagePath + "/derjenichegif.gif", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("verbeugung")]
        [Alias("verbeugen")]
        public async Task SendVerbeugung()
        {
            await Context.Channel.SendFileAsync(_imagePath + "/verbeugung.gif", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("kind")]
        [Alias("kindinauto","kindweint","zeigteier")]
        public async Task SendKind()
        {
            await Context.Channel.SendFileAsync(_imagePath + "/kind.png", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("notiert")]
        [Alias("nodiert","nodierd")]
        public async Task SendNotiert([Remainder] string args = null)
        {
            if (args == null)
            {
                var number = new Random().Next(1, 3);

                await Context.Channel.SendFileAsync(_imagePath + $"/notiert{number}.gif", embed: Controller.OgerBot.GetStandardSoundEmbed());
            }
            else
            {
                await Context.Channel.SendFileAsync(_imagePath + $"/notiert{args}.gif", embed: Controller.OgerBot.GetStandardSoundEmbed());
            }
        }

        [Command("nice")]
        [Alias("nais")]
        public async Task SendNice([Remainder] string args = null)
        {
            if (args == null)
            {
                var number = new Random().Next(1, 4);

                await Context.Channel.SendFileAsync(_imagePath + $"/nais{number}.gif", embed: Controller.OgerBot.GetStandardSoundEmbed());
            }
            else
            {
                await Context.Channel.SendFileAsync(_imagePath + $"/nais{args}.gif", embed: Controller.OgerBot.GetStandardSoundEmbed());
            }
        }

        [Command("sport")]
        [Alias("sportlord")]
        public async Task SendSportLord([Remainder] string args = null)
        {
            if (args == null)
            {
                var number = new Random().Next(1, 6);

                await Context.Channel.SendFileAsync(_imagePath + $"/sportLord{number}.gif", embed: Controller.OgerBot.GetStandardSoundEmbed());
            }
            else
            {
                await Context.Channel.SendFileAsync(_imagePath + $"/sportLord{args}.gif", embed: Controller.OgerBot.GetStandardSoundEmbed());
            }
        }

        [Command("buddern")]
        [Alias("brot", "butter", "kochen")]
        public async Task SendBuddern()
        {
            await Context.Channel.SendFileAsync(_imagePath + "/buddern.gif", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("allah")]
        public async Task Sendallah()
        {
            await Context.Channel.SendFileAsync(_imagePath + "/allah.jpg", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }
    }
}
