using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.Rest;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Serilog;

namespace DiscordOgerBot.Controller
{
    internal static class OgerBot
    {
        public static CommandService CommandService { get; private set; }
        public static List<string> FooterDictionary { get; private set; }

        private static DiscordSocketClient _client;
        private static IServiceProvider _services;
        private static Dictionary<ulong, ulong> _repliedMessagesId;
        private static Dictionary<string, List<string>> _translateDictionary;
        private static CancellationTokenSource _cancellationTokenCheckUsers;

        public static async Task StartBot()
        {
            try
            {
                FooterDictionary = ReadDictionaryFromFile(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory,
                    "../DiscordOgerBot/Dict/dictionary_footer.json")))["footer"];

                _client = new DiscordSocketClient(new DiscordSocketConfig {MessageCacheSize = 1000});
                CommandService = new CommandService();
                _services = new ServiceCollection()
                    .AddSingleton(_client)
                    .AddSingleton(CommandService)
                    .BuildServiceProvider();
                _translateDictionary = ReadDictionaryFromFile(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory,
                    "../DiscordOgerBot/Dict/dictionary_default.json")));
                _repliedMessagesId = new Dictionary<ulong, ulong>();

                var token = Environment.GetEnvironmentVariable("BOTTOKEN");

                _client.Log += DiscordClientLogs;

                await _client.LoginAsync(TokenType.Bot, token);
                await _client.StartAsync();

                _client.ReactionAdded += ReactionAdded;
                _client.ReactionRemoved += ReactionRemoved;
                _client.MessageReceived += MessageReceived;
                _client.MessageUpdated += MessageUpdated;

                await CommandService.AddModulesAsync(Assembly.GetEntryAssembly(), _services);

                await _client.SetGameAsync("ob Haider vorm Tor stehen", type: ActivityType.Watching);
                Log.Information("Bot Started!");

                _cancellationTokenCheckUsers = new CancellationTokenSource();
                new Task(CheckUsersTask, _cancellationTokenCheckUsers.Token, TaskCreationOptions.LongRunning).Start();

                await Task.Delay(-1);

            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Bot could not start!");
            }
        }

        public static async Task<IUser> GetUserFromId(ulong userId)
        {
            try
            {
                var user = _client.GetUser(userId);
                if (user != null) return user;
                using var restClient = new DiscordRestClient();
                return await restClient.GetUserAsync(userId);

            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Could not Get user from ID {Environment.NewLine}" +
                                 $"User: {userId} {Environment.NewLine}");
                return null;
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

                Log.Information($"Translated Message! {Environment.NewLine}" +
                                   $"Original Author: {originalMessage.Author.Username}, with id: {originalMessage.Author.Id} {Environment.NewLine}" +
                                   $"Original Message: {originalMessage.Content} {Environment.NewLine}" +
                                   $"Original Message Link: {originalMessage.GetJumpUrl()} {Environment.NewLine}" +
                                   $"Translate Request from: with id: {reaction.UserId} {Environment.NewLine}" +
                                   $"Tranlated Message: {botReplyMessage.Content} {Environment.NewLine}" +
                                   $"Translated Message Link: {botReplyMessage.GetJumpUrl()}");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Bot could not reply!");
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

                    Log.Information($"Translated Message deleted! {Environment.NewLine}" +
                                       $"Original Author: {originalMessage.Author.Username}, with id: {originalMessage.Author.Id} {Environment.NewLine}" +
                                       $"Original Message Link: {originalMessage.GetJumpUrl()} {Environment.NewLine}" +
                                       $"Translate Request from: {reaction.User.Value.Username}, with id: {reaction.UserId} {Environment.NewLine}" +
                                       $"Translated Message Link: {botReplyMessage.GetJumpUrl()}");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Bot could not delete message!");
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

                await DataBase.CreateUser(message.Author, context.Guild.Id);

                if (context.Guild.Id == 758745761566818314)
                {
                    await CheckUser(message.Author as SocketGuildUser);
                    TimeManagement.Measure(message.Author.Id);
                }

                if (message.HasStringPrefix("og ", ref argPos, StringComparison.OrdinalIgnoreCase))
                {
                    var result = await CommandService.ExecuteAsync(context, argPos, _services);
                    if (result.IsSuccess)
                    {
                        await DataBase.IncreaseInteractionCount(message.Author, context.Guild.Id);
                        Log.Information($"Command executed! {Environment.NewLine}" +
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

                        Log.Warning($"Command could not be executed! {Environment.NewLine}" +
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
                Log.Error(ex, "Error in recieving message!");
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

                await DataBase.CreateUser(message.Author, context.Guild.Id);

                if (message.HasStringPrefix("og ", ref argPos, StringComparison.OrdinalIgnoreCase))
                {

                    var result = await CommandService.ExecuteAsync(context, argPos, _services);

                    if (result.IsSuccess)
                    {
                        await DataBase.IncreaseInteractionCount(message.Author, context.Guild.Id);
                        Log.Information($"Command executed! {Environment.NewLine}" +
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

                        Log.Warning($"Command could not be executed! {Environment.NewLine}" +
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
                Log.Error(ex, "Error in recieving message!");
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
                Log.Error(ex, "Could not translate the given text!");
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

        public static (IRole, IRole, TimeSpan) GetRoleForTimeSpendWorking(TimeSpan timeSpendWorking)
        {
            var roles = _client.GetGuild(758745761566818314).Roles;
            var ranks = Globals.WorkingRanks.TimeForRanks;

            var currentRank = ranks.FirstOrDefault(rank => timeSpendWorking >= rank.Time);
            var nextRank = ranks.Find(m => m.Rank == currentRank.Rank + 1);

            var currentRole = roles.FirstOrDefault(m => m.Id == currentRank.RankId);
            var nextRole = roles.FirstOrDefault(m => m.Id == nextRank.RankId);

            var timeTilNextRole = nextRank.Time - timeSpendWorking;

            return (currentRole, nextRole, timeTilNextRole);
        }

        private static async Task CheckUser(SocketGuildUser user)
        {
            try
            {
                if(user.Id == 386989432148066306) return;
                var server = _client.GetGuild(758745761566818314);
                if (server == null) return;
                var roles = server.Roles;
                var ranks = Globals.WorkingRanks.TimeForRanks;

                if (user == null) return;
                var timeFromDb = await DataBase.GetTimeSpendWorking(user, server.Id);
                if (timeFromDb == new TimeSpan()) return;
                var rankUserShouldHave = ranks.FirstOrDefault(rank => timeFromDb >= rank.Time);
                var roleUserShouldHave = roles.FirstOrDefault(m => m.Id == rankUserShouldHave.RankId);
                if (roleUserShouldHave == null) return;

                if (user.Roles.Any(m => m.Id == roleUserShouldHave.Id)) return;

                //Log.Information($"User: {user.Username} should have role {roleUserShouldHave.Name}");

                foreach (var role in user.Roles)
                {
                    if (ranks.Any(m => m.RankId == role.Id)) await user.RemoveRoleAsync(role);
                }

                await user.AddRoleAsync(roleUserShouldHave);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "In checkUser");
            }
        }

        public static async Task CheckUsers()
        {
            var server = _client.GetGuild(758745761566818314);
            if(server == null) return;
            var roles = server.Roles;
            var ranks = Globals.WorkingRanks.TimeForRanks;

            foreach (var user in server.Users)
            {
                if (user == null) continue;
                if(user.Id == 386989432148066306) continue;
                var timeFromDb = await DataBase.GetTimeSpendWorking(user, server.Id);
                if (timeFromDb == new TimeSpan()) continue;
                var rankUserShouldHave = ranks.FirstOrDefault(rank => timeFromDb >= rank.Time);
                var roleUserShouldHave = roles.FirstOrDefault(m => m.Id == rankUserShouldHave.RankId);
                if (roleUserShouldHave == null) continue;

                if (user.Roles.Any(m => m.Id == roleUserShouldHave.Id)) continue;

                //Log.Information($"User: {user.Username} should have role {roleUserShouldHave.Name}");

                foreach (var role in user.Roles)
                {
                    if (ranks.Any(m => m.RankId == role.Id)) await user.RemoveRoleAsync(role);
                }

                await user.AddRoleAsync(roleUserShouldHave);
            }
        }

        private static async void CheckUsersTask()
        {
            while (!_cancellationTokenCheckUsers.Token.IsCancellationRequested)
            {
                _cancellationTokenCheckUsers.Token.WaitHandle.WaitOne(TimeSpan.FromMinutes(5));

                await CheckUsers();
            }
        }

        private static Dictionary<string, List<string>> ReadDictionaryFromFile(string path)
        {
            using var r = new StreamReader(path);
            var json = r.ReadToEnd();
            return JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(json);
        }

        private static Task DiscordClientLogs(LogMessage message)
        {
            Log.Information(message.Message);
            return Task.CompletedTask;
        }
    }
}
