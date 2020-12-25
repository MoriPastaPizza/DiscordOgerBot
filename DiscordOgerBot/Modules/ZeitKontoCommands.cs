using System;
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
        public async Task GetTimeWorking()
        {
            var rand = new Random();
            var commandContext = new SocketCommandContext(Context.Client, Context.Message);
            var timeSpendWorking = await Controller.DataBase.GetTimeSpendWorking(Context.User, commandContext);

            var embedBuilder = new EmbedBuilder();

            var embed = embedBuilder
                .WithTitle($"Zeitkonto von: {Context.User.Username}")
                .AddField($"{timeSpendWorking.Days} Tage, {timeSpendWorking.Hours} Stunden & {timeSpendWorking.Minutes} Minuten",
                    $"Gesamt Stunden: {timeSpendWorking.TotalHours}")

                .WithFooter(footer =>
                    footer.Text =
                        Controller.OgerBot.FooterDictionary[rand.Next(Controller.OgerBot.FooterDictionary.Count)])
                .WithColor(Color.Gold)
                .WithCurrentTimestamp()
                .WithAuthor(Context.User)
                .Build();

            await Context.Channel.SendMessageAsync(embed: embed);
        }
    }
}
