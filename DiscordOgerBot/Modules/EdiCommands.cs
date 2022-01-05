﻿using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordOgerBot.Controller;

namespace DiscordOgerBot.Modules
{
    public class EdiCommands : ModuleBase<SocketCommandContext>
    {

        private readonly string _imagePath = Path.GetFullPath(
            Path.Combine(AppContext.BaseDirectory, "../DiscordOgerBot/Images/Edi"));

        private const int EdiChance = 20;
        private readonly Random _rand = new();

        [Command("edy")]
        public async Task Edi()
        {
            if (!(Context.User is SocketGuildUser user)) return;
            //if (Context.Channel.Id != 925680854229995571)
            //{
            //    await ReplyAsync("Edi nur noch in: <#925680854229995571>");
            //    return;
            //}

            //if (user.GuildPermissions.KickMembers)
            //{
            //    await ReplyAsync("Mods dürfen nicht mit spielen <:loser:888861000303018054>");
            //    return;
            //}

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
                var timeout = TimeSpan.FromMinutes(_rand.Next(5, 121));
                await Context.Message.ReplyAsync($"Du hast {roll} gewürfelt: Das gibt nen Timeout für {timeout}");
                await user.SetTimeOutAsync(timeout);
                DataBase.IncreaseEdiTimeoutTotal(user.Id, timeout);
            }

            DataBase.IncreaseEdiUsed(user.Id);
        }

        [Command("edy rank")]
        [Alias("edy score")]
        public async Task GetEdiScore()
        {
            if (!(Context.User is SocketGuildUser user)) return;
            //if (Context.Channel.Id != 925680854229995571)
            //{
            //    await ReplyAsync("Edi nur noch in: <#925680854229995571>");
            //    return;
            //}

            //if (user.GuildPermissions.KickMembers)
            //{
            //    await ReplyAsync("Mods dürfen nicht mit spielen <:loser:888861000303018054>");
            //    return;
            //}

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
                                             $"Mit {currentUser.EdiTimeOutTotal} im Edi-Timeout!{Environment.NewLine}" +
                                             $"Du hast Edi {currentUser.EdiUsed} mal benutzt | {currentUser.EdiSuccessfull} mal davon erfolgreich!");

        }

        [Command("edy purge")]
        [RequireOwner]
        public async Task PurgeEdi()
        {
            DataBase.ResetEdiData();
            await Context.Channel.SendMessageAsync("Edi Scores purged!");
        }
    }
}
