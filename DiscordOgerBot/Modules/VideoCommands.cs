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


        [Command("land")]
        [Alias("land gewinnen")]
        public async Task SendLand()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/land.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("klo")]
        [Alias("toilette", "muscheln")]
        [Summary("ein zufälliges Klo Video")]
        public async Task SendKlo([Remainder] string args = null)
        {
            if (args == null)
            {
                var number = new Random().Next(1, 3);

                await Context.Channel.SendFileAsync(_videoPath + $"/klo{number}.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
            }
            else
            {
                await Context.Channel.SendFileAsync(_videoPath + $"/klo{args}.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
            }
        }

        [Command("welt")]
        [Alias("weld")]
        public async Task SendWelt()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/welt.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("bär")]
        [Alias("bärenangriff", "angriff")]
        public async Task SendBaer()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/bär.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("lesen")]
        [Alias("lesne")]
        public async Task SendLesne()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/lesne.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("eisviech")]
        public async Task SendEisviech()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/Eisviech.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }
        
        [Command("ach du scheiße")]
        [Alias("scheiße","achduscheiße")]
        public async Task SendScheiße()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/Ach_du_Scheiße.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("kinderdisco")]
        [Summary("ein zufälliges Kinderdisco Video")]
        public async Task SendKinderdisco([Remainder] string args = null)
        {
            if(args == null)
            {
                var number = new Random().Next(1, 3);

                await Context.Channel.SendFileAsync(_videoPath + $"/kinderdisco{number}.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
            }
            else
            {
                await Context.Channel.SendFileAsync(_videoPath + $"/kinderdisco{args}.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
            }
        }

        [Command("sound")]
        public async Task SendSound()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/sound.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("breitsamer")]
        [Alias("honig")]
        public async Task SendBreitsamer()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/breitsamer.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("pilotenprüfung")]
        [Alias("pilot")]
        [Summary("ein zufälliges Piloten Video")]
        public async Task SendPilotenprüfung([Remainder] string args = null)
        {
            if (args == null)
            {
                var number = new Random().Next(1, 3);

                await Context.Channel.SendFileAsync(_videoPath + $"/pilotenprüfung{number}.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
            }
            else
            {
                await Context.Channel.SendFileAsync(_videoPath + $"/pilotenprüfung{args}.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
            }
        }

        [Command("vollpfostne")]
        [Alias("vollpfosten")]
        public async Task SendVollfosten()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/vollpfostne.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("ich")]
        public async Task SendIch()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/ich.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("sprachologe")]
        public async Task SendSprachologe()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/sprachologe.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("derjeniche")]
        [Alias("derjenige")]
        public async Task SendDerjeniche()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/derjeniche.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("furz")]
        public async Task SendFurz()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/furz.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("arbeitslord")]
        [Alias("erarbeitet")]
        [Summary("ein zufälliges Arbeitslord Video")]
        public async Task SendArbeitslord([Remainder] string args = null)
        {
            if (args == null)
            {
                var number = new Random().Next(1, 3);

                await Context.Channel.SendFileAsync(_videoPath + $"/arbeitslord{number}.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
            }
            else
            {
                await Context.Channel.SendFileAsync(_videoPath + $"/arbeitslord{args}.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
            }
        }

        [Command("lisa")]
        [Alias("geile muschi","geile Lisa")]
        public async Task SendLisa()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/Lisa.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed(), isSpoiler: true);
        }

        [Command("jagen")]
        [Alias("finden")]
        public async Task SendJagen()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/jagen.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("panflöte")]
        [Alias("flötenlord")]
        public async Task SendPanflöte()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/panflöte.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("liepe")]
        [Alias("liebe")]
        public async Task SendLiepe()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/liepe.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("schönen abend")]
        [Alias("meddl off")]
        public async Task SendSchönenAbend()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/schönen_abend.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("kissenwurf")]
        [Summary("ein zufälliges Kissenwurf Video")]
        public async Task SendKissenwurf([Remainder] string args = null)
        {
            if (args == null)
            {
                var number = new Random().Next(1, 3);

                await Context.Channel.SendFileAsync(_videoPath + $"/kissenwurf{number}.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
            }
            else
            {
                await Context.Channel.SendFileAsync(_videoPath + $"/kissenwurf{args}.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
            }
        }

        [Command("polizei")]
        [Alias("bolizei")]
        public async Task SendPolizei([Remainder] string args = null)
        {
            if (args == null)
            {
                var number = new Random().Next(1, 3);

                await Context.Channel.SendFileAsync(_videoPath + $"/polizei{number}.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
            }
            else
            {
                await Context.Channel.SendFileAsync(_videoPath + $"/polizei{args}.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
            }
        }

        [Command("hure")]
        public async Task SendHure()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/Hure.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("look")]
        [Alias("neuerlook","schminke")]
        public async Task SendLook()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/Look.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("kiste")]
        [Alias("sarkasmus","ichliebedich","kommvorbei","indiekiste")]
        public async Task SendKiste()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/in_die_Kiste_springen.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("faust")]
        [Alias("stahl")]
        public async Task SendFaust()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/faust.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("sagensoll")]
        [Alias("sagen","weißnichtmehr")]
        public async Task SendSagesoll()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/sagensoll.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("hinten")]
        [Alias("dahinten", "dahintne","hintne")]
        public async Task SendHinten()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/hinten.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("bodybuilder")]
        [Alias("fiech", "viech", "mukkies","muggies","folldesviech","folldesfiech")]
        public async Task SendBodybuilder()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/Follderbaddibilda.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("fakt")]
        [Alias("faktis","faktist","faggd","fagd","fagt","faggt","faggdis","faggdist")]
        public async Task SendFakt()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/fakt_ist.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("schlaganfall")]
        [Summary("ein zufälliges Schlaganfall Video")]
        public async Task SendSchlaganfall([Remainder] string args = null)
        {
            if (args == null)
            {
                var number = new Random().Next(1, 5);

                await Context.Channel.SendFileAsync(_videoPath + $"/schlaganfall{number}.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
            }
            else
            {
                await Context.Channel.SendFileAsync(_videoPath + $"/schlaganfall{args}.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
            }
        }

        [Command("anime")]
        [Alias("seakyle")]
        public async Task SendAnime()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/anime.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("knie")]
        [Alias("fickteuch")]
        public async Task SendKnie()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/Knie.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("dinge")]
        [Alias("ding")]
        public async Task SendDinge()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/Dinge.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

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
        public async Task SendAlexa([Remainder] string args = null)
        {
            if (args == null)
            {
                var number = new Random().Next(1, 7);

                await Context.Channel.SendFileAsync(_videoPath + $"/alexa{number}.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
            }
            else
            {
                await Context.Channel.SendFileAsync(_videoPath + $"/alexa{args}.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
            }
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
        public async Task SendMallah([Remainder] string args = null)
        {
            if (args == null)
            {
                var number = new Random().Next(1, 8);

                await Context.Channel.SendFileAsync(_videoPath + $"/mallah{number}.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
            }
            else
            {
                await Context.Channel.SendFileAsync(_videoPath + $"/mallah{args}.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
            }
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
        [Alias("hagebudne","hagebuddne")]
        [Summary("ein zufälliges Hagebudne Video")]
        public async Task SendHagebudne([Remainder] string args = null)
        {
            if (args == null)
            {
                var number = new Random().Next(1, 5);

                await Context.Channel.SendFileAsync(_videoPath + $"/haggebudne{number}.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
            }
            else
            {
                await Context.Channel.SendFileAsync(_videoPath + $"/haggebudne{args}.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
            }
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
        [Alias("wichst", "wixt", "wixxt", "wigst","wiggsne")]
        public async Task SendWiggstIhrNet([Remainder] string args = null)
        {
            if (args == null)
            {
                var number = new Random().Next(1, 5);

                await Context.Channel.SendFileAsync(_videoPath + $"/wiggsne{number}.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
            }
            else
            {
                await Context.Channel.SendFileAsync(_videoPath + $"/wiggsne{args}.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
            }
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
        public async Task SendNein([Remainder] string args = null)
        {
            if (args == null)
            {
                var number = new Random().Next(1, 3);
                await Context.Channel.SendFileAsync(_videoPath + $"/nein{number}.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
            }
            else
            {
                await Context.Channel.SendFileAsync(_videoPath + $"/nein{args}.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
            }
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
        [Summary("ein zufälliges Wurst Video")]
        public async Task SendWurst([Remainder] string args = null)
        {
            if (args == null)
            {
                var number = new Random().Next(1, 3);

                await Context.Channel.SendFileAsync(_videoPath + $"/wurst{number}.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
            }
            else
            {
                await Context.Channel.SendFileAsync(_videoPath + $"/wurst{args}.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
            }
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
        [Summary("ein zufälliges Neger Video")]
        public async Task SendNeger([Remainder] string args = null)
        {
            if (args == null)
            {
                var number = new Random().Next(1, 3);

                await Context.Channel.SendFileAsync(_videoPath + $"/neger{number}.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
            }
            else
            {
                await Context.Channel.SendFileAsync(_videoPath + $"/neger{args}.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
            }
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

        [Command("marie")]
        public async Task SendMarie()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/marie.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("honey")]
        [Alias("hannie", "hanni", "hanny", "hani")]
        public async Task SendHoney([Remainder] string args = null)
        {
            if (args == null)
            {
                var number = new Random().Next(1, 4);

                await Context.Channel.SendFileAsync(_videoPath + $"/Honey{number}.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
            }
            else
            {
                await Context.Channel.SendFileAsync(_videoPath + $"/Honey{args}.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
            }
        }

        [Command("stirb")]
        [Alias("klirren")]
        public async Task SendStirb()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/stirb.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("mariokart")]
        [Alias("lieblingsmap")]
        public async Task SendMarioKart()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/mariokart.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("ßo")]
        [Alias("so")]
        public async Task SendSo()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/ßo.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("bass")]
        public async Task SendBass()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/bass.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }
    }
}
