﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;

namespace DiscordOgerBotWeb.Modules
{
    public class VideoCommands : ModuleBase<SocketCommandContext>
    {
        private readonly string _videoPath = Path.GetFullPath(
            Path.Combine(AppContext.BaseDirectory, "../OgerBot/videos"));

        [Command("kindergarten")]
        public async Task SendKinderGarten()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/kindergarten.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("asylanten")]
        [Alias("asylant")]
        public async Task SendAsylanten()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/asylanten.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("alexa")]
        [Alias("Alegser", "Aleggser", "Aleggsa", "Alegsa")]
        [Summary("ein zufälliges Alexa Video")]
        public async Task SendAlexa()
        {
            var number = new Random().Next(1, 7);

            await Context.Channel.SendFileAsync(_videoPath + $"/alexa{number}.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("marzipan")]
        public async Task SendMarzipan()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/marzipan.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("frauchen")]
        [Alias("16h")]
        public async Task SendFrauchen()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/frauchen.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("mallah")]
        public async Task SendMallah()
        {
            var number = new Random().Next(1, 8);

            await Context.Channel.SendFileAsync(_videoPath + $"/mallah{number}.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("weltne")]
        [Alias("welten")]
        public async Task SendWelten()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/weltne.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("1900")]
        public async Task Send1900()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/1900.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("rettich")]
        public async Task SendRettich()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/rettich.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("haggebudne")]
        [Alias("hagebudne")]
        public async Task SendHagebudne()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/haggebudne.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("sohn")]
        public async Task SendSohn()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/sohn.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("xbuddn")]
        public async Task SendXBuddn()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/xbuddn.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("nixmehr")]
        [Alias("morgen")]
        public async Task SendNixMehr()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/MorgenIsNixMehr.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }

        [Command("wiggst")]
        [Alias("wichst", "wixt", "wixxt", "wigst")]
        public async Task SendWiggstIhrNet()
        {
            await Context.Channel.SendFileAsync(_videoPath + "/wiggstIhrNed.mp4", embed: Controller.OgerBot.GetStandardSoundEmbed());
        }
    }
}