using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Design;

namespace DiscordOgerBot.Modules
{
    public class PensiCommands : ModuleBase<SocketCommandContext>
    {
        private readonly string _pensiPath = Path.GetFullPath(
           Path.Combine(AppContext.BaseDirectory, "../DiscordOgerBot/Pensi"));

        [Command("pensi")]
        [Alias("penis","speer")]
        public async Task SendPensi([Remainder] string args = null)
        {
            var pensiArray = Directory.GetFiles(_pensiPath);
            string pensiString = null;

            if (uint.TryParse(args, out var setIndex))
            {
                if (setIndex <= pensiArray.Length)
                {
                    pensiString = Path.GetFileName(pensiArray[setIndex]);
                }
            }
            else
            {
                var rand = new Random();
                var index = rand.Next(pensiArray.Length);
                pensiString = Path.GetFileName(pensiArray[index - 1]);
            }

            if (pensiString == null)
            {
                await Context.Channel.SendMessageAsync(
                    "Das Pensi Bild gibt es nicht <:kuhlschrank:776496613933580298>");
                return;
            }

            await Context.Channel.SendFileAsync($"{_pensiPath}/{pensiString}", embed: Controller.OgerBot.GetStandardSoundEmbed(), isSpoiler: true, text: "Provided by Pensi Providerin Sörbi 🤘");
        }
    }
}
