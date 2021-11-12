using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using DiscordOgerBot.Controller;
using Serilog;

namespace DiscordOgerBot.Modules
{
    public class VlogCommands : ModuleBase<SocketCommandContext>
    {
        [Command("vlog")]
        public async Task SendVlog()
        {
            try
            {
                var vlog = GenerateVlog();

                if (vlog == null) return;

                var persData = DataBase.GetPersistentData();
                persData.VlogCount += 1;
                
                var random = new Random();

                var embedBuilder = new EmbedBuilder();
                var embed = embedBuilder
                    .WithTitle($"Pflog #{persData.VlogCount} | Provided by Pflog-Fee Frl. Fei")
                    .WithDescription(vlog)
                    .WithFooter(footer =>
                        footer.Text =
                            OgerBot.FooterDictionary[random.Next(OgerBot.FooterDictionary.Count)])
                    .WithColor(Color.DarkGrey)
                    .WithCurrentTimestamp()
                    .Build();

                DataBase.SetPersistentData(persData);
                await Context.Channel.SendMessageAsync(embed: embed);
            }
            catch (Exception ex)
            {
                Log.Error(ex, nameof(SendVlog));
            }
        }

        private static string GenerateVlog()
        {
            try
            {
                var rand = new Random();
                var uhrzeit = DateTime.Now + TimeSpan.FromHours(1);
                var wach = rand.Next(1, 4);

                //Temperaturansage wäre auch Nice, aber auch hier - kein Plan, wie man das machen kann

                //Haideranzahl
                var haider = rand.Next(2, 100);

                //Generiere die ganzen Strings

                string[] begruessung = { "beim Drachen.", "bei dieser riesen Scheiße." };
                string[] haiderboese = { "haben die nix Besseres zu tun.", "dabei haben wir immer noch Goronner.", "dabei haben wir Betretungsverbot.", "wie kann man nur so asozial sein.", "sind die alle arbeitslos." };
                string[] haiderstraftat = { "mein Tor kaputt gemacht.", "mit Eiern auf mein Haus geworfen!", "mit Eiern nach mir geworfen, meine ganzen Hände stinken nach Ei!", "Farbe in meinen Hof geworfen… ICH HASSE ROSA.", "eine Wurst reingeworfen – ich hasse Lebensmittelverschwendung.", "Scheiße gebaut.", "Reifen in meinen Hof geworfen.", "ein Schild in die Wiese genagelt – das ist Sachbeschädigung!" };
                string[] polizei = { "macht wieder nix!", "war heute schon 3x da!", "fährt einfach nicht von selber mal durch!", "sagt ich soll reingehen und meinen Mund halten!" };
                string[] schicht = { "dabei ist das eigentlich die gute Schicht.", "das ist halt wieder die schlechte Schicht.", "das ist wieder die Schicht von Herrn P., der spielt nur auf seinem Handy rum!" };
                string[] selbstmitleid = { "bin dann angeblich wieder Derjeniche!", "bin dann derjeniche, der in den Knast gehen soll!", "bin angeblich derjeniche, der ständig die Polizei ruft!", "bin dann angeblich aggressiv, dabei verteidige ich mich nur!", "krieg dann wieder die Anzeige wegen Beleidigung, weil ich gesagt hab, dass Herr P. seinen Dschobb net macht, dabei hab ich nur gemeint, dass er den bei MIR net macht!" };
                string[] nachbarn = { "könnten auch mal was gegen die Haider tun!", "machen mal wieder nix!", "könnten auch mal die Polizei rufen!", "zeigen MICH an wegen Lärmbelästigung!" };
                string[] ankuendigung = { "nix mehr machen, ich bin müde.", "nix mehr machen, ich fühl mich net gut.", "vielleicht später streamen, aber ich weiß noch net wann.", "noch eine Folge Lustlord aufnehmen." };
                string[] ausrede = { "weil ich viele Termine hab.", "weil ich wieder Zelda suchte.", "weil ich im Haus am rumräumen bin." };
                string[] gesundheitszustand = { "bin aber eh a weng angeschlagen.", "hab aktuell wieder Wasser in den Beinen.", "hab die Griselkrätze.", "hab Bauchschmerzen.", "hab flodden Otto.", "hab immer noch die Wunde, wegen die Haider, die verheilt einfach nicht. Aber ich hab KEIN DIABETES! Muss was anderes sein." };
                string[] ankuendigung2 = { "ich arbeite grad an einem neuen Song, der sollte demnächst kommen.", "ich hab grad eine Idee für ein neues Projekt, da muss ich mal schauen, wie ich das mache." };
                string[] amerzon = { "da könnt ihr ja mal draufschauen, wenn ihr wollt.", "da hab ich gestern eine neue Wunschliste aufgemacht.", "da bräuchte ich jetzt a paar Sachen für a neues Projekt, aber ich sag euch net, was ich vorhabe.", "da hab ich Drucker-Papier drauf, das brauch ich für den Dragon-Monday, sonst kann ich den net machen ohne Skript." };
                string[] hermesbote = { "war heute noch net da.", "soll jetzt endlich mal kommen, ich warte auf Ersatzteile für meinen Stuhl, der ist schon wieder kaputt!", "kommt grad, Moment, ich muss kurz weg… ßo, wieder da.", "war heute da, ich hab hier das Paket, ich pack mal aus… Schon wieder Unterputzdeckel!" };
                string[] froindin = { "will später noch vorbeikommen, und dann ficken wir, bis die Wände einstürzen!", "hat mir diese Kette geschenkt.", "hat Schluss gemacht." };
                string[] haare = { "will ich mir wieder lang wachsen lassen.", "muss ich mal nachschneiden.", "Sehen heute voll gut aus, die Haider sind immer so neidisch auf meine Haare!" };
                string[] schoedder = { "ist heute von Boss – der Boss trägt Boss.", "hab ich seit 5 Tagen net gewechselt, weil ich mich net geduscht hab, und ich das immer erst wechsel, wenn ich mich dusch." };
                string[] knast = { "geh ich net, hört auf, immer davon zu sprechen!", "geh ich net, ich hab Berufung eingelegt!", "geh ich net, des seh ich gar net ein, ich hab mich nur verteidigt!", "geh ich net, da brech ich aus und geh zum Bumsen zu meiner Freundin!", "werd ich net gehen. Punkt!" };
                string[] audo = { "hört da endlich auf zu fragen.", "das will ich mir demnächst kaufen!", "das hab ich noch net!", "werd ich mir kaufen; der Lord fährt Ford!" };
                string[] fressi = { "ich werde mir jetzt was bei Dschino bestellen!", "ich schieb mir jetzt eine Pizza in den Ofen!", "mach mir jetzt ein paar Eier. Niemand isst so geil Eier wie ich!", "ich werde jetzt Grillen. Dabei ess ich ja gar net so viel Fleisch!", "hol mir jetzt was vom Beck in die Rewe!" };

                // Display the result.  
                return
                    $"Meddl Leute und herzlich Willkommen {begruessung[rand.Next(begruessung.Length)]} Heute gibt’s nicht viel zu erzählen, " +
                    $"es ist {uhrzeit.ToShortTimeString()} Uhr und ich bin seit {wach} Stunden wach. {Environment.NewLine}{Environment.NewLine} Es waren heute schon wieder {haider} " +
                    $"Haider vor meiner Haustür. Echt ey, {haiderboese[rand.Next(haiderboese.Length)]} Und dann haben die {haiderstraftat[rand.Next(haiderstraftat.Length)]} " +
                    $"Und die Polizei {polizei[rand.Next(polizei.Length)]}" +
                    $" Und {schicht[rand.Next(schicht.Length)]} Aber ICH {selbstmitleid[rand.Next(selbstmitleid.Length)]} Und die Nachbarn {nachbarn[rand.Next(nachbarn.Length)]} {Environment.NewLine}{Environment.NewLine}" +
                    $" Ich werde jetzt dann heute {ankuendigung[rand.Next(ankuendigung.Length)]} Ich komm grad eh zu nix, {ausrede[rand.Next(ausrede.Length)]} " +
                    $"Ich {gesundheitszustand[rand.Next(gesundheitszustand.Length)]} Dabei werd ich sonst nie krank! Aber {ankuendigung2[rand.Next(ankuendigung2.Length)]}{Environment.NewLine}{Environment.NewLine}" +
                    $" Wegen der Amerzon-Wunschliste - {amerzon[rand.Next(amerzon.Length)]} Der Hermes-Bote {hermesbote[rand.Next(hermesbote.Length)]}{Environment.NewLine}{Environment.NewLine} " +
                    $"Meine Freundin {froindin[rand.Next(froindin.Length)]} Meine Haare {haare[rand.Next(haare.Length)]} Mein D-Schödder {schoedder[rand.Next(schoedder.Length)]}" +
                    $" Und in Knast {knast[rand.Next(knast.Length)]} Und wegen Auto, {audo[rand.Next(audo.Length)]} Ich hab heute den ganzen Tag nix gegessen, " +
                    $"{fressi[rand.Next(fressi.Length)]}{Environment.NewLine}{Environment.NewLine} Bis zum nächsten Mal und Tschö Tschö.";
            }
            catch (Exception ex)
            {
                Log.Error(ex, nameof(GenerateVlog));
                return null;
            }
        }
    }
}
