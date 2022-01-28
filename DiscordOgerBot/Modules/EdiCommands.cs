using System;
using System.IO;
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

        private readonly string _imagePath = Path.GetFullPath(
            Path.Combine(AppContext.BaseDirectory, "../DiscordOgerBot/Images/Edi/Common"));

        private const int EdiChance = 20;
        private const ulong NecoChannelId = 925680854229995571;
        private const ulong EdiPlayerRoleId = 935194892718710794;
        private const ulong EdiTimeoutRole = 934758329228591104;

        private const string EdiAuthorImageUrl = "https://raw.githubusercontent.com/MoriPastaPizza/DiscordOgerBot/master/DiscordOgerBot/Images/Edi/edi.png";

        private readonly Random _rand = new();

        [Command("edi")]
        public async Task Edi()
        {
            if (!(Context.User is SocketGuildUser user)) return;

            await ReplyAsync("Edi derzeit pausiert für Season 1!");
            return;

            if (Context.Channel.Id != NecoChannelId)
            {
                await ReplyAsync($"Edi nur noch in: <#{NecoChannelId}>");
                return;
            }

            if (user.GuildPermissions.KickMembers)
            {
                await ReplyAsync("Mods dürfen nicht mit spielen <:loser:888861000303018054>");
                return;
            }

            var roll = _rand.Next(1, 101);
            var rollSuccess = roll <= EdiChance;

            if (rollSuccess)
            {
                var edis = Directory.GetFiles(_imagePath);
                var selectedEdi = edis[_rand.Next(edis.Length)];

                await Context.Channel.SendFileAsync(selectedEdi, $"Du hast {roll} gewürfelt: Hier ein Edi :3");
                DataBase.IncreaseEdiSuccessfull(user.Id);
            }
            else
            {
                var timeout = TimeSpan.FromMinutes(_rand.Next(5, 181));
                await Context.Message.ReplyAsync($"Du hast {roll} gewürfelt: Das gibt nen Timeout für {timeout}");
                await user.SetTimeOutAsync(timeout);
                DataBase.IncreaseEdiTimeoutTotal(user.Id, timeout);
            }

            DataBase.IncreaseEdiUsed(user.Id);
            await user.AddRoleAsync(EdiPlayerRoleId);
        }

        [Command("edi rank")]
        [Alias("edi score")]
        [RequireOwner]
        public async Task GetEdiScore()
        {
            if (!(Context.User is SocketGuildUser user)) return;

            if (Context.Channel.Id != NecoChannelId)
            {
                await ReplyAsync($"Edi nur noch in: <#{NecoChannelId}>");
                return;
            }

            if (user.GuildPermissions.KickMembers)
            {
                await ReplyAsync("Mods dürfen nicht mit spielen <:loser:888861000303018054>");
                return;
            }

            var allUsers = DataBase.GetAllUsers();
            var currentUser = allUsers.FirstOrDefault(m => m.Id == Context.User.Id.ToString());
            if (currentUser == null || currentUser.EdiUsed < 1)
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
        [RequireOwner]
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
        [RequireOwner]
        public async Task GetTopEdiAll()
        {
            try
            {
                //if (Context.Channel.Id != NecoChannelId)
                //{
                //    await ReplyAsync($"Edi nur noch in: <#{NecoChannelId}>");
                //    return;
                //}

                var allUsers = DataBase.GetAllUsers();
                allUsers = allUsers
                    .Where(m => m.EdiUsed > 0)
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
        [RequireOwner]
        public async Task GetTopSeason0()
        {
            try
            {
                //if (Context.Channel.Id != NecoChannelId)
                //{
                //    await ReplyAsync($"Edi nur noch in: <#{NecoChannelId}>");
                //    return;
                //}

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
        [RequireOwner]
        public async Task GetTopSeason1()
        {
            try
            {
                //if (Context.Channel.Id != NecoChannelId)
                //{
                //    await ReplyAsync($"Edi nur noch in: <#{NecoChannelId}>");
                //    return;
                //}

                var allUsers = DataBase.GetAllUsers();
                allUsers = allUsers
                    .Where(m => m.EdiTimeOutTotalSeason1 > TimeSpan.Zero)
                    .OrderByDescending(m => m.EdiTimeOutTotalSeason1)
                    .ToList();

                var topString = string.Empty;
                for (var i = 0; i < 20; i++)
                {
                    topString += $"{1 + i}. {allUsers[i].Name} => {Math.Round(allUsers[i].EdiTimeOutTotalSeason1.TotalHours, 2)} Stunden {Environment.NewLine}";
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
        [RequireOwner]
        public async Task GetEdiRates()
        {
            try
            {
                var edis = EdiController.EdisWithProbs;

                var common = edis.FirstOrDefault(m => m.Item.Type == EdiType.Common);
                var uncommon = edis.FirstOrDefault(m => m.Item.Type == EdiType.Uncommon);
                var rare = edis.FirstOrDefault(m => m.Item.Type == EdiType.Rare);
                var epic = edis.FirstOrDefault(m => m.Item.Type == EdiType.Epic);
                var legendary = edis.FirstOrDefault(m => m.Item.Type == EdiType.Legendary);
                var mythic = edis.FirstOrDefault(m => m.Item.Type == EdiType.Mythic);

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


        [Command("edi merge")]
        [RequireOwner]
        public async Task MergeEdi()
        {
            try
            {
                var count = DataBase.MergeEdi();
                await Context.Message.ReplyAsync($"Merged {count} Users!");
            }
            catch (Exception e)
            {
                Log.Error(e, nameof(MergeEdi));
            }
        }

        [Command("edi test")]
        [RequireOwner]
        public async Task Test()
        {
            try
            {
                if (!(Context.User is SocketGuildUser user)) return;

                if (user.Roles.Any(m => m.Id == EdiTimeoutRole))
                {
                    var timeTillUnlockUnix = DataBase.GetEdiTimeTillUnlock(user.Id);
                    var timeTillUnlock = DateTimeOffset.FromUnixTimeSeconds(timeTillUnlockUnix);
                    var timeCet =
                        TimeZoneInfo.ConvertTimeBySystemTimeZoneId(timeTillUnlock, "Europe/Berlin");

                    await ReplyAsync($"Du bist noch im Timeout bis: {timeCet}");
                    return;
                }

                var edi = EdiController.GetAnEdi();
                var embedBuilder = new EmbedBuilder();
                var totalTime = edi.BasicTime + edi.BonusTime;
                var endTime =
                    TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Europe/Berlin") +
                    totalTime;
                var endTimeUnix = new DateTimeOffset(endTime).ToUnixTimeSeconds();
                
                embedBuilder
                    .WithTitle(edi.Name)
                    .WithDescription($"Du hast einen {edi.Name} gezogen!")
                    .AddField("Deine Zeit", edi.BasicTime + " + Bonus: "  + edi.BonusTime + Environment.NewLine
                    + "Gesamt: " + totalTime)
                    .AddField("Auszeit Ende", endTime + " CET")
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
                await Context.Message.ReplyAsync(embed: embed);

            }
            catch (Exception ex)
            {
                Log.Error(ex, nameof(Test));
            }
        }
    }
}
