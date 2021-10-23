using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Discord.Commands;
// ReSharper disable UnusedMember.Global

namespace DiscordOgerBot.Modules
{
    public class VideoCommands : ModuleBase<SocketCommandContext>
    {
        private readonly string _gitHubBaseUrl = "https://raw.githubusercontent.com/MoriPastaPizza/DiscordOgerBot/master/DiscordOgerBot/Videos/";

        private async Task SendVideo(string fileName, bool isSpoiler = false)
        {
            using var client = new WebClient();
            await using var stream = new MemoryStream(client.DownloadData(_gitHubBaseUrl + fileName));
            await Context.Channel.SendFileAsync(stream, fileName, embed: Controller.OgerBot.GetStandardSoundEmbed(), isSpoiler: isSpoiler);
        }

        [Command("mimimi")]
        [Alias("mimi")]
        public async Task SendMimi()
        {
            await SendVideo("mimimi.mp4");
        }

        [Command("alarm 3")]
        [Alias("siegesgeheul", "siegesgeheule")]
        public async Task SendAlarm()
        {
            await SendVideo("alarm3.mp4");
        }

        [Command("unbesiegt 2")]
        [Alias("unbesigt", "auf ewig", "auf ewich", "drachenlord4ever", "thebest", "the best")]
        public async Task SendUnbesigt()
        {
            await SendVideo("unbesigt.mp4");
        }

        [Command("größe")]
        [Alias("groeße", "grösse", "groesse")]
        public async Task SendGroesse()
        {
            await SendVideo("groesse.webm", true);
        }

        [Command("milchschnitte")]
        [Alias("schnitte", "milch schnitte", "schnidde")]
        public async Task SendSchnitte()
        {
            await SendVideo("schnitte.mp4");
        }

        [Command("russisch")]
        public async Task SendRussisch()
        {
            await SendVideo("russisch.mp4");
        }

        [Command("made")]
        [Alias("fliege", "fruchtfliege")]
        public async Task SendMade()
        {
            await SendVideo("made.mp4");
        }

        [Command("kampf")]
        [Alias("gampf", "kämpfen", "walze")]
        public async Task SendKampf()
        {
            await SendVideo("kampf.mp4");
        }

        [Command("besiegt")]
        [Alias("besigt", "nicht gewinnen")]
        public async Task SendBesiegt([Remainder] string args = null)
        {
            if (args == null)
            {
                var number = new Random().Next(1, 3);

                await SendVideo($"besiegt{number}.mp4");
            }
            else
            {
                await SendVideo($"besiegt{args}.mp4");
            }
        }

        [Command("freundin")]
        [Alias("liepe2", "liebe2", "liepe 2", "liebe 2")]
        public async Task SendFreundin()
        {
            await SendVideo("freundin.mp4");
        }
        
        [Command("besucher")]
        [Alias("richtig und wichtig", "richtig & wichtig", "richtig&wichtig", "besuche")]
        public async Task SendBesucher()
        {
            await SendVideo("besucher_sind_richtig_und_wichtig.mp4");
        }

        [Command("vogel")]
        [Alias("vogel alarm", "alarm2", "alarmvogel")]
        public async Task SendVogel()
        {
            await SendVideo("alarm_vogel.mp4");
        }

        [Command("hallo")]
        public async Task SendHallo()
        {
            await SendVideo("hallo.mp4");
        }

        [Command("helfen")]
        public async Task SendHelfen()
        {
            await SendVideo("helfen.mp4");
        }

        [Command("kopfab")]
        [Alias("kopf ab")]
        public async Task SendKopfAb()
        {
            await SendVideo("kopfab.mp4");
        }

        [Command("traum")]
        public async Task SendTraum()
        {
            await SendVideo("traum.mp4");
        }

        [Command("mori")]
        [Alias("lauch")]
        public async Task SendMori()
        {
            await SendVideo("mori.mp4");
        }

        [Command("hippielord")]
        [Alias("mensch")]
        public async Task SendHippielord()
        {
            await SendVideo("Hippielord.mp4");
        }

        [Command("stuhl")]
        [Alias("schduhl")]
        public async Task SendStuhl([Remainder] string args = null)
        {
            if (args == null)
            {
                var number = new Random().Next(1, 3);

                await SendVideo($"stuhl{number}.mp4");
            }
            else
            {
                await SendVideo($"stuhl{args}.mp4");
            }
        }

        [Command("paniert")]
        public async Task SendPaniert()
        {
            await SendVideo("paniert.mp4");
        }

