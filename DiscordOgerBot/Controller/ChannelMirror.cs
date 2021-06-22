using System;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using Serilog;

namespace DiscordOgerBot.Controller
{
    internal static class ChannelMirror
    {
        internal static async Task MessageReceived(SocketMessage origMessage)
        {
            try
            {
                if (!(origMessage is SocketUserMessage message)) return;
                var context = new SocketCommandContext(OgerBot.Client, message);
                if (context.Channel.Id != 782676609349189642) return;

                var mirrorChannel = (SocketTextChannel)OgerBot.Client.GetChannel(856581010039767050);
                var messageContent = origMessage.Content ?? string.Empty;

                await mirrorChannel.SendMessageAsync("⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯");
                await mirrorChannel.SendMessageAsync($"{message.Author.Username}:");

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
