using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace DiscordOgerBot.Modules
{
    public class EdiCommands : ModuleBase<SocketCommandContext>
    {

        private readonly string _imagePath = Path.GetFullPath(
            Path.Combine(AppContext.BaseDirectory, "../DiscordOgerBot/Images/Edi"));

        private readonly int _ediChance = 20;
        private readonly Random _rand = new();

        [Command("edy")]
        public async Task Edi()
        {
            if (!(Context.User is SocketGuildUser user)) return;
            if (Context.Channel.Id != 763782279548895233)
            {
                await ReplyAsync("Edi nur noch in: <#925680854229995571>");
                return;
            }

            //if (user.GuildPermissions.KickMembers)
            //{
            //    await ReplyAsync("Mods dürfen nicht mit spielen <:loser:888861000303018054>");
            //    return;
            //}

            var roll = _rand.Next(1, 101);
            var rollSuccess = roll <= _ediChance;

            if (rollSuccess)
            {
                var edis = Directory.GetFiles(_imagePath);
                var selectedEdi = edis[_rand.Next(edis.Length)];

                await Context.Channel.SendFileAsync(selectedEdi, $"Du hast {roll} gewürfelt: Hier ein Edi :3");
            }
            else
            {
                var timeout = TimeSpan.FromMinutes(_rand.Next(5, 121));
                await Context.Message.ReplyAsync($"Du hast {roll} gewürfelt: Das gibt nen Timeout für {timeout}");
                await user.SetTimeOutAsync(timeout);
            }
        }
    }
}
