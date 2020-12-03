using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;

namespace DiscordOgerBotWeb.Modules
{
    public class SoundCommands : ModuleBase<SocketCommandContext>
    {
        private readonly string _soundPath = Path.GetFullPath(
            Path.Combine(AppContext.BaseDirectory, "../OgerBot/sounds"));

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
        [Summary("*Der ist übel*")]
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
        [Summary("*Der ist übel*")]
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

        [Command("schnauf")]
        [Summary("*schnauf*")]
        public async Task SendSchnauf()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/schnauf.ogg", embed: Controller.OgerBot.GetStandartSoundEmbed());
        }

        [Command("russland")]
        public async Task SendRussland()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/russland.ogg", embed: Controller.OgerBot.GetStandartSoundEmbed());
        }

        [Command("igel")]
        [Alias("eagle")]
        public async Task SendIgel()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/igelhalb.mp3", embed: Controller.OgerBot.GetStandartSoundEmbed());
        }

        [Command("igelkreis")]
        [Alias("eagle cries", "eagle kreis", "igel kreis")]
        public async Task SendIgelKreis()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/igel.mp3", embed: Controller.OgerBot.GetStandartSoundEmbed());
        }

        [Command("urschrei")]
        public async Task SendUrSchrei()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/urschrei.mp3", embed: Controller.OgerBot.GetStandartSoundEmbed());
        }

        [Command("tschobb")]
        [Alias("such dir nen job", "job")]
        public async Task SendTschobb()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/tschobb.mp3", embed: Controller.OgerBot.GetStandartSoundEmbed());
        }

        [Command("aggst")]
        [Alias("axt", "net der drache")]
        public async Task SendAxt()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/aggst.mp3", embed: Controller.OgerBot.GetStandartSoundEmbed());
        }

        [Command("schlaganfall")]
        public async Task SendSchlaganfall()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/schlaganfall.mp3", embed: Controller.OgerBot.GetStandartSoundEmbed());
        }

        [Command("asia")]
        public async Task SendAsia()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/asia.mp3", embed: Controller.OgerBot.GetStandartSoundEmbed());
        }

        [Command("derjeniche")]
        [Alias("derjenige")]
        public async Task SendDerjenice()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/derjeniche.ogg", embed: Controller.OgerBot.GetStandartSoundEmbed());
        }


        [Command("freiflug")]
        [Alias("freiflüge")]
        public async Task SendFreiflug()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/freiflug.mp3", embed: Controller.OgerBot.GetStandartSoundEmbed());
        }

        [Command("alarm")]
        [Alias("wiwi")]
        public async Task SendWiwiw()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/wiwiwiwiiw.mp3", embed: Controller.OgerBot.GetStandartSoundEmbed());
        }

        [Command("eng")]
        [Alias("jung")]
        public async Task SendZuEng()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/zueng.ogg", embed: Controller.OgerBot.GetStandartSoundEmbed());
        }

        [Command("bells")]
        public async Task SendBells()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/tschinglBells.ogg", embed: Controller.OgerBot.GetStandartSoundEmbed());
        }

        [Command("trutoger")]
        [Alias("Truthahn")]
        public async Task SendHand()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/TrutogerImage.jpg", embed: Controller.OgerBot.GetStandartSoundEmbed());
            await Context.Channel.SendFileAsync(_soundPath + "/Trutoger.mp3", embed: Controller.OgerBot.GetStandartSoundEmbed());
        }

        [Command("schlau")]
        public async Task SendSchlau()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/schlau.ogg", embed: Controller.OgerBot.GetStandartSoundEmbed());
        }

    }
}
