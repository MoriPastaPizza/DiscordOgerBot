using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace DiscordOgerBotWeb.Modules
{
    public class Commands : ModuleBase<SocketCommandContext>
    {

        private readonly string _soundPath = Path.GetFullPath(
            Path.Combine(AppContext.BaseDirectory, "../OgerBot/sounds"));

        [Command("help")]
        [Alias("hilfe")]
        public async Task SendHelp()
        {

            var random = new Random();
            var embed = new EmbedBuilder
            {
                Title = "Meddl Leudde!:metal:"
            };
            var buildEmbed = embed

                .WithDescription(
                    "Ich versuche deutsche Sätze ins Meddlfrängische zu übersetzen! " + Environment.NewLine +
                    "Eine Discord Nachricht auf Hochdeutsch nervt dich? Kein Problem! **reagiere einfach auf die Nachricht mit :OgerBot: **" + Environment.NewLine +
                    Environment.NewLine +
                    "**Der Server braucht ein Emoji mit dem Namen OgerBot!!** (Wende dich an die Server Admins die wissen das ganz bestimmt)" + Environment.NewLine +
                    Environment.NewLine +
                    "Die Nachricht kann auch wieder gelöscht werden indem man die Reaction wieder entfernt")

                .AddField("Sounds",
                "Ich kann auch Sounds abspielen! Für eine Übersicht schreib einfach **og commands**")

                .AddField("Willst du mich auf deinem eigenen Discord Server?",
                    "Das kannst du ganz einfach [hier](https://discord.com/api/oauth2/authorize?client_id=761895612291350538&permissions=383040&scope=bot) machen!")

                .AddField("Weitere Hilfe",
                    "Sollte ich mal nicht richtig funktionieren, komm ins [DrachenlordKoreaDiscord](https://discord.gg/kHkpemTS) oder wende dich bitte an meinen Erbauer:" + Environment.NewLine +
                    "[Discord](https://discordapp.com/users/386989432148066306) | [Reddit](https://www.reddit.com/user/MoriPastaPizza)")

                .AddField("Links",
                    "[Github](https://github.com/MoriPastaPizza/DiscordOgerBotWeb) | " +
                    "[Lade den Bot auf deinen Server ein!](https://discord.com/api/oauth2/authorize?client_id=761895612291350538&permissions=383040&scope=bot) | " +
                    "[DrachenlordKoreaDiscord](https://discord.gg/kHkpemTS)")

                .WithAuthor(Context.Client.CurrentUser)
                .WithFooter(footer =>
                    footer.Text =
                        Controller.OgerBot.FooterDictionary[random.Next(Controller.OgerBot.FooterDictionary.Count)])
                .WithColor(Color.Blue)
                .WithCurrentTimestamp()
                .Build();

            await Context.Channel.SendMessageAsync(embed: buildEmbed);
        }

        [Command("commands")]
        [Alias("command", "kommando", "kommandos", "sounds")]
        public async Task SendCommands()
        {
            var random = new Random();
            var commands = Controller.OgerBot.CommandService.Commands.ToList();
            var commandList = commands.Select(command => $"**{command.Aliases.Aggregate((i, j) => i + " " + j)}** {command.Summary}").ToList();
            var fieldString = commandList.Aggregate((i, j) => i + " | " + j).ToString();

            var embedBuilder = new EmbedBuilder
            {
                Title = "Commands"
            };

            embedBuilder
                .WithDescription(fieldString)
                .AddField("Links",
                    "[Github](https://github.com/MoriPastaPizza/DiscordOgerBotWeb) | " +
                    "[Lade den Bot auf deinen Server ein!](https://discord.com/api/oauth2/authorize?client_id=761895612291350538&permissions=383040&scope=bot) | " +
                    "[DrachenlordKoreaDiscord](https://discord.gg/kHkpemTS)")
                .WithAuthor(Context.Client.CurrentUser)
                .WithFooter(footer =>
                    footer.Text =
                        Controller.OgerBot.FooterDictionary[random.Next(Controller.OgerBot.FooterDictionary.Count)])
                .WithColor(Color.Red)
                .WithCurrentTimestamp();

            await Context.Channel.SendMessageAsync(embed: embedBuilder.Build());
        }


        [Command("skrr")]
        [Alias("skör")]
        public async Task SendSkrr()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/skrrskrr.ogg", embed: Controller.OgerBot.GetStandartSoundEmbed());
        }

        [Command("entglasen")]
        public async Task SendEntglasung()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/entglasen.ogg", embed: Controller.OgerBot.GetStandartSoundEmbed());
        }

        [Command("issa")]
        [Summary("*Da issa schon wiedda alta*")]
        public async Task SendIssaWieder()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/issawieder.ogg", embed: Controller.OgerBot.GetStandartSoundEmbed());
        }

        [Command("gnoggt")]
        [Summary("*Ich will das der ausgnoggt wird*")]
        public async Task SendAusgnoggt()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/genoggt.ogg", embed: Controller.OgerBot.GetStandartSoundEmbed());
        }

        [Command("nixlustig")]
        [Summary("*Brüllen*")]
        public async Task SendNixLustig()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/Da_is_nix_lustigs_dran_Du_Wichser.mp3", embed: Controller.OgerBot.GetStandartSoundEmbed());
        }

        [Command("bringDichUm")]
        [Summary("*Brüllen*")]
        public async Task SendBringDichUm()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/Geh_und_bring_Dich_um_Du_Bastard.mp3", embed: Controller.OgerBot.GetStandartSoundEmbed());
        }

        [Command("hurensohnFresse")]
        [Summary("*Brüllen*")]
        public async Task SendHurenSohnFresse()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/Halt_Deine_Hurnsohnfresse_und_verpiss_Dich.mp3", embed: Controller.OgerBot.GetStandartSoundEmbed());
        }

        [Command("gott")]
        [Summary("*Brüllen*")]
        public async Task SendGott()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/Ich_bin_hier_der_Gott_und_nicht_Du.mp3", embed: Controller.OgerBot.GetStandartSoundEmbed());
        }

        [Command("abschlachten")]
        [Summary("*Brüllen*")]
        public async Task SendAbschlachten()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/Ich_werd_euch_Hurensohne_abschlachten_wie_Tiere_die_ihr_seid.mp3", embed: Controller.OgerBot.GetStandartSoundEmbed());
        }

        [Command("motorrad")]
        public async Task SendMotorrad()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/motorad.ogg", embed: Controller.OgerBot.GetStandartSoundEmbed());
        }

        [Command("schmorn")]
        public async Task SendSchmorn()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/Schmorn_wie_Viecher.mp3", embed: Controller.OgerBot.GetStandartSoundEmbed());
        }

        [Command("wiebidde")]
        public async Task SendWieBidde()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/wiebidde.ogg", embed: Controller.OgerBot.GetStandartSoundEmbed());
        }

        [Command("killingfloor")]
        public async Task SendKillingFloor()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/killingfloor.ogg", embed: Controller.OgerBot.GetStandartSoundEmbed());
        }

        [Command("killingfloorfull")]
        public async Task SendKillingFloorFull()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/killingfloorfull.ogg", embed: Controller.OgerBot.GetStandartSoundEmbed());
        }

        [Command("klingelständer")]
        public async Task SendKlingelStänder()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/Klingelständer.ogg", embed: Controller.OgerBot.GetStandartSoundEmbed());
        }

        [Command("chloe")]
        public async Task SendChloe()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/chloe.ogg", embed: Controller.OgerBot.GetStandartSoundEmbed());
        }

        [Command("qual")]
        [Summary("*Der ist brutal*")]
        public async Task SendQual()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/qual.ogg", embed: Controller.OgerBot.GetStandartSoundEmbed());
        }

        [Command("hurensohntür")]
        [Summary("*Brüllen*")]
        public async Task SendTür()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/hurensohnTuer.ogg", embed: Controller.OgerBot.GetStandartSoundEmbed());
        }

        [Command("arrogant")]
        [Summary("*Brüllen*")]
        public async Task SendArrogant()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/arrogant.mp3", embed: Controller.OgerBot.GetStandartSoundEmbed());
        }

        [Command("etzio")]
        [Summary("*Brüllen*")]
        public async Task SendEtzio()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/verdammtervollidiot.mp3", embed: Controller.OgerBot.GetStandartSoundEmbed());
        }

        [Command("waszurhölle")]
        [Summary("*Brüllen*")]
        public async Task SendWasZurHölle()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/waszurhoellemachst.mp3", embed: Controller.OgerBot.GetStandartSoundEmbed());
        }

        [Command("brüllheulen")]
        [Summary("*selbsterklärend*")]
        public async Task SendBrüllHeulen()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/abgefucktewichser.mp3", embed: Controller.OgerBot.GetStandartSoundEmbed());
        }

        [Command("macht")]
        [Summary("*Brüllen*")]
        public async Task SendMacht()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/macht.mp3", embed: Controller.OgerBot.GetStandartSoundEmbed());
        }

        [Command("arschlöcher")]
        [Summary("*Vadammten Aschlöchaaaa*")]
        public async Task SendArschlöcher()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/arschlocha.mp3", embed: Controller.OgerBot.GetStandartSoundEmbed());
        }

        [Command("dorian")]
        public async Task SendDorian()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/dorianwichser.mp3", embed: Controller.OgerBot.GetStandartSoundEmbed());
        }

        [Command("hauab")]
        [Summary("*Oger Urschrei*")]
        public async Task SendHauAb()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/hauab.mp3", embed: Controller.OgerBot.GetStandartSoundEmbed());
        }

        [Command("keinrichter")]
        public async Task SendKeinRichter()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/keirichter.mp3", embed: Controller.OgerBot.GetStandartSoundEmbed());
        }

        [Command("riesigebombe")]
        public async Task SendBombe()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/riesiche_bombe.mp3", embed: Controller.OgerBot.GetStandartSoundEmbed());
        }

        [Command("hördichnet")]
        public async Task SendHörDichNet()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/spacken.mp3", embed: Controller.OgerBot.GetStandartSoundEmbed());
        }

        [Command("unbesiegt")]
        [Summary("*IHR WERDET MICH NIEMALS BESIEGEN*")]
        public async Task SendUnbesiegt()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/niemalsbesiegen.mp3", embed: Controller.OgerBot.GetStandartSoundEmbed());
        }

        [Command("winkla")]
        public async Task SendWinkla()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/winkla.ogg", embed: Controller.OgerBot.GetStandartSoundEmbed());
        }

        [Command("ausdiemaus")]
        public async Task SendAusdieMaus()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/ausdiemaus.ogg", embed: Controller.OgerBot.GetStandartSoundEmbed());
        }

        [Command("gimp")]
        public async Task SendGimp()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/gimp.mp3", embed: Controller.OgerBot.GetStandartSoundEmbed());
        }

        [Command("danke")]
        [Summary("*Danke dafür Iblabli*")]
        public async Task SendDanke()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/dankedafur.mp3", embed: Controller.OgerBot.GetStandartSoundEmbed());
        }

        [Command("html")]
        public async Task SendHtml()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/html.mp3", embed: Controller.OgerBot.GetStandartSoundEmbed());
        }

        [Command("frosch")]
        public async Task SendFrosch()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/afrosch.mp3", embed: Controller.OgerBot.GetStandartSoundEmbed());
        }

        [Command("arichtig")]
        public async Task SendRichtig()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/arichtig.mp3", embed: Controller.OgerBot.GetStandartSoundEmbed());
        }

        [Command("gedrollt")]
        public async Task SendDrollt()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/getrolltwird.mp3", embed: Controller.OgerBot.GetStandartSoundEmbed());
        }

        [Command("keiladde")]
        public async Task SendKeiLadde()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/keiladde.mp3", embed: Controller.OgerBot.GetStandartSoundEmbed());
        }

        [Command("warum")]
        [Summary("*Warumääää?*")]
        public async Task SendWarum()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/warumaehdrachenshow.mp3", embed: Controller.OgerBot.GetStandartSoundEmbed());
        }

        [Command("pennä")]
        public async Task SendPennä()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/dupennaeh.mp3", embed: Controller.OgerBot.GetStandartSoundEmbed());
        }

        [Command("arbeiten")]
        public async Task SendArbeiten()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/amarbeitne.mp3", embed: Controller.OgerBot.GetStandartSoundEmbed());
        }

        [Command("audo")]
        [Summary("*Auddo der wo fährt*")]
        public async Task SendAudo()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/hauptsachenaudo.mp3", embed: Controller.OgerBot.GetStandartSoundEmbed());
        }

        [Command("rücken")]
        public async Task SendRücken()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/rueckmal.mp3", embed: Controller.OgerBot.GetStandartSoundEmbed());
        }

        [Command("liebnhaider")]
        public async Task SendLiebnHaider()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/libnhater.mp3", embed: Controller.OgerBot.GetStandartSoundEmbed());
        }
    }
}
