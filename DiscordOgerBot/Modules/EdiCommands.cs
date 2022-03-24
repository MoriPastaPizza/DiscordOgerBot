using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordOgerBot.Controller;
using DiscordOgerBot.Models;
using Serilog;

namespace DiscordOgerBot.Modules
{
    public class EdiCommands : ModuleBase<SocketCommandContext>
    {
        private const ulong NecoChannelId = 925680854229995571;
        private const ulong EdiPlayerRoleId = 935194892718710794;
        private const ulong EdiTimeoutRole = 934758329228591104;

        private const string EdiAuthorImageUrl = "https://raw.githubusercontent.com/MoriPastaPizza/DiscordOgerBot/master/DiscordOgerBot/Images/Edi/edi.png";

        private readonly Random _rand = new();

        [Command("edi")]
        public async Task Edi()
        {
            if (!(Context.User is SocketGuildUser user)) return;

            if (Context.Channel.Id != NecoChannelId)
            {
                await ReplyAsync($"Edi nur noch in: <#{NecoChannelId}>");
                return;
            }

            if (user.Roles.Any(m => m.Id == EdiTimeoutRole))
            {
                var timeTillUnlockUnix = DataBase.GetEdiTimeTillUnlock(user.Id);

                if (DateTimeOffset.Now.ToUnixTimeSeconds() < timeTillUnlockUnix)
                {
                    var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                    dtDateTime = dtDateTime.AddSeconds(timeTillUnlockUnix);
                    var timeCet = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dtDateTime, "Europe/Berlin");

                    await Context.Message.ReplyAsync($"Du bist noch im Timeout bis: {timeCet.ToString(new CultureInfo("DE-de"))} CET");
                    return;
                }

                await user.RemoveRoleAsync(EdiTimeoutRole);

            }

            var edi = EdiController.GetAnEdi();
            var embedBuilder = new EmbedBuilder();
            var totalTime = edi.BasicTime + edi.BonusTime;
            var endTime =
                TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Europe/Berlin") +
                totalTime;
            var endTimeUnix = DateTimeOffset.Now.ToUnixTimeSeconds() + (long)totalTime.TotalSeconds;


            embedBuilder
                .WithTitle(edi.Name)
                .WithDescription($"Du hast einen {edi.Name} gezogen!")
                .AddField("Deine Zeit", edi.BasicTime + " + Bonus: " + edi.BonusTime + Environment.NewLine
                + "Gesamt: " + totalTime)
                .AddField("Auszeit Ende", endTime.ToString(new CultureInfo("DE-de")) + " CET")
                .AddField("Roll", edi.Roll)
                .WithColor(edi.Color.Item1, edi.Color.Item2, edi.Color.Item3)
                .WithImageUrl(edi.ImageUrl)
                .WithAuthor("Good Luck Boi Edi", EdiAuthorImageUrl)
                .WithFooter(footer =>
                    footer.Text =
                        OgerBot.FooterDictionary[_rand.Next(OgerBot.FooterDictionary.Count)])
                .WithCurrentTimestamp();

            var embed = embedBuilder.Build();

            await user.AddRoleAsync(EdiTimeoutRole);
            DataBase.AddTimetoSeason1(user.Id, totalTime);
            DataBase.SetEdiTimeTillUnlock(user.Id, endTimeUnix);
            await ReplyAsync(embed: embed);
            await user.AddRoleAsync(EdiPlayerRoleId);
        }

        [Command("edi rank")]
        public async Task GetEdiScore()
        {
            if (!(Context.User is SocketGuildUser)) return;

            if (Context.Channel.Id != NecoChannelId)
            {
                await ReplyAsync($"Edi nur noch in: <#{NecoChannelId}>");
                return;
            }

            var allUsers = DataBase.GetAllUsers();
            var currentUser = allUsers.FirstOrDefault(m => m.Id == Context.User.Id.ToString());
            if (currentUser == null || currentUser.EdiTimeOutTotal > TimeSpan.Zero)
            {
                await Context.Message.ReplyAsync("Du hast noch keine Edi-Punkte!");
                return;
            }

            allUsers = allUsers
                .Where(m => m.EdiUsed > 0)
                .OrderByDescending(m => m.EdiTimeOutTotal)
                .ToList();

            var rank = 1 + allUsers.TakeWhile(user => user.Id != currentUser.Id).Count();

            await Context.Message.ReplyAsync($"Du bist derzeit auf **Platz {rank}!** {Environment.NewLine}" +
                                             $"Mit {Math.Round(currentUser.EdiTimeOutTotal.TotalHours, 2)} Stunden im Edi-Timeout!{Environment.NewLine}" +
                                             $"Du hast Edi {currentUser.EdiUsed} mal benutzt | {currentUser.EdiSuccessfull} mal davon erfolgreich!");

        }

        [Command("edi top")]
        public async Task GetTopEdi()
        {
            try
            {
                await GetTopSeason1();
            }
            catch (Exception ex)
            {
                Log.Error(ex, nameof(GetTopEdi));
            }
        }