        [Command("steffi")]
        public async Task SendSteffi()
        {
            await SendVideo("steffi.mp4");
        }

        [Command("dick")]
        [Alias("dicke eier")]
        public async Task SendDick()
        {
            await SendVideo("dick.mp4");
        }

        [Command("erwachsen")]
        [Alias("eigenes haus")]
        public async Task SendErwachsen()
        {
            await SendVideo("erwachsen.mp4");
        }

        [Command("winkler")]
        [Alias("wingler", "winggl")]
        public async Task SendWinkler()
        {
            await SendVideo("winkler.mp4");
        }

        [Command("badummtss")]
        [Alias("badumm tss","flachwitz")]
        public async Task SendBadummtss()
        {
            await SendVideo("badumm_tss.mp4");
        }

        [Command("ääh")]
        [Alias("ähm")]
        public async Task SendÄäh()
        {
            await SendVideo("ääh.mp4");
        }

        [Command("land")]
        [Alias("land gewinnen")]
        public async Task SendLand()
        {
            await SendVideo("land.mp4");
        }

        [Command("klo")]
        [Alias("toilette", "muscheln")]
        [Summary("ein zufälliges Klo Video")]
        public async Task SendKlo([Remainder] string args = null)
        {
            if (args == null)
            {
                var number = new Random().Next(1, 3);

                await SendVideo($"klo{number}.mp4");
            }
            else
            {
                await SendVideo($"klo{args}.mp4");
            }
        }

        [Command("welt")]
        [Alias("weld")]
        public async Task SendWelt([Remainder] string args = null)
        {
            if (args == null)
            {
                var number = new Random().Next(1, 3);

                await SendVideo($"welt{number}.mp4");
            }
            else
            {
                await SendVideo($"welt{args}.mp4");
            }
        }

        [Command("bär")]
        [Alias("bärenangriff", "angriff")]
        public async Task SendBaer()
        {
            await SendVideo("bär.mp4");
        }

        [Command("lesen")]
        [Alias("lesne")]
        public async Task SendLesne()
        {
            await SendVideo("lesne.mp4");
        }

        [Command("eisviech")]
        public async Task SendEisviech()
        {
            await SendVideo("Eisviech.mp4");
        }
        
        [Command("ach du scheiße")]
        [Alias("scheiße","achduscheiße")]
        public async Task SendScheiße()
        {
            await SendVideo("Ach_du_Scheiße.mp4");
        }

        [Command("kinderdisco")]
        [Summary("ein zufälliges Kinderdisco Video")]
        public async Task SendKinderdisco([Remainder] string args = null)
        {
            if(args == null)
            {
                var number = new Random().Next(1, 3);

                await SendVideo($"kinderdisco{number}.mp4");
            }
            else
            {
                await SendVideo($"kinderdisco{args}.mp4");
            }
        }

        [Command("sound")]
        public async Task SendSound()
        {
            await SendVideo("sound.mp4");
        }

        [Command("breitsamer")]
        [Alias("honig")]
        public async Task SendBreitsamer()
        {
            await SendVideo("breitsamer.mp4");
        }

        [Command("pilotenprüfung")]
        [Alias("pilot")]
        [Summary("ein zufälliges Piloten Video")]
        public async Task SendPilotenprüfung([Remainder] string args = null)
        {
            if (args == null)
            {
                var number = new Random().Next(1, 3);

                await SendVideo($"pilotenprüfung{number}.mp4");
            }
            else
            {
                await SendVideo($"pilotenprüfung{args}.mp4");
            }
        }

        [Command("vollpfostne")]
        [Alias("vollpfosten")]
        public async Task SendVollfosten()
        {
            await SendVideo("vollpfostne.mp4");
        }

        [Command("ich")]
        public async Task SendIch()
        {
            await SendVideo("ich.mp4");
        }

        [Command("sprachologe")]
        public async Task SendSprachologe()
        {
            await SendVideo("sprachologe.mp4");
        }

        [Command("derjeniche")]
        [Alias("derjenige")]
        public async Task SendDerjeniche()
        {
            await SendVideo("derjeniche.mp4");
        }

        [Command("furz")]
        public async Task SendFurz()
        {
            await SendVideo("furz.mp4");
        }

        [Command("arbeitslord")]
        [Alias("erarbeitet")]
        [Summary("ein zufälliges Arbeitslord Video")]
        public async Task SendArbeitslord([Remainder] string args = null)
        {
            if (args == null)
            {
                var number = new Random().Next(1, 3);

                await SendVideo($"arbeitslord{number}.mp4");
            }
            else
            {
                await SendVideo($"arbeitslord{args}.mp4");
            }
        }

