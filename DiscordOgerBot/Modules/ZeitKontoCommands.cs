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
            var commandContext = new SocketCommandContext(Context.Client, Context.Message);
            var timeSpendWorking = await Controller.DataBase.GetTimeSpendWorking(Context.User, commandContext);

            var embedBuilder = new EmbedBuilder();

            var embed = embedBuilder
                .WithAuthor(Context.User)
                .WithTitle($"Zeitkonto von: {Context.User.Username}")
                .AddField($"Tage: {timeSpendWorking.Days} und {timeSpendWorking.Hours}:{timeSpendWorking.Minutes}",
                    $"Gesamt Stunden: {timeSpendWorking.TotalHours}")
                .Build();

            await Context.Channel.SendMessageAsync(embed: embed);
        }
    }
}
