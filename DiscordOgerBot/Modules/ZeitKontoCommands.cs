using System.Threading.Tasks;
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

            await Context.Channel.SendMessageAsync($"Tage: {timeSpendWorking.Days}, Stunden: {timeSpendWorking.Hours}, Minuten: {timeSpendWorking.Minutes}");
        }
    }
}
