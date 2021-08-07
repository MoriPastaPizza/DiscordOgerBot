using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace DiscordOgerBot.Modules
{
    public class GenericCommands : ModuleBase<SocketCommandContext>
    {

        private readonly Random _rand = new();

        [Command("help")]
        public async Task SendHelp()
        {

            var embed = new EmbedBuilder
            {
                Title = "Meddl Leudde!:metal:"
            };
            var buildEmbed = embed

                .WithDescription(
                    "Ich versuche deutsche Sätze ins Meddlfrängische zu übersetzen! " + Environment.NewLine +
                    "Eine Discord Nachricht auf Hochdeutsch nervt dich? Kein Problem! **reagiere einfach auf die Nachricht mit :OgerBot: **" +
                    Environment.NewLine +
                    Environment.NewLine +
                    "**Der Server braucht ein Emoji mit dem Namen OgerBot!!** (Wende dich an die Server Admins die wissen das ganz bestimmt)" +
                    Environment.NewLine +
                    Environment.NewLine +
                    "Die Nachricht kann auch wieder gelöscht werden indem man die Reaction wieder entfernt")

                .AddField("Sounds",
                    "Ich kann auch Sounds abspielen! Für eine Übersicht schreib einfach **og commands**")

                .AddField("Willst du mich auf deinem eigenen Discord Server?",
                    "Das kannst du ganz einfach [hier](https://discord.com/api/oauth2/authorize?client_id=761895612291350538&permissions=383040&scope=bot) machen!")

                .AddField("Weitere Hilfe",
                    "Sollte ich mal nicht richtig funktionieren, komm ins [DrachenlordKoreaDiscord](https://discord.gg/jNkTrsZvW3) oder wende dich bitte an meinen Erbauer:" +
                    Environment.NewLine +
                    "[Discord](https://discordapp.com/users/386989432148066306) | [Reddit](https://www.reddit.com/user/MoriPastaPizza)")

                .AddField("Links",
                    "[Github](https://github.com/MoriPastaPizza/DiscordOgerBotWeb) | " +
                    "[Lade den Bot auf deinen Server ein!](https://discord.com/api/oauth2/authorize?client_id=761895612291350538&permissions=383040&scope=bot) | " +
                    "[DrachenlordKoreaDiscord](https://discord.gg/jNkTrsZvW3)")

                .WithAuthor(Context.Client.CurrentUser)
                .WithFooter(footer =>
                    footer.Text =
                        Controller.OgerBot.FooterDictionary[_rand.Next(Controller.OgerBot.FooterDictionary.Count)])
                .WithColor(Color.Blue)
                .WithCurrentTimestamp()
                .Build();

            await Context.Channel.SendMessageAsync(embed: buildEmbed);
        }

        [Command("commands")]
        public async Task SendCommands()
        {
            var module = Controller.OgerBot.CommandService.Modules
                .FirstOrDefault(m => m.Name == nameof(GenericCommands));

            if (module == null) return;

            var commands = module.Commands
                .OrderBy(m => m.Name)
                .ToList();

            var commandList = commands
                .Select(command => $"**{command.Aliases.Aggregate((i, j) => i + " " + j)}** {command.Summary}")
                .ToList();
            var fieldString = commandList.Aggregate((i, j) => i + " | " + j).ToString();

            var embedBuilder = new EmbedBuilder
            {
                Title = "Commands"
            };

            embedBuilder
                .WithDescription(fieldString)
                .AddField("Links",
                    "[Github](https://github.com/MoriPastaPizza/DiscordOgerBotWeb) | " +
                    "[Lade den Bot auf deinen Server ein!](https://discord.com/api/oauth2/authorize?client_id=761895612291350538&permissions=383040&scope=bot) | " +
                    "[DrachenlordKoreaDiscord](https://discord.gg/jNkTrsZvW3)")
                .WithAuthor(Context.Client.CurrentUser)
                .WithFooter(footer =>
                    footer.Text =
                        Controller.OgerBot.FooterDictionary[_rand.Next(Controller.OgerBot.FooterDictionary.Count)])
                .WithColor(Color.Red)
                .WithCurrentTimestamp();

            await Context.Channel.SendMessageAsync(embed: embedBuilder.Build());
        }

        [Command("sounds")]
        public async Task SendSounds()
        {
            var module = Controller.OgerBot.CommandService.Modules
                .FirstOrDefault(m => m.Name == nameof(SoundCommands));

            if (module == null) return;

            var commands = module.Commands
                .Where(m => m.Name != "asia")
                .OrderBy(m => m.Name)
                .ToList();

            var commandList = commands
                .Select(command => $"**{command.Aliases.Aggregate((i, j) => i + " " + j)}** {command.Summary}")
                .ToList();
            var fieldString = commandList.Aggregate((i, j) => i + " | " + j).ToString();

            var embedBuilder = new EmbedBuilder
            {
                Title = "Sound Commands"
            };

            embedBuilder
                .WithDescription(fieldString)
                .AddField("Links",
                    "[Github](https://github.com/MoriPastaPizza/DiscordOgerBotWeb) | " +
                    "[Lade den Bot auf deinen Server ein!](https://discord.com/api/oauth2/authorize?client_id=761895612291350538&permissions=383040&scope=bot) | " +
                    "[DrachenlordKoreaDiscord](https://discord.gg/jNkTrsZvW3)")
                .WithAuthor(Context.Client.CurrentUser)
                .WithFooter(footer =>
                    footer.Text =
                        Controller.OgerBot.FooterDictionary[_rand.Next(Controller.OgerBot.FooterDictionary.Count)])
                .WithColor(Color.Red)
                .WithCurrentTimestamp();

            await Context.Channel.SendMessageAsync(embed: embedBuilder.Build());

        }

        [Command("videos")]
        public async Task SendVideos()
        {
            var module = Controller.OgerBot.CommandService.Modules
                .FirstOrDefault(m => m.Name == nameof(VideoCommands));

            if (module == null) return;

            var commands = module.Commands
                .Where(m => m.Name != "oof")
                .OrderBy(m => m.Name)
                .ToList();

            var commandList = commands
                .Select(command => $"**{command.Aliases.Aggregate((i, j) => i + " " + j)}** {command.Summary}")
                .ToList();
            var fieldString = commandList.Aggregate((i, j) => i + " | " + j).ToString();

            var embedBuilder = new EmbedBuilder
            {
                Title = "Video Commands"
            };

            embedBuilder
                .WithDescription(fieldString)
                .AddField("Links",
                    "[Github](https://github.com/MoriPastaPizza/DiscordOgerBotWeb) | " +
                    "[Lade den Bot auf deinen Server ein!](https://discord.com/api/oauth2/authorize?client_id=761895612291350538&permissions=383040&scope=bot) | " +
                    "[DrachenlordKoreaDiscord](https://discord.gg/jNkTrsZvW3)")
                .WithAuthor(Context.Client.CurrentUser)
                .WithFooter(footer =>
                    footer.Text =
                        Controller.OgerBot.FooterDictionary[_rand.Next(Controller.OgerBot.FooterDictionary.Count)])
                .WithColor(Color.Red)
                .WithCurrentTimestamp();

            await Context.Channel.SendMessageAsync(embed: embedBuilder.Build());

        }

        [Command("images")]
        public async Task SendImages()
        {
            var module = Controller.OgerBot.CommandService.Modules
                .FirstOrDefault(m => m.Name == nameof(ImageCommands));

            if (module == null) return;

            var commands = module.Commands
                .OrderBy(m => m.Name)
                .ToList();

            var commandList = commands
                .Select(command => $"**{command.Aliases.Aggregate((i, j) => i + " " + j)}** {command.Summary}")
                .ToList();
            var fieldString = commandList.Aggregate((i, j) => i + " | " + j).ToString();

            var embedBuilder = new EmbedBuilder
            {
                Title = "Image Commands"
            };

            embedBuilder
                .WithDescription(fieldString)
                .AddField("Links",
                    "[Github](https://github.com/MoriPastaPizza/DiscordOgerBotWeb) | " +
                    "[Lade den Bot auf deinen Server ein!](https://discord.com/api/oauth2/authorize?client_id=761895612291350538&permissions=383040&scope=bot) | " +
                    "[DrachenlordKoreaDiscord](https://discord.gg/jNkTrsZvW3)")
                .WithAuthor(Context.Client.CurrentUser)
                .WithFooter(footer =>
                    footer.Text =
                        Controller.OgerBot.FooterDictionary[_rand.Next(Controller.OgerBot.FooterDictionary.Count)])
                .WithColor(Color.Red)
                .WithCurrentTimestamp();

            await Context.Channel.SendMessageAsync(embed: embedBuilder.Build());

        }
    }
}

