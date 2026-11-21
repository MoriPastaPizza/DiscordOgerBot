using Discord.Commands;

namespace DiscordOgerBot.Modules
{
    public class ImageCommands : ModuleBase<SocketCommandContext>
    {
        private const string ImagePath = "./Images";

        [Command("edi")]
        public async Task SendEdi()
        {
            await Context.Channel.SendFileAsync(ImagePath + "/edi.webp", text: "Zu Edis Ehren!" ,embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("mund")]
        public async Task SendMund()
        {
            await Context.Channel.SendFileAsync(ImagePath + "/mund.gif", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("derjeniche gif")]
        [Alias("nichtderjeniche","nicht derjeniche")]
        public async Task SendDerjeniche()
        {
            await Context.Channel.SendFileAsync(ImagePath + "/derjenichegif.gif", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("verbeugung")]
        [Alias("verbeugen")]
        public async Task SendVerbeugung([Remainder] string args = null)
        {
            if (args == null)
            {
                var number = new Random().Next(1, 3);

                await Context.Channel.SendFileAsync(ImagePath + $"/verbeugung{number}.gif", embed: Controller.OgerBot.GetStandardSoundEmbed());
            }
            else
            {
                await Context.Channel.SendFileAsync(ImagePath + $"/verbeugung{args}.gif", embed: Controller.OgerBot.GetStandardSoundEmbed());
            }
        }

        [Command("kind")]
        [Alias("kindinauto","kindweint","zeigteier")]
        public async Task SendKind()
        {
            await Context.Channel.SendFileAsync(ImagePath + "/kind.png", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("notiert")]
        [Alias("nodiert","nodierd")]
        public async Task SendNotiert([Remainder] string args = null)
        {
            if (args == null)
            {
                var number = new Random().Next(1, 3);

                await Context.Channel.SendFileAsync(ImagePath + $"/notiert{number}.gif", embed: Controller.OgerBot.GetStandardSoundEmbed());
            }
            else
            {
                await Context.Channel.SendFileAsync(ImagePath + $"/notiert{args}.gif", embed: Controller.OgerBot.GetStandardSoundEmbed());
            }
        }

        [Command("nice")]
        [Alias("nais")]
        public async Task SendNice([Remainder] string args = null)
        {
            if (args == null)
            {
                var number = new Random().Next(1, 4);

                await Context.Channel.SendFileAsync(ImagePath + $"/nais{number}.gif", embed: Controller.OgerBot.GetStandardSoundEmbed());
            }
            else
            {
                await Context.Channel.SendFileAsync(ImagePath + $"/nais{args}.gif", embed: Controller.OgerBot.GetStandardSoundEmbed());
            }
        }

        [Command("sport")]
        [Alias("sportlord")]
        public async Task SendSportLord([Remainder] string args = null)
        {
            if (args == null)
            {
                var number = new Random().Next(1, 6);

                await Context.Channel.SendFileAsync(ImagePath + $"/sportLord{number}.gif", embed: Controller.OgerBot.GetStandardSoundEmbed());
            }
            else
            {
                await Context.Channel.SendFileAsync(ImagePath + $"/sportLord{args}.gif", embed: Controller.OgerBot.GetStandardSoundEmbed());
            }
        }

        [Command("buddern")]
        [Alias("brot", "butter", "kochen")]
        public async Task SendBuddern()
        {
            await Context.Channel.SendFileAsync(ImagePath + "/buddern.gif", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("allah")]
        public async Task Sendallah()
        {
            await Context.Channel.SendFileAsync(ImagePath + "/allah.jpg", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }
    }
}
