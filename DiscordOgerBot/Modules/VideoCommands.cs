using System;
using System.IO;
using System.Threading.Tasks;
using Discord.Commands;

namespace DiscordOgerBot.Modules
{
    public class VideoCommands : ModuleBase<SocketCommandContext>
    {
        private readonly string _videoPath = Path.GetFullPath(
            Path.Combine(AppContext.BaseDirectory, "../DiscordOgerBot/Videos"));

        [Command("kindergarten")]
        public async Task SendKinderGarten()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/kindergarten.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("asylanten")]
        [Alias("asylant")]
        public async Task SendAsylanten()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/asylanten.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("alexa")]
        [Alias("Alegser", "Aleggser", "Aleggsa", "Alegsa")]
        [Summary("ein zufälliges Alexa Video")]
        public async Task SendAlexa()
        {
            var number = new Random().Next(1, 7);

            await Context.Channel.SendFileAsync(_videoPath + $"/alexa{number}.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("marzipan")]
        public async Task SendMarzipan()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/marzipan.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("frauchen")]
        [Alias("16h")]
        public async Task SendFrauchen()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/frauchen.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("mallah")]
        public async Task SendMallah()
        {
            var number = new Random().Next(1, 8);

            await Context.Channel.SendFileAsync(_videoPath + $"/mallah{number}.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("weltne")]
        [Alias("welten")]
        public async Task SendWelten()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/weltne.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("1900")]
        public async Task Send1900()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/1900.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("rettich")]
        public async Task SendRettich()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/rettich.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("haggebudne")]
        [Alias("hagebudne")]
        public async Task SendHagebudne()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/haggebudne.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("sohn")]
        public async Task SendSohn()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/sohn.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("xbuddn")]
        public async Task SendXBuddn()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/xbuddn.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("nixmehr")]
        [Alias("morgen")]
        public async Task SendNixMehr()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/MorgenIsNixMehr.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("wiggst")]
        [Alias("wichst", "wixt", "wixxt", "wigst")]
        public async Task SendWiggstIhrNet()
        {
            var number = new Random().Next(1, 3);

            await Context.Channel.SendFileAsync(_videoPath + $"/wiggsne{number}.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("oof")]
        public async Task SendOof()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/oof.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("heiho")]
        public async Task SendHeiHo()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/heiho.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("propaly")]
        public async Task SendPropaly()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/begin.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("falta")]
        [Alias("falter")]
        public async Task SendFalta()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/falta.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("nein")]
        [Alias("nö")]
        public async Task SendNein()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/nein.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("neudral")]
        [Alias("neutral")]
        public async Task SendNeutral()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/neudral.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("pferdefotze")]
        [Alias("liebe")]
        public async Task SendLiebe()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/pferdefotze.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("wunschliste")]
        [Alias("bettelliste")]
        public async Task SendWunschliste()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/wunschliste.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("zahnlücke")]
        [Alias("lachanfall")]
        public async Task SendZahnlücke()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/zahnlücke.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("wurst")]
        public async Task SendWurst()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/wurst.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("alter")]
        [Alias("alder", "alda")]
        public async Task SendAlder()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/alda.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("nüsseab")]
        public async Task SendnüsseAb()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/nüsseab.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("pulverisiert")]
        [Alias("These")]
        public async Task SendPulver()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/pulverisiert.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("bisexuell")]
        [Alias("biseggsuel")]
        public async Task SendBisex()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/biseggsuell.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("idioten")]
        [Alias("dawärt")]
        public async Task SendDieScheiße()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/AlleDieseScheiße.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("packtaus")]
        public async Task SendPacktAus()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/packtnaus.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("50er")]
        public async Task Send50Er()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/50er.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("paarmal")]
        public async Task SendPaarMal()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/aboarmal.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("laufen")]
        public async Task SendLaufen()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/laufenimmerso.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("neger")]
        public async Task SendNeger()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/neger.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("20cent")]
        public async Task Send20Cent()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/20cent.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }


        [Command("eis")]
        public async Task SendEis()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/eis.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("bock")]
        public async Task SendBock()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/bock.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("eier")]
        public async Task SendEier()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/windEier.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("mama")]
        public async Task SendMama()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/mama.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("saufen")]
        [Alias("wochenende", "neikibbn")]
        public async Task SendSaufen()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/neikibbn.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("sauerstoff")]
        public async Task SendSauerstoff()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/sauerstoff.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("zuggen")]
        [Alias("zuggne")]
        public async Task SendZuggne()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/zuggne.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("barcelona")]
        public async Task SendBarcelona()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/barcelona.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("bannhammer")]
        public async Task SendBanhammer()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/bannhammer.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("aufn")]
        public async Task SendAufn()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/aufn.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("aufklärungsvideo")]
        public async Task SendAufklärung()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/aufklärung.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }
    }
}
