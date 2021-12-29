﻿using System;
using System.IO;
using System.Threading.Tasks;
using Discord.Commands;

namespace DiscordOgerBot.Modules
{
    public class SoundCommands : ModuleBase<SocketCommandContext>
    {
        private readonly string _soundPath = Path.GetFullPath(
            Path.Combine(AppContext.BaseDirectory, "../DiscordOgerBot/Sounds"));

        [Command("schmusemaus")]
        public async Task SendSchmuseMaus()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/schmusemaus.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("kleiner schwanz")]
        [Alias("selbstbewusstsen")]
        public async Task SendKleinerSchwanz()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/kleiner.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("eingesperrt")]
        [Alias("kinderschänder", "ungerecht", "massenmörder")]
        public async Task SendMassenmorderKinderschander()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/massenmorder_kinderschander.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("knien")]
        [Alias("demut", "demut zeigen", "niemals knien")]
        public async Task SendKnien()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/knien.flac", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("halt dein maul")]
        [Alias("maul", "halts maul", "halt's maul")]
        public async Task SendMaul()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/haltdeinmaaaaaaaaaaaaaaaul.ogg", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("aufgelegt")]
        public async Task SendAufgelegt()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/aufgelegt.ogg", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }
        
        [Command("wer sonst")]
        [Alias("winkler wer sonst")]
        [Summary("*Wer sonst?*")]
        public async Task SendWerSonst()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/winklerwersonst.ogg", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }
        
        [Command("hurensohn")]
        [Alias("du blöder hurensohn")]
        public async Task SendHurensohn()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/dubloederhurensohn.ogg", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }
        
        [Command("asthma")]
        [Alias("schnaufen")]
        public async Task SendSchnaufen()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/asthma.ogg", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }
        
        [Command("nazis")]
        public async Task SendNazis()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/nazis.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("gadsenfutter")]
        [Alias("Katzenfutter", "Katzenfudder", "Gadsenfudder")]
        public async Task SendGadzenfutter()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/gadzenfutter.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("flodderotto")]
        [Alias("flodderoddo", "flotterotto", "flotter otto", "flodder oddo", "flodder otto")]
        public async Task SendFlodderOtto()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/flodderotto.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("600 full")]
        public async Task Send600Full()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/600km_full.ogg", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("600")]
        public async Task Send600()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/600km_cut.ogg", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("winkelspasti")]
        public async Task SendWinkelSpasti()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/winkelspasti.ogg", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("durchfall")]
        public async Task SendDurchfall()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/durchfall.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("ooh")]
        [Alias("oh", "ohh")]
        public async Task SendOoh()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/ooh.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("ferlassuns")]
        [Alias("verlass uns", "ferlass uns")]
        public async Task SendFerlassuns()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/ferlassuns.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("rechtssystem")]
        public async Task SendRechtssystem()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/rechtssystem.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("verdammteaxt")]
        [Alias("verdammte axt", "verdammte aggst")]
        public async Task SendVerdammteaxt()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/verdammteaxt.ogg", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("wasser")]
        [Alias("wachsen","nahrung")]
        public async Task SendWasser()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/wasser.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("rückmal")]
        public async Task SendRückmal()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/rueckmal.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("ori")]
        public async Task SendOri()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/ori.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("reina")]
        [Alias("reinaaa","raina","rainaaa")]
        public async Task SendReina()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/RAINAAAA.ogg", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("wenicher")]
        [Alias("derhaid")]
        public async Task SendWenicher()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/DerHaidWirdWenicherJunge.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("revolver")]
        [Alias("schuss", "mitrevolver", "headshot")]
        public async Task SendRevolver([Remainder] string args = null)
        {

            if (args == null)
            {
                var number = new Random().Next(1, 3);
                await Context.Channel.SendFileAsync(_soundPath + $"/revolver{number}.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
            }
            else
            {
                await Context.Channel.SendFileAsync(_soundPath + $"/revolver{args}.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
            }

        }

        [Command("PipiKaka")]
        [Alias("ppkk", "AA", "PipiKakaAA", "Kaka")]
        public async Task SendKaka()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/ppkkaa.ogg", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("fortnite")]
        [Alias("tanz", "grab")]
        public async Task SendFortnite()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/fortnitetanz.ogg", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("baddne")]
        [Alias("badden", "baddn")]
        public async Task SendBaddne()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/baddne.ogg", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("sieofsiefs")]
        [Alias("siefs", "sieof", "ßie", "ßieofßiefs", "ßiefs", "sie", "sea", "seaof", "sea of")]
        public async Task SendSieofsiefs()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/sieofsiefs.ogg", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("skrr")]
        [Alias("skör")]
        public async Task SendSkrr()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/skrrskrr.ogg", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("entglasen")]
        [Alias("entglasen")]
        public async Task SendEntglasung()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/entglasen.ogg", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("issa")]
        [Summary("*Da issa schon wiedda alta*")]
        public async Task SendIssaWieder()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/issawieder.ogg", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("gnoggt")]
        [Summary("*Ich will das der ausgnoggt wird*")]
        public async Task SendAusgnoggt()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/genoggt.ogg", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("nixlustig")]
        [Summary("*Brüllen*")]
        public async Task SendNixLustig()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/Da_is_nix_lustigs_dran_Du_Wichser.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("bringDichUm")]
        [Summary("*Brüllen*")]
        public async Task SendBringDichUm()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/Geh_und_bring_Dich_um_Du_Bastard.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("hurensohnFresse")]
        [Summary("*Brüllen*")]
        public async Task SendHurenSohnFresse()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/Halt_Deine_Hurnsohnfresse_und_verpiss_Dich.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("gott")]
        [Summary("*Brüllen*")]
        public async Task SendGott()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/Ich_bin_hier_der_Gott_und_nicht_Du.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("abschlachten")]
        [Summary("*Brüllen*")]
        public async Task SendAbschlachten()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/Ich_werd_euch_Hurensohne_abschlachten_wie_Tiere_die_ihr_seid.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("motorrad")]
        public async Task SendMotorrad()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/motorad.ogg", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("schmorn")]
        [Summary("*Der ist übel*")]
        public async Task SendSchmorn()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/Schmorn_wie_Viecher.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("wiebidde")]
        public async Task SendWieBidde()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/wiebidde.ogg", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("killingfloor")]
        public async Task SendKillingFloor()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/killingfloor.ogg", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("killingfloorfull")]
        public async Task SendKillingFloorFull()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/killingfloorfull.ogg", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("klingelständer")]
        public async Task SendKlingelStänder()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/Klingelständer.ogg", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("chloe")]
        public async Task SendChloe()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/chloe.ogg", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("qual")]
        [Summary("*Der ist übel*")]
        public async Task SendQual()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/qual.ogg", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("hurensohntür")]
        [Summary("*Brüllen*")]
        public async Task SendTür()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/hurensohnTuer.ogg", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("arrogant")]
        [Summary("*Brüllen*")]
        public async Task SendArrogant()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/arrogant.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("etzio")]
        [Summary("*Brüllen*")]
        public async Task SendEtzio()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/verdammtervollidiot.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("waszurhölle")]
        [Summary("*Brüllen*")]
        public async Task SendWasZurHölle()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/waszurhoellemachst.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("brüllheulen")]
        [Summary("*selbsterklärend*")]
        public async Task SendBrüllHeulen()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/abgefucktewichser.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("macht")]
        [Summary("*Brüllen*")]
        public async Task SendMacht()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/macht.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("arschlöcher")]
        [Summary("*Vadammten Aschlöchaaaa*")]
        public async Task SendArschlöcher()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/arschlocha.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("dorian")]
        public async Task SendDorian()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/dorianwichser.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("hauab")]
        [Summary("*Oger Urschrei*")]
        public async Task SendHauAb()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/hauab.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("keinrichter")]
        public async Task SendKeinRichter()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/keirichter.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("riesigebombe")]
        public async Task SendBombe()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/riesiche_bombe.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("hördichnet")]
        public async Task SendHörDichNet()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/spacken.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("unbesiegt")]
        [Summary("*IHR WERDET MICH NIEMALS BESIEGEN*")]
        public async Task SendUnbesiegt()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/niemalsbesiegen.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("winkla")]
        public async Task SendWinkla()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/winkla.ogg", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("ausdiemaus")]
        public async Task SendAusdieMaus()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/ausdiemaus.ogg", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("gimp")]
        public async Task SendGimp()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/gimp.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("danke")]
        [Summary("*Danke dafür Iblabli*")]
        public async Task SendDanke()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/dankedafur.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("html")]
        public async Task SendHtml()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/html.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("frosch")]
        public async Task SendFrosch()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/afrosch.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("arichtig")]
        public async Task SendRichtig()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/arichtig.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("gedrollt")]
        public async Task SendDrollt()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/getrolltwird.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("keiladde")]
        public async Task SendKeiLadde()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/keiladde.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("warum")]
        [Summary("*Warumääää?*")]
        public async Task SendWarum()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/warumaehdrachenshow.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("pennä")]
        public async Task SendPennä()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/dupennaeh.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("arbeiten")]
        public async Task SendArbeiten()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/amarbeitne.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("audo")]
        [Summary("*Auddo der wo fährt*")]
        public async Task SendAudo()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/hauptsachenaudo.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("rücken")]
        public async Task SendRücken()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/rueckmal.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("liebnhaider")]
        public async Task SendLiebnHaider()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/libnhater.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("schnauf")]
        [Summary("*schnauf*")]
        public async Task SendSchnauf()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/schnauf.ogg", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("russland")]
        public async Task SendRussland()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/russland.ogg", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("igel")]
        [Alias("eagle")]
        public async Task SendIgel()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/igelhalb.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("igelkreis")]
        [Alias("eagle cries", "eagle kreis", "igel kreis")]
        public async Task SendIgelKreis()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/igel.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("urschrei")]
        public async Task SendUrSchrei()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/urschrei.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("tschobb")]
        [Alias("such dir nen job", "job")]
        public async Task SendTschobb()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/tschobb.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("aggst")]
        [Alias("axt", "net der drache")]
        public async Task SendAxt()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/aggst.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("schlaganfallsound")]
        public async Task SendSchlaganfallsound()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/schlaganfall.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("asia")]
        public async Task SendAsia()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/1337.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("derjenichesound")]
        [Alias("derjenigesound")]
        public async Task SendDerjenicheSound()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/derjeniche.ogg", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("freiflug")]
        [Alias("freiflüge")]
        public async Task SendFreiflug()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/freiflug.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("alarm")]
        [Alias("wiwi","wiwiwi")]
        public async Task SendWiwiw()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/wiwiwiwiiw.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("eng")]
        [Alias("jung")]
        public async Task SendZuEng()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/zueng.ogg", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("bells")]
        public async Task SendBells()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/tschinglBells.ogg", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("trutoger")]
        [Alias("Truthahn")]
        public async Task SendHand()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/TrutogerImage.jpg");
            await Context.Channel.SendFileAsync(_soundPath + "/Trutoger.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("schlau")]
        public async Task SendSchlau()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/schlau.ogg", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("gumba")]
        public async Task SendGumba()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/gumba.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("englisch")]
        public async Task SendEnglisch()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/englischlord.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("findus")]
        public async Task SendAffenNecherFindus()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/findus.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("respekt")]
        public async Task SendRedetMitRespekt()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/reded_mid_repekt.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("reden")]
        [Alias("redne", "durcheinander")]
        public async Task SendWieOftNoch()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/Wie_oft_noch.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("familie")]
        public async Task SendFamilie()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/familie.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("rainwinver")]
        [Alias("schanzenroads")]
        public async Task SendCountryRoads()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/Schanzen-Roads_by_Rain_Winver.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("reyner")]
        [Alias("reiner, feddna")]
        public async Task SendReyner()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/fettena.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("gejaule")]
        public async Task SendGejaule()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/mei_hard_will_go_under.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("ohnedich")]
        public async Task SendOhneDich()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/hallelujah.mp3", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }
    }
}
