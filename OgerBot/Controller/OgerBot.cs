using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DiscordOgerBotWeb.Controller
{
    internal static class OgerBot
    {
        public static CommandService CommandService { get; private set; }
        public static List<string> FooterDictionary { get; private set; }

        private static DiscordSocketClient _client;
        private static IServiceProvider _services;
        private static Dictionary<ulong, ulong> _repliedMessagesId;
        private static Dictionary<string, List<string>> _translateDictionary;
        private static ILogger _logger;

        public static async Task StartBot()
        {

            // Start Logger
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });
            _logger = loggerFactory.CreateLogger("Oger Bot");

            try
            {
                FooterDictionary = ReadDictionaryFromFile(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory,
                    "../OgerBot/dict/dictionary_footer.json")))["footer"];

                _client = new DiscordSocketClient(new DiscordSocketConfig {MessageCacheSize = 100});
                CommandService = new CommandService();
                _services = new ServiceCollection()
                    .AddSingleton(_client)
                    .AddSingleton(CommandService)
                    .BuildServiceProvider();
                _translateDictionary = ReadDictionaryFromFile(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory,
                    "../OgerBot/dict/dictionary_default.json")));
                _repliedMessagesId = new Dictionary<ulong, ulong>();

                var token = Environment.GetEnvironmentVariable("BOTTOKEN");

                await _client.LoginAsync(TokenType.Bot, token);
                await _client.StartAsync();

                _client.ReactionAdded += ReactionAdded;
                _client.ReactionRemoved += ReactionRemoved;
                _client.MessageReceived += MessageReceived;
                _client.MessageUpdated += MessageUpdated;

                await CommandService.AddModulesAsync(Assembly.GetEntryAssembly(), _services);

                _client.Ready += () =>
                {
                    _client.SetGameAsync("Schmeißt grad die Brügel Naus 👊", streamUrl: null, ActivityType.CustomStatus);
                    _logger.LogInformation("Bot Started!");
                    return Task.CompletedTask;
                };
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Bot could not start!");
                await StartBot();
            }
        }

        private static async Task ReactionAdded(Cacheable<IUserMessage, ulong> message, ISocketMessageChannel channel, SocketReaction reaction)
        {
            try
            {
                if (reaction.Emote.Name != "OgerBot") return;
                if (_repliedMessagesId.ContainsKey(message.Id)) return;

                var originalMessage = await message.GetOrDownloadAsync();
                if (originalMessage.Author.IsBot) return;

                var context = new SocketCommandContext(_client, originalMessage as SocketUserMessage);
                var dataBaseTask = DataBase.IncreaseInteractionCount(_client.GetUser(reaction.UserId), context);

                var translatedMessage = TranslateToOger(originalMessage);

                var random = new Random();
                var embedBuilder = new EmbedBuilder();

                embedBuilder

                .AddField("Was ist passiert?", "Schreib einfach: \"og help\"")

                .AddField("Links",
                    "[Github](https://github.com/MoriPastaPizza/DiscordOgerBotWeb) | " +
                    "[Lade den Bot auf deinen Server ein!](https://discord.com/api/oauth2/authorize?client_id=761895612291350538&permissions=383040&scope=bot) | " +
                    "[DrachenlordKoreaDiscord](https://discord.gg/MmWQ5pCsHa)")

                .WithDescription(translatedMessage)
                .WithAuthor(originalMessage.Author)
                .WithFooter(footer =>
                    footer.Text =
                        FooterDictionary[random.Next(FooterDictionary.Count)])
                .WithColor(Color.DarkPurple)
                .WithCurrentTimestamp();



                var botReplyMessage = await channel.SendMessageAsync(embed: embedBuilder.Build());

                _repliedMessagesId.Add(originalMessage.Id, botReplyMessage.Id);

                _logger.LogInformation($"Translated Message! {Environment.NewLine}" +
                                       $"Original Author: {originalMessage.Author.Username}, with id: {originalMessage.Author.Id} {Environment.NewLine}" +
                                       $"Original Message: {originalMessage.Content} {Environment.NewLine}" +
                                       $"Original Message Link: {originalMessage.GetJumpUrl()} {Environment.NewLine}" +
                                       $"Translate Request from: with id: {reaction.UserId} {Environment.NewLine}" +
                                       $"Tranlated Message: {botReplyMessage.Content} {Environment.NewLine}" +
                                       $"Translated Message Link: {botReplyMessage.GetJumpUrl()}");

                await dataBaseTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bot could not reply!");
            }
        }

        private static async Task ReactionRemoved(Cacheable<IUserMessage, ulong> message, ISocketMessageChannel channel, SocketReaction reaction)
        {
            try
            {
                if (reaction.Emote.Name != "OgerBot") return;
                var originalMessage = await message.GetOrDownloadAsync();

                foreach (var (emote, metadata) in originalMessage.Reactions)
                {
                    if (emote.Name == "OgerBot" && metadata.ReactionCount >= 1) return;
                }

                if (_repliedMessagesId.TryGetValue(originalMessage.Id, out var botReplyId))
                {
                    var botReplyMessage = await channel.GetMessageAsync(botReplyId);
                    await botReplyMessage.DeleteAsync();
                    _repliedMessagesId.Remove(originalMessage.Id);

                    _logger.LogInformation($"Translated Message deleted! {Environment.NewLine}" +
                                    $"Original Author: {originalMessage.Author.Username}, with id: {originalMessage.Author.Id} {Environment.NewLine}" +
                                    $"Original Message Link: {originalMessage.GetJumpUrl()} {Environment.NewLine}" +
                                    $"Translate Request from: {reaction.User.Value.Username}, with id: {reaction.UserId} {Environment.NewLine}" +
                                    $"Translated Message Link: {botReplyMessage.GetJumpUrl()}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bot could not delete message!");
            }
        }

        private static async Task MessageReceived(SocketMessage arg)
        {
            try
            {
                if (!(arg is SocketUserMessage message)) return;
                if (message.Author.IsBot) return;

                var context = new SocketCommandContext(_client, message);
                var argPos = 0;

                if (message.HasStringPrefix("og ", ref argPos, StringComparison.OrdinalIgnoreCase))
                {
                    var result = await CommandService.ExecuteAsync(context, argPos, _services);
                    if (result.IsSuccess)
                    {
                        await DataBase.IncreaseInteractionCount(message.Author, context);
                        _logger.LogInformation($"Command executed! {Environment.NewLine}" +
                                               $"Command from : {message.Author.Username}, with id: {message.Author.Id} {Environment.NewLine}" +
                                               $"Command: {message.Content} {Environment.NewLine}" +
                                               $"Command Link: {message.GetJumpUrl()}");
                    }
                    else
                    {

                        if (result.ErrorReason.Contains("50013"))
                        {
                            await message.Author.SendMessageAsync(
                                "Meddl du Kaschber! Du hast gerade versucht mich zu benutzen, aber ich kann leider nicht antworten 😔" +
                                Environment.NewLine +
                                "Gib mir in dem Channel doch bitte die Rechte, oder frag die Server Admins/Mods" +
                                Environment.NewLine +
                                "Am besten macht Ihr gleiche eine Bot-Rolle für alle euere Bots. Meddl off 🤘");
                        }

                        if (result.Error == CommandError.UnknownCommand)
                        {
                            await message.Channel.SendMessageAsync($"{message.Author.Mention} Das Kommando kenn ich net. Und ich hab nen hohen IQ in Kommandos! {Environment.NewLine}" +
                                                                   "Schau doch mal hier nach: og commands");
                        }

                        _logger.LogWarning($"Command could not be executed! {Environment.NewLine}" +
                                           $"Command from : {message.Author.Username}, with id: {message.Author.Id} {Environment.NewLine}" +
                                           $"Command: {message.Content} {Environment.NewLine}" +
                                           $"Command Link: {message.GetJumpUrl()} {Environment.NewLine}" +
                                           $"Error Reason: {result.ErrorReason} {Environment.NewLine}" +
                                           $"Error: {result.Error}");
                    }
                }

                else
                {
                    await CheckIfMeddlWasRecieved(message);
                    await CheckIfVallahWasRecieved(message, context);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in recieving message!");
            }
        }

        private static async Task MessageUpdated(Cacheable<IMessage, ulong> arg1, SocketMessage arg2, ISocketMessageChannel arg3)
        {
            try
            {
                if (!(arg2 is SocketUserMessage message)) return;
                if (message.Author.IsBot) return;

                var context = new SocketCommandContext(_client, message);
                var argPos = 0;

                if (message.HasStringPrefix("og ", ref argPos, StringComparison.OrdinalIgnoreCase))
                {

                    var result = await CommandService.ExecuteAsync(context, argPos, _services);

                    if (result.IsSuccess)
                    {
                        await DataBase.IncreaseInteractionCount(message.Author, context);
                        _logger.LogInformation($"Command executed! {Environment.NewLine}" +
                                               $"Command from : {message.Author.Username}, with id: {message.Author.Id} {Environment.NewLine}" +
                                               $"Command: {message.Content} {Environment.NewLine}" +
                                               $"Command Link: {message.GetJumpUrl()}");
                    }
                    else
                    {
                        if (result.ErrorReason.Contains("50013"))
                        {
                            await message.Author.SendMessageAsync(
                                "Meddl du Kaschber! Du hast gerade versucht mich zu benutzen, aber ich kann leider nicht antworten 😔" +
                                Environment.NewLine +
                                "Gib mir in dem Channel doch bitte die Rechte, oder frag die Server Admins/Mods" +
                                Environment.NewLine +
                                "Am besten macht Ihr gleiche eine Bot-Rolle für alle euere Bots. Meddl off 🤘");
                        }

                        _logger.LogWarning($"Command could not be executed! {Environment.NewLine}" +
                                           $"Command from : {message.Author.Username}, with id: {message.Author.Id} {Environment.NewLine}" +
                                           $"Command: {message.Content} {Environment.NewLine}" +
                                           $"Command Link: {message.GetJumpUrl()} {Environment.NewLine}" +
                                           $"Error Reason: {result.ErrorReason} {Environment.NewLine}" +
                                           $"Error: {result.Error}");
                    }
                }

                else
                {
                    await CheckIfMeddlWasRecieved(message);
                    await CheckIfVallahWasRecieved(message, context);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in recieving message!");
            }
        }

        private static async Task CheckIfMeddlWasRecieved(IMessage message)
        {
            if (message.Content.Contains("meddl", StringComparison.OrdinalIgnoreCase) && message.Content.Length <= 10)
            {
                if (message.Content.Contains("meddl off", StringComparison.OrdinalIgnoreCase))
                {
                    await message.Channel.SendMessageAsync($"{message.Author.Mention} Meddl off du Kaschber 🤘");
                }
                else if (message.Content.Contains("meddlmoin", StringComparison.OrdinalIgnoreCase))
                {
                    await message.Channel.SendMessageAsync($"{message.Author.Mention} Meddlmoin du Kaschber 🤘");
                }
                else
                {
                    await message.Channel.SendMessageAsync($"{message.Author.Mention} Meddl🤘");
                }
            }
        }

        private static async Task CheckIfVallahWasRecieved(IMessage message, SocketCommandContext context)
        {
            if (message.Content.Contains("Valar morghulis", StringComparison.OrdinalIgnoreCase))
            {
                if (!(message.Author is SocketGuildUser author)) return;
                var role = context.Guild.GetRole(784512104459272253);
                await author.AddRoleAsync(role);
                await author.SendMessageAsync("Valar dohaeris");
            }
        }

        private static string TranslateToOger(IMessage originalMessage)
        {

            var rand = new Random();
            var newMessage = originalMessage.Content;

            try
            {
                foreach (var (wordToTranslate, possibleTranslations) in _translateDictionary)
                {
                    var length = possibleTranslations.Count;
                    var pattern = Regex.Escape(wordToTranslate);

                    newMessage = Regex.Replace(newMessage, pattern,
                        m => possibleTranslations[rand.Next(length)], RegexOptions.IgnoreCase);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Could not translate the given text!");
            }

            return newMessage;
        }

        public static Embed GetStandardSoundEmbed()
        {
            var random = new Random();
            var embedBuilder = new EmbedBuilder();

            embedBuilder
                .WithDescription(
                    "[Github](https://github.com/MoriPastaPizza/DiscordOgerBotWeb) | " +
                    "[Lade den Bot auf deinen Server ein!](https://discord.com/api/oauth2/authorize?client_id=761895612291350538&permissions=383040&scope=bot) | " +
                    "[DrachenlordKoreaDiscord](https://discord.gg/MmWQ5pCsHa)")

                .WithFooter(footer =>
                    footer.Text =
                        FooterDictionary[random.Next(FooterDictionary.Count)])
                .WithColor(Color.DarkPurple)
                .WithCurrentTimestamp();

            return embedBuilder.Build();
        }

        private static Dictionary<string, List<string>> ReadDictionaryFromFile(string path)
        {
            using var r = new StreamReader(path);
            var json = r.ReadToEnd();
            return JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(json);
        }
    }
}