        [Command("edi top all")]
        public async Task GetTopEdiAll()
        {
            try
            {
                if (Context.Channel.Id != NecoChannelId)
                {
                    await ReplyAsync($"Edi nur noch in: <#{NecoChannelId}>");
                    return;
                }

                var allUsers = DataBase.GetAllUsers();
                allUsers = allUsers
                    .Where(m => m.EdiTimeOutTotal > TimeSpan.Zero)
                    .OrderByDescending(m => m.EdiTimeOutTotal)
                    .ToList();

                var topString = string.Empty;
                for (var i = 0; i < 20; i++)
                {
                    topString += $"{1 + i}. {allUsers[i].Name} => {Math.Round(allUsers[i].EdiTimeOutTotal.TotalHours, 2)} Stunden {Environment.NewLine}";
                }

                var embedBuilder = new EmbedBuilder();
                var embed = embedBuilder
                    .WithTitle("Top Edi Fans <:edi:849529660492218368>")
                    .WithDescription(topString)
                    .WithFooter(footer =>
                        footer.Text =
                            OgerBot.FooterDictionary[_rand.Next(OgerBot.FooterDictionary.Count)])
                    .WithColor(Color.LightOrange)
                    .WithCurrentTimestamp()
                    .Build();

                await Context.Channel.SendMessageAsync(embed: embed);
            }
            catch (Exception ex)
            {
                Log.Error(ex, nameof(GetTopEdiAll));
            }
        }

        [Command("edi top 0")]
        public async Task GetTopSeason0()
        {
            try
            {
                if (Context.Channel.Id != NecoChannelId)
                {
                    await ReplyAsync($"Edi nur noch in: <#{NecoChannelId}>");
                    return;
                }

                var allUsers = DataBase.GetAllUsers();
                allUsers = allUsers
                    .Where(m => m.EdiTimeOutTotalSeason0 > TimeSpan.Zero)
                    .OrderByDescending(m => m.EdiTimeOutTotalSeason0)
                    .ToList();

                var topString = string.Empty;
                for (var i = 0; i < 20; i++)
                {
                    topString += $"{1 + i}. {allUsers[i].Name} => {Math.Round(allUsers[i].EdiTimeOutTotalSeason0.TotalHours, 2)} Stunden {Environment.NewLine}";
                }

                var embedBuilder = new EmbedBuilder();
                var embed = embedBuilder
                    .WithTitle("Top Edi Fans Season 0 <:edi:849529660492218368>")
                    .WithDescription(topString)
                    .WithFooter(footer =>
                        footer.Text =
                            OgerBot.FooterDictionary[_rand.Next(OgerBot.FooterDictionary.Count)])
                    .WithColor(Color.LightOrange)
                    .WithCurrentTimestamp()
                    .Build();

                await Context.Channel.SendMessageAsync(embed: embed);
            }
            catch (Exception ex)
            {
                Log.Error(ex, nameof(GetTopSeason0));
            }
        }

        [Command("edi top 1")]
        public async Task GetTopSeason1()
        {
            try
            {
                if (Context.Channel.Id != NecoChannelId)
                {
                    await ReplyAsync($"Edi nur noch in: <#{NecoChannelId}>");
                    return;
                }

                var allUsers = DataBase.GetAllUsers();
                allUsers = allUsers
                    .Where(m => m.EdiTimeOutTotalSeason1 > TimeSpan.Zero)
                    .OrderByDescending(m => m.EdiTimeOutTotalSeason1)
                    .ToList();

                var topString = string.Empty;
                for (var i = 0; i < allUsers.Count; i++)
                {
                    topString += $"{1 + i}. {allUsers[i].Name} => {Math.Round(allUsers[i].EdiTimeOutTotalSeason1.TotalHours, 2)} Stunden {Environment.NewLine}";
                    if(i >= 19) break;
                }

                var embedBuilder = new EmbedBuilder();
                var embed = embedBuilder
                    .WithTitle("Top Edi Fans Season 1 <:edi:849529660492218368>")
                    .WithDescription(topString)
                    .WithFooter(footer =>
                        footer.Text =
                            OgerBot.FooterDictionary[_rand.Next(OgerBot.FooterDictionary.Count)])
                    .WithColor(Color.LightOrange)
                    .WithCurrentTimestamp()
                    .Build();

                await Context.Channel.SendMessageAsync(embed: embed);
            }
            catch (Exception ex)
            {
                Log.Error(ex, nameof(GetTopSeason1));
            }
        }

        [Command("edi rates")]
        public async Task GetEdiRates()
        {
            try
            {
                var edis = EdiController.EdisWithProbs;

                var common = edis.First(m => m.Item.Type == EdiType.Common);
                var uncommon = edis.First(m => m.Item.Type == EdiType.Uncommon);
                var rare = edis.First(m => m.Item.Type == EdiType.Rare);
                var epic = edis.First(m => m.Item.Type == EdiType.Epic);
                var legendary = edis.First(m => m.Item.Type == EdiType.Legendary);
                var mythic = edis.First(m => m.Item.Type == EdiType.Mythic);

                var embedBuilder = new EmbedBuilder();

                embedBuilder
                    .WithTitle("Edi Rates Season 1")
                    .WithDescription("Aktuellen drop raten!")
                    .AddField("Common Edi", $"{Math.Round(common.Probability * 100, 2)}%")
                    .AddField("Uncommon Edi", $"{Math.Round(uncommon.Probability * 100, 2)}%")
                    .AddField("Rare Edi", $"{Math.Round(rare.Probability * 100, 2)}%")
                    .AddField("Epic Edi", $"{Math.Round(epic.Probability * 100, 2)}%")
                    .AddField("Legendary Edi", $"{Math.Round(legendary.Probability * 100, 2)}%")
                    .AddField("Mythic Edi", $"{Math.Round(mythic.Probability * 100, 2)}%")
                    .WithColor(Color.LightGrey)
                    .WithAuthor("Good Luck Boi Edi", EdiAuthorImageUrl)
                    .WithFooter(footer =>
                        footer.Text =
                            OgerBot.FooterDictionary[_rand.Next(OgerBot.FooterDictionary.Count)])
                    .WithCurrentTimestamp();

                var embed = embedBuilder.Build();

                await Context.Channel.SendMessageAsync(embed: embed);

            }
            catch (Exception ex)
            {
                Log.Error(ex, nameof(GetEdiRates));
            }
        }
    }
}
