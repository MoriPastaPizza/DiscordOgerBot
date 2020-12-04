using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace DiscordOgerBotWeb.Modules
{
    public class SayCommands : ModuleBase<SocketCommandContext>
    {
        [Command("say")]
        public async Task SendCommand([Remainder]string message)
        {
            if(!(Context.User is SocketGuildUser user)) return;
            if (!user.GuildPermissions.KickMembers)
            {
                await Context.Channel.SendMessageAsync($"{user.Mention} Auf dich hör ich ned du Spaggn, ich hab Mussig an!!");
            }

            await Context.Message.DeleteAsync();
            await Context.Channel.SendMessageAsync(message);
        }
    }
}
