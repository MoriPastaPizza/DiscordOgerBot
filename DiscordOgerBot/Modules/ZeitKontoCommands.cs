using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace DiscordOgerBot.Modules
{
    public class ZeitKontoCommands : ModuleBase<SocketCommandContext>
    {

        [Command("checkusers")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        public async Task CheckUsersCommand()
        {
            await Controller.OgerBot.CheckUsers();
        }

        [Command("used")]
        public async Task SendUsed()
        {
            var commandContext = new SocketCommandContext(Context.Client, Context.Message);
            var timesUsed = await Controller.DataBase.GetTimesBotUsed(Context.User, commandContext.Guild.Id);

            await Context.Channel.SendMessageAsync($"Du hast mich schon {timesUsed} mal benutzt!",
                embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("zeit")]
        [Alias("Zeitkonto")]
        public async Task GetTimeWorking()
        {
            if (Context.Channel.Id != 802167112268775454)
            {
                await ReplyAsync("Zeit bitte nur noch bei der <#802167112268775454>");
                return;
            }

            var rand = new Random();
            var commandContext = new SocketCommandContext(Context.Client, Context.Message);
            var timeSpendWorking = await Controller.DataBase.GetTimeSpendWorking(Context.User, commandContext.Guild.Id);
            var (currentRole, nextRole, timeTillRole) = Controller.OgerBot.GetRoleForTimeSpendWorking(timeSpendWorking);
            var currentRoleLogoString = Globals.WorkingRanks.TimeForRanks
                .FirstOrDefault(m => m.RankId == currentRole.Id).ImageUrl;

            var nextRoleName = nextRole == null ? "Max. Level" : nextRole.Name;

            var embedBuilder = new EmbedBuilder();

            var embed = embedBuilder
                .WithTitle($"{Math.Round(timeSpendWorking.TotalHours, 2)} Stunden ({currentRole.Name})")
                .WithDescription($"Das sind {timeSpendWorking.Days} Tage {timeSpendWorking.Hours} Stunden und {timeSpendWorking.Minutes} Minuten, die du schon auf Discord gearbeitet hast. {Environment.NewLine}" +
                                 $"gezählt wird auf jedem Discord-Server wo ich aktiv bin!")

                .AddField($"Dein nächster Rang: {nextRoleName}", $"in {Math.Round(timeTillRole.TotalHours, 1)} Stunden!")

                .AddField(
                    "Info",
                    "[Github](https://github.com/MoriPastaPizza/DiscordOgerBotWeb) | " +
                        "[Lade den Bot auf deinen Server ein!](https://discord.com/api/oauth2/authorize?client_id=761895612291350538&permissions=383040&scope=bot) | " +
                        "[DrachenlordKoreaDiscord](https://discord.gg/jNkTrsZvW3)")


                .WithThumbnailUrl(currentRoleLogoString)

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
