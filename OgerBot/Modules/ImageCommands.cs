﻿using System;
using System.IO;
using System.Threading.Tasks;
using Discord.Commands;

namespace DiscordOgerBotWeb.Modules
{
    public class ImageCommands : ModuleBase<SocketCommandContext>
    {
        private readonly string _imagePath = Path.GetFullPath(
            Path.Combine(AppContext.BaseDirectory, "../OgerBot/images"));

        [Command("nice")]
        [Alias("nais")]
        public async Task SendNice()
        {
            var rand = new Random();
            await Context.Channel.SendFileAsync(_imagePath + $"/nais{rand.Next(1,4)}.gif", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("sport")]
        [Alias("sportlord")]
        public async Task SendSportLord()
        {
            var rand = new Random();
            await Context.Channel.SendFileAsync(_imagePath + $"/sportLord{rand.Next(1, 6)}.gif", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("buddern")]
        [Alias("brot", "butter", "kochen")]
        public async Task SendBuddern()
        {
            await Context.Channel.SendFileAsync(_imagePath + "/buddern.gif", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }
    }
}