        [Command("lisa")]
        [Alias("geile muschi","geile Lisa")]
        public async Task SendLisa()
        {
            await SendVideo("Lisa.mp4");
        }

        [Command("jagen")]
        [Alias("finden")]
        public async Task SendJagen()
        {
            await SendVideo("jagen.mp4");
        }

        [Command("panflöte")]
        [Alias("flötenlord")]
        public async Task SendPanflöte()
        {
            await SendVideo("panflöte.mp4");
        }

        [Command("liepe")]
        [Alias("liebe")]
        public async Task SendLiepe()
        {
            await SendVideo("liepe.mp4");
        }

        [Command("schönen abend")]
        [Alias("meddl off")]
        public async Task SendSchönenAbend()
        {
            await SendVideo("schönen_abend.mp4");
        }

        [Command("kissenwurf")]
        [Summary("ein zufälliges Kissenwurf Video")]
        public async Task SendKissenwurf([Remainder] string args = null)
        {
            if (args == null)
            {
                var number = new Random().Next(1, 6);

                await SendVideo($"kissenwurf{number}.mp4");
            }
            else
            {
                await SendVideo($"kissenwurf{args}.mp4");
            }
        }

        [Command("polizei")]
        [Alias("bolizei")]
        public async Task SendPolizei([Remainder] string args = null)
        {
            if (args == null)
            {
                var number = new Random().Next(1, 4);

                await SendVideo($"polizei{number}.mp4");
            }
            else
            {
                await SendVideo($"polizei{args}.mp4");
            }
        }

        [Command("hure")]
        public async Task SendHure()
        {
            await SendVideo("Hure.mp4");
        }

        [Command("look")]
        [Alias("neuerlook","schminke")]
        public async Task SendLook()
        {
            await SendVideo("Look.mp4");
        }

        [Command("kiste")]
        [Alias("sarkasmus","ichliebedich","kommvorbei","indiekiste")]
        public async Task SendKiste()
        {
            await SendVideo("in_die_Kiste_springen.mp4");
        }

        [Command("faust")]
        [Alias("stahl")]
        public async Task SendFaust()
        {
            await SendVideo("faust.mp4");
        }

        [Command("sagensoll")]
        [Alias("sagen","weißnichtmehr")]
        public async Task SendSagesoll()
        {
            await SendVideo("sagensoll.mp4");
        }

        [Command("hinten")]
        [Alias("dahinten", "dahintne","hintne")]
        public async Task SendHinten()
        {
            await SendVideo("hinten.mp4");
        }

        [Command("bodybuilder")]
        [Alias("fiech", "viech", "mukkies","muggies","folldesviech","folldesfiech")]
        public async Task SendBodybuilder()
        {
            await SendVideo("Follderbaddibilda.mp4");
        }

        [Command("fakt")]
        [Alias("faktis","faktist","faggd","fagd","fagt","faggt","faggdis","faggdist")]
        public async Task SendFakt()
        {
            await SendVideo("fakt_ist.mp4");
        }

        [Command("schlaganfall")]
        [Summary("ein zufälliges Schlaganfall Video")]
        public async Task SendSchlaganfall([Remainder] string args = null)
        {
            if (args == null)
            {
                var number = new Random().Next(1, 10);

                await SendVideo($"schlaganfall{number}.mp4");
            }
            else
            {
                await SendVideo($"schlaganfall{args}.mp4");
            }
        }

        [Command("anime")]
        [Alias("seakyle")]
        public async Task SendAnime([Remainder] string args = null)
        {
            if (args == null)
            {
                var number = new Random().Next(1, 3);

                await SendVideo($"seakyle{number}.mp4");
            }
            else
            {
                await SendVideo($"seakyle{args}.mp4");
            }
        }

        [Command("knie")]
        [Alias("fickteuch")]
        public async Task SendKnie()
        {
            await SendVideo("Knie.mp4");
        }

        [Command("dinge")]
        [Alias("ding")]
        public async Task SendDinge()
        {
            await SendVideo("Dinge.mp4");
        }

        [Command("kindergarten")]
        public async Task SendKinderGarten()
        {
            await SendVideo("kindergarten.mp4");
        }

        [Command("asylanten")]
        [Alias("asylant")]
        public async Task SendAsylanten()
        {
            await SendVideo("asylanten.mp4");
        }

