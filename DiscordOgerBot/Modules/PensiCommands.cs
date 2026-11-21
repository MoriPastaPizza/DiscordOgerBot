using Discord.Commands;

namespace DiscordOgerBot.Modules
{
    public class PensiCommands : ModuleBase<SocketCommandContext>
    {
        private const string PensiPath = "./Pensi";

        [Command("pensi")]
        [Alias("penis","speer")]
        public async Task SendPensi([Remainder] string args = null)
        {
            var pensiArray = Directory.GetFiles(PensiPath);
            string pensiString = null;

            if (args == null)
            {
                var rand = new Random();
                var index = rand.Next(pensiArray.Length);
                pensiString = Path.GetFileName(pensiArray[index - 1]);
                await Context.Channel.SendFileAsync($"{PensiPath}/{pensiString}", embed: Controller.OgerBot.GetStandardSoundEmbed(), isSpoiler: true, text: "Provided by Pensi Providerin Sörbi 🤘");
                return;
            }

            if (uint.TryParse(args, out var setIndex))
            {
                if (setIndex <= pensiArray.Length)
                {
                    pensiString = Path.GetFileName(pensiArray[setIndex - 1]);
                }
            }
            else
            {
                return;
            }

            if (pensiString == null)
            {
                await Context.Channel.SendMessageAsync(
                    "Das Pensi Bild gibt es nicht <:kuhlschrank:776496613933580298>");
                return;
            }

            await Context.Channel.SendFileAsync($"{PensiPath}/{pensiString}", embed: Controller.OgerBot.GetStandardSoundEmbed(), isSpoiler: true, text: "Provided by Pensi Providerin Sörbi 🤘");
        }
    }
}
