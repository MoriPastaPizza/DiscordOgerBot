using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;

namespace DiscordOgerBotWeb.Modules
{
    public class SoundCommands : ModuleBase<SocketCommandContext>
    {
        private readonly string _soundPath = Path.GetFullPath(
            Path.Combine(AppContext.BaseDirectory, "../OgerBot/sounds"));


        private readonly string _videoPath = Path.GetFullPath(
            Path.Combine(AppContext.BaseDirectory, "../OgerBot/videos"));


        [Command("alarm")]
        [Alias("wiwi")]
        public async Task SendWiwiw()
        {
            await Context.Channel.SendFileAsync(_soundPath + "/wiwiwiwiiw.mp3", embed: Controller.OgerBot.GetStandartSoundEmbed());
        }

    }
}
