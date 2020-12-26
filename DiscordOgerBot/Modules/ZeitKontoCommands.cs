using System;
using System.IO;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace DiscordOgerBot.Modules
{
    public class ZeitKontoCommands : ModuleBase<SocketCommandContext>
    {

        [Command("used")]
        public async Task SendUsed()
        {
            var commandContext = new SocketCommandContext(Context.Client, Context.Message);
            var timesUsed = await Controller.DataBase.GetTimesBotUsed(Context.User, commandContext);

            await Context.Channel.SendMessageAsync($"Du hast mich schon {timesUsed} mal benutzt!",
                embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("zeit")]
        [Alias("Zeitkonto")]
        public async Task GetTimeWorking()
        {
            var rand = new Random();
            var commandContext = new SocketCommandContext(Context.Client, Context.Message);
            var timeSpendWorking = await Controller.DataBase.GetTimeSpendWorking(Context.User, commandContext);
            var (currentRole, nextRole, timeTillRole) = Controller.OgerBot.GetRoleForTimeSpendWorking(timeSpendWorking);

            var embedBuilder = new EmbedBuilder();

            var embed = embedBuilder
                .WithTitle($"{Math.Round(timeSpendWorking.TotalHours, 2)} Stunden ({currentRole.Name})")
                .WithDescription($"Das sind {timeSpendWorking.Days} Tage {timeSpendWorking.Hours} Stunden und {timeSpendWorking.Minutes} Minuten, die du schon auf Discord gearbeitet hast. {Environment.NewLine}" +
                                 $"gezählt wird auf jedem Discord-Server wo ich aktiv bin!")

                .AddField($"Dein nächster Rang: {nextRole.Name}", $"in {Math.Round(timeTillRole.TotalHours, 2)} Sunden!")

                .AddField(
                    "Info",
                    "[Github](https://github.com/MoriPastaPizza/DiscordOgerBotWeb) | " +
                        "[Lade den Bot auf deinen Server ein!](https://discord.com/api/oauth2/authorize?client_id=761895612291350538&permissions=383040&scope=bot) | " +
                        "[DrachenlordKoreaDiscord](https://discord.gg/MmWQ5pCsHa)")


                .WithThumbnailUrl("https://raw.githubusercontent.com/MoriPastaPizza/DiscordOgerBot/master/DiscordOgerBot/Images/arbeitspulli.png")

                .WithFooter(footer =>
                    footer.Text =
                        Controller.OgerBot.FooterDictionary[rand.Next(Controller.OgerBot.FooterDictionary.Count)])
                .WithColor(currentRole.Color)
                .WithCurrentTimestamp()
                .WithAuthor(Context.User)
                .Build();

            await Context.Channel.SendMessageAsync(embed: embed);
        }
    }
}
