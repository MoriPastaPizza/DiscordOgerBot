using Discord.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
            await Context.Channel.SendFileAsync(_pensiPath + "/1jJ77goh.jpg", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }
    }
}
