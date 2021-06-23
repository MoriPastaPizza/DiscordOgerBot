using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using Serilog;

namespace DiscordOgerBot.Controller
{
    internal static class ChannelMirror
    {

        private static readonly List<ulong> ChannelsToMirror = new()
        {
            782676609349189642,
            782673372961964063,
            782683410803458078,
            783116240914350102,
            784787966827429899,
            782673501915578408,
            782683111237353482
        };

        internal static async Task MessageReceived(SocketMessage origMessage)
        {
            try
            {
                if (!(origMessage is SocketUserMessage message)) return;
                var context = new SocketCommandContext(OgerBot.Client, message);
                if (!ChannelsToMirror.Contains(context.Channel.Id)) return;

                var mirrorChannel = (SocketTextChannel)OgerBot.Client.GetChannel(857164844478365697);
                var messageContent = origMessage.Content ?? string.Empty;

                await mirrorChannel.SendMessageAsync("⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯");
                await mirrorChannel.SendMessageAsync($"**{message.Author.Username}** in **{message.Channel.Name}**:");

                if (messageContent != string.Empty)
                {
                    await mirrorChannel.SendMessageAsync(messageContent);
                }

                if (origMessage.Attachments.Count > 0 || origMessage.Attachments != null)
                {
                    foreach (var attachment in origMessage.Attachments)
                    {
                        await mirrorChannel.SendMessageAsync(attachment.Url);
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex, nameof(MessageReceived));
            }
        }
    }
}