        [Command("alexa")]
        [Alias("Alegser", "Aleggser", "Aleggsa", "Alegsa")]
        [Summary("ein zufälliges Alexa Video")]
        public async Task SendAlexa([Remainder] string args = null)
        {
            if (args == null)
            {
                var number = new Random().Next(1, 7);

                await SendVideo($"alexa{number}.mp4");
            }
            else
            {
                await SendVideo($"alexa{args}.mp4");
            }
        }

        [Command("marzipan")]
        public async Task SendMarzipan()
        {
            await SendVideo("marzipan.mp4");
        }

        [Command("frauchen")]
        [Alias("16h")]
        public async Task SendFrauchen()
        {
            await SendVideo("frauchen.mp4");
        }

        [Command("mallah")]
        public async Task SendMallah([Remainder] string args = null)
        {
            if (args == null)
            {
                var number = new Random().Next(1, 8);

                await SendVideo($"mallah{number}.mp4");
            }
            else
            {
                await SendVideo($"mallah{args}.mp4");
            }
        }

        [Command("weltne")]
        [Alias("welten")]
        public async Task SendWelten()
        {
            await SendVideo("weltne.mp4");
        }

        [Command("1900")]
        public async Task Send1900()
        {
            await SendVideo("1900.mp4");
        }

        [Command("rettich")]
        public async Task SendRettich()
        {
            await SendVideo("rettich.mp4");
        }

        [Command("haggebudne")]
        [Alias("hagebudne","hagebuddne")]
        [Summary("ein zufälliges Hagebudne Video")]
        public async Task SendHagebudne([Remainder] string args = null)
        {
            if (args == null)
            {
                var number = new Random().Next(1, 6);

                await SendVideo($"haggebudne{number}.mp4");
            }
            else
            {
                await SendVideo($"haggebudne{args}.mp4");
            }
        }

        [Command("sohn")]
        public async Task SendSohn()
        {
            await SendVideo("sohn.mp4");
        }

        [Command("xbuddn")]
        public async Task SendXBuddn()
        {
            await SendVideo("xbuddn.mp4");
        }

        [Command("nixmehr")]
        [Alias("morgen")]
        public async Task SendNixMehr()
        {
            await SendVideo("MorgenIsNixMehr.mp4");
        }

        [Command("wiggst")]
        [Alias("wichst", "wixt", "wixxt", "wigst","wiggsne")]
        public async Task SendWiggstIhrNet([Remainder] string args = null)
        {
            if (args == null)
            {
                var number = new Random().Next(1, 6);

                await SendVideo($"wiggsne{number}.mp4");
            }
            else
            {
                await SendVideo($"wiggsne{args}.mp4");
            }
        }

        [Command("oof")]
        public async Task SendOof()
        {
            await SendVideo("oof.mp4");
        }

        [Command("heiho")]
        public async Task SendHeiHo()
        {
            await SendVideo("heiho.mp4");
        }

        [Command("propaly")]
        public async Task SendPropaly()
        {
            await SendVideo("begin.mp4");
        }

        [Command("falta")]
        [Alias("falter")]
        public async Task SendFalta()
        {
            await SendVideo("falta.mp4");
        }

        [Command("nein")]
        [Alias("nö")]
        public async Task SendNein([Remainder] string args = null)
        {
            if (args == null)
            {
                var number = new Random().Next(1, 3);
                await SendVideo($"nein{number}.mp4");
            }
            else
            {
                await SendVideo($"nein{args}.mp4");
            }
        }

        [Command("neudral")]
        [Alias("neutral")]
        public async Task SendNeutral([Remainder] string args = null)
        {
            if (args == null)
            {
                var number = new Random().Next(1, 3);
                await SendVideo($"neudral{number}.mp4");
            }
            else
            {
                await SendVideo($"neudral{args}.mp4");
            }

        }

        [Command("pferdefotze")]
        [Alias("liebe")]
        public async Task SendLiebe()
        {
            await SendVideo("pferdefotze.mp4");
        }

        [Command("wunschliste")]
        [Alias("bettelliste")]
        public async Task SendWunschliste()
        {
            await SendVideo("wunschliste.mp4");
        }

        [Command("zahnlücke")]
        [Alias("lachanfall")]
        public async Task SendZahnlücke()
        {
            await SendVideo("zahnlücke.mp4");
        }

        [Command("wurst")]
        [Summary("ein zufälliges Wurst Video")]
        public async Task SendWurst([Remainder] string args = null)
        {
            if (args == null)
            {
                var number = new Random().Next(1, 3);

                await SendVideo($"wurst{number}.mp4");
            }
            else
            {
                await SendVideo($"wurst{args}.mp4");
            }
        }

