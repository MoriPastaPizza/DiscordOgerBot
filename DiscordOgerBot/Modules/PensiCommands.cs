using Discord.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DiscordOgerBot.Modules
{
    public class PensiCommands : ModuleBase<SocketCommandContext>
    {
        private readonly string _pensiPath = Path.GetFullPath(
           Path.Combine(AppContext.BaseDirectory, "../DiscordOgerBot/Pensi"));

        [Command("pensi")]
        [Alias("penis","speer")]
        public async Task SendPensi()
        {
            var pensiArray = Directory.GetFiles(_pensiPath);

            var rand = new Random();
            var index = rand.Next(pensiArray.Length);

            await Context.Channel.SendFileAsync(_pensiPath + pensiArray[index], embed: Controller.OgerBot.GetStandardSoundEmbed(), isSpoiler: true);
        }
    }
}