        [Command("alter")]
        [Alias("alder", "alda")]
        public async Task SendAlder()
        {
            await SendVideo("alda.mp4");
        }

        [Command("nüsseab")]
        public async Task SendnüsseAb()
        {
            await SendVideo("nüsseab.mp4");
        }

        [Command("pulverisiert")]
        [Alias("These")]
        public async Task SendPulver()
        {
            await SendVideo("pulverisiert.mp4");
        }

        [Command("bisexuell")]
        [Alias("biseggsuel")]
        public async Task SendBisex()
        {
            await SendVideo("biseggsuell.mp4");
        }

        [Command("idioten")]
        [Alias("dawärt")]
        public async Task SendDieScheiße()
        {
            await SendVideo("AlleDieseScheiße.mp4");
        }

        [Command("packtaus")]
        public async Task SendPacktAus()
        {
            await SendVideo("packtnaus.mp4");
        }

        [Command("50er")]
        public async Task Send50Er()
        {
            await SendVideo("50er.mp4");
        }

        [Command("paarmal")]
        public async Task SendPaarMal()
        {
            await SendVideo("aboarmal.mp4");
        }

        [Command("laufen")]
        public async Task SendLaufen()
        {
            await SendVideo("laufenimmerso.mp4");
        }

        [Command("neger")]
        [Summary("ein zufälliges Neger Video")]
        public async Task SendNeger([Remainder] string args = null)
        {
            if (args == null)
            {
                var number = new Random().Next(1, 3);

                await SendVideo($"neger{number}.mp4");
            }
            else
            {
                await SendVideo($"neger{args}.mp4");
            }
        }

        [Command("20cent")]
        public async Task Send20Cent()
        {
            await SendVideo("20cent.mp4");
        }


        [Command("eis")]
        public async Task SendEis()
        {
            await SendVideo("eis.mp4");
        }

        [Command("bock")]
        public async Task SendBock()
        {
            await SendVideo("bock.mp4");
        }

        [Command("eier")]
        public async Task SendEier()
        {
            await SendVideo("windEier.mp4");
        }

        [Command("mama")]
        public async Task SendMama()
        {
            await SendVideo("mama.mp4");
        }

        [Command("saufen")]
        [Alias("wochenende", "neikibbn", "saufne")]
        public async Task SendSaufen([Remainder] string args = null)
        {
            if (args == null)
            {
                var number = new Random().Next(1, 3);

                await SendVideo($"saufen{number}.mp4");
            }
            else
            {
                await SendVideo($"saufen{args}.mp4");
            }
        }

        [Command("sauerstoff")]
        public async Task SendSauerstoff()
        {
            await SendVideo("sauerstoff.mp4");
        }

        [Command("zuggen")]
        [Alias("zuggne")]
        public async Task SendZuggne()
        {
            await SendVideo("zuggne.mp4");
        }

        [Command("barcelona")]
        public async Task SendBarcelona()
        {
            await SendVideo("barcelona.mp4");
        }

        [Command("bannhammer")]
        public async Task SendBanhammer()
        {
            await SendVideo("bannhammer.mp4");
        }

        [Command("aufn")]
        public async Task SendAufn()
        {
            await SendVideo("aufn.mp4");
        }

        [Command("aufklärungsvideo")]
        public async Task SendAufklärung()
        {
            await SendVideo("aufklärung.mp4");
        }

        [Command("marie")]
        public async Task SendMarie()
        {
            await SendVideo("marie.mp4");
        }

        [Command("honey")]
        [Alias("hannie", "hanni", "hanny", "hani")]
        public async Task SendHoney([Remainder] string args = null)
        {
            if (args == null)
            {
                var number = new Random().Next(1, 5);

                await SendVideo($"Honey{number}.mp4");
            }
            else
            {
                await SendVideo($"Honey{args}.mp4");
            }
        }

        [Command("stirb")]
        [Alias("klirren")]
        public async Task SendStirb()
        {
            await SendVideo("stirb.mp4");
        }

        [Command("mariokart")]
        [Alias("lieblingsmap")]
        public async Task SendMarioKart()
        {
            await SendVideo("mariokart.mp4");
        }

        [Command("ßo")]
        [Alias("so")]
        public async Task SendSo()
        {
            await SendVideo("ßo.mp4");
        }

        [Command("bass")]
        public async Task SendBass()
        {
            await SendVideo("bass.mp4");
        }
    }
}
