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
        internal static CommandService CommandService { get; private set; }
        internal static List<string> FooterDictionary { get; private set; }
        internal static DiscordSocketClient Client { get; private set; }

        private static DateTime FrauchenTime { get; } = new(2008, 4, 1, 17, 00, 0);
        private static bool FrauchenFlag { get; set; }

        private static DateTime Time1510 { get; } = new(2008, 4, 1, 13, 10, 0);
        private static bool TimeFlag1510 { get; set; }

        private static DateTime SchanzenCountDownTime { get; } = new (2022, 1, 5, 0, 0, 0);

        private static List<string> _oragleList;
        private static IServiceProvider _services;
        private static Dictionary<ulong, ulong> _repliedMessagesId;
        private static Dictionary<ulong, ulong> _oragleMessegesId;
        private static Dictionary<string, List<string>> _translateDictionary;
        private static CancellationTokenSource _cancellationTokenCheckUsers;

        public static async Task StartBot()
        {
            try
            {
                FooterDictionary = ReadDictionaryFromFile(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory,
                    "../DiscordOgerBot/Dict/dictionary_footer.json")))["footer"];

                _oragleList = ReadDictionaryFromFile(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory,
                    "../DiscordOgerBot/Dict/dictionary_Oragle.json")))["oragle"];

                _translateDictionary = ReadDictionaryFromFile(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory,
                    "../DiscordOgerBot/Dict/dictionary_default.json")));

                Client = new DiscordSocketClient(new DiscordSocketConfig {MessageCacheSize = 1000});
                CommandService = new CommandService();

                _services = new ServiceCollection()
                    .AddSingleton(Client)
                    .AddSingleton(CommandService)
                    .BuildServiceProvider();
                _repliedMessagesId = new Dictionary<ulong, ulong>();
                _oragleMessegesId = new Dictionary<ulong, ulong>();

                Client.Log += DiscordClientLogs;
                Client.ReactionAdded += ReactionAdded;
                Client.ReactionRemoved += ReactionRemoved;
                Client.MessageReceived += MessageReceived;
                Client.MessageReceived += ChannelMirror.MessageReceived;
                Client.MessageUpdated += MessageUpdated;
                Client.Ready += ClientReady;

                await Client.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("BOTTOKEN"));
                await Client.StartAsync();
                await CommandService.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
                
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
                var user = Client.GetUser(userId);
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
                switch (reaction.Emote.Name)
                {
                    case "OgerBot":
                        await Translate(message, channel, reaction);
                        break;
                    case "OgerBotOragle":
                        await OgerOragle(message);
                        break;
                    default:
                        return;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Bot could not reply!");
            }
        }

        private static async Task Translate(Cacheable<IUserMessage, ulong> message, ISocketMessageChannel channel, SocketReaction reaction)
        {
            try
            {
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
                    "[DrachenlordKoreaDiscord](https://discord.gg/jNkTrsZvW3)")

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
                Log.Error(ex, nameof(Translate));
            }
        }

        private static async Task OgerOragle(Cacheable<IUserMessage, ulong> message)
        {
            try
            {
                if (_oragleMessegesId.ContainsKey(message.Id)) return;
                var originalMessage = await message.GetOrDownloadAsync();
                if (originalMessage.Author.IsBot) return;

                var rand = new Random();

                var reply = _oragleList[rand.Next(_oragleList.Count)];
                var botMessage = await originalMessage.ReplyAsync(reply);

                _oragleMessegesId.Add(originalMessage.Id, botMessage.Id);
            }
            catch (Exception ex)
            {
                Log.Error(ex, nameof(OgerOragle));
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

        private static async Task MessageReceived(SocketMessage origMessage)
        {
            try
            {
                if (origMessage.Author.IsBot) return;
                DataBase.CreateUser(origMessage.Author);
                TimeManagement.Measure(origMessage.Author.Id);

                if (!(origMessage is SocketUserMessage message)) return;
                var context = new SocketCommandContext(Client, message);

                var argPos = 0;


                if (context.Guild.Id == 758745761566818314)
                {
                    await CheckUser(message.Author as SocketGuildUser);
                }
                
                if (message.HasStringPrefix("og ", ref argPos, StringComparison.OrdinalIgnoreCase))
                {
                    var result = await CommandService.ExecuteAsync(context, argPos, _services);
                    if (result.IsSuccess)
                    {
                        DataBase.IncreaseInteractionCount(message.Author, context.Guild.Id);
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

                var context = new SocketCommandContext(Client, message);
                var argPos = 0;

                if (message.HasStringPrefix("og ", ref argPos, StringComparison.OrdinalIgnoreCase))
                {

                    var result = await CommandService.ExecuteAsync(context, argPos, _services);

                    if (result.IsSuccess)
                    {
                        DataBase.IncreaseInteractionCount(message.Author, context.Guild.Id);
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
                        _ => possibleTranslations[rand.Next(length)], RegexOptions.IgnoreCase);
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
                    "[DrachenlordKoreaDiscord](https://discord.gg/jNkTrsZvW3)")

                .WithFooter(footer =>
                    footer.Text =
                        FooterDictionary[random.Next(FooterDictionary.Count)])
                .WithColor(Color.DarkPurple)
                .WithCurrentTimestamp();

            return embedBuilder.Build();
        }

        public static (IRole, IRole, TimeSpan) GetRoleForTimeSpendWorking(TimeSpan timeSpendWorking)
        {
            var roles = Client.GetGuild(758745761566818314).Roles;
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
                if(user.Id is 386989432148066306 or 218373955658973195 or 722485168723722313) return;
                var server = Client.GetGuild(758745761566818314);
                if (server == null) return;
                var roles = server.Roles;
                var ranks = Globals.WorkingRanks.TimeForRanks;

                var timeFromDb = DataBase.GetTimeSpendWorking(user, server.Id);
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
            var server = Client.GetGuild(758745761566818314);
            if(server == null) return;
            var roles = server.Roles;
            var ranks = Globals.WorkingRanks.TimeForRanks;

            foreach (var user in server.Users)
            {
                try
                {
                    if (user == null) continue;
                    if (user.Id is 218373955658973195 or 722485168723722313) continue;
                    var timeFromDb = DataBase.GetTimeSpendWorking(user, server.Id);
                    if (timeFromDb == new TimeSpan()) continue;
                    var rankUserShouldHave = ranks.FirstOrDefault(rank => timeFromDb >= rank.Time);
                    var roleUserShouldHave = roles.FirstOrDefault(m => m.Id == rankUserShouldHave.RankId);
                    if (roleUserShouldHave == null) continue;

                    if (user.Roles.Any(m => m.Id == roleUserShouldHave.Id)) continue;

                    foreach (var role in user.Roles)
                    {
                        if (ranks.Any(m => m.RankId == role.Id)) await user.RemoveRoleAsync(role);
                    }

                    await user.AddRoleAsync(roleUserShouldHave);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Checking time for User");
                }
            }
        }

        private static async Task ClientReady()
        {
            Log.Information("Bot Started!");
            Log.Information("Setting Game...");
            await Client.SetGameAsync("wer in den Knast muss", type: ActivityType.Watching);

            Log.Information("Starting long running tasks....");
            _cancellationTokenCheckUsers = new CancellationTokenSource();
            new Task(OneMinuteTask, _cancellationTokenCheckUsers.Token, TaskCreationOptions.LongRunning).Start();
            new Task(TwentyMinuteTask, _cancellationTokenCheckUsers.Token, TaskCreationOptions.LongRunning).Start();

            Log.Information("Getting Build-version....");
            var version = Environment.GetEnvironmentVariable("HEROKU_RELEASE_VERSION");
            if (version != null)
            {
                Log.Information("Version: " + version);
                var guild = Client.Guilds.FirstOrDefault(m => m.Id == 758745761566818314);
                if(guild == null) return;

                var role = guild.GetRole(818550591773081632);
                await role.ModifyAsync(props =>
                {
                    props.Name = version;
                });
            }

            Log.Information("Getting Changes...");
            await CheckForNewReleaseNotes();
        }

        private static async void OneMinuteTask()
        {
            while (!_cancellationTokenCheckUsers.Token.IsCancellationRequested)
            {
                await CheckForFrauchenTime();
                await CheckForTime1510();
                await CheckUsers();
                _cancellationTokenCheckUsers.Token.WaitHandle.WaitOne(TimeSpan.FromMinutes(1));
            }
        }

        private static async void TwentyMinuteTask()
        {
            while (!_cancellationTokenCheckUsers.Token.IsCancellationRequested)
            {
                await SetCountDowns();
                _cancellationTokenCheckUsers.Token.WaitHandle.WaitOne(TimeSpan.FromMinutes(20));
            }
        }

        private static async Task SetCountDowns()
        {
            try
            {
                var guild = Client.Guilds.FirstOrDefault(m => m.Id == 758745761566818314);
                if(guild == null) return;
                var channel = (SocketVoiceChannel) guild.GetChannel(905795752360558643);

                var timeLeft = SchanzenCountDownTime - DateTime.Now + TimeSpan.FromHours(2);

                await channel.ModifyAsync(props =>
                {
                    props.Name = $"🏠🔜Auszug: {timeLeft.Days} Days, {timeLeft.Hours}h";
                });
            }
            catch (Exception ex)
            {
                Log.Error(ex, nameof(SetCountDowns));
            }
        }

        private static async Task CheckForFrauchenTime()
        {
            try
            {
                if (DateTime.Now.Hour == FrauchenTime.Hour && !FrauchenFlag)
                {
                    FrauchenFlag = true;
                    var videoPath = Path.GetFullPath(
                        Path.Combine(AppContext.BaseDirectory, "../DiscordOgerBot/Videos"));
                    var channel = (ISocketMessageChannel)Client.GetChannel(759039858319687700);
                    await channel.SendFileAsync(videoPath + "/frauchen.mp4", embed: GetStandardSoundEmbed(), text: "Hier die tägliche Frauchen-Dosis <:Kopfhrer:773464326920077343>");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, nameof(CheckForFrauchenTime));
            }
        }

        private static async Task CheckForTime1510()
        {
            try
            {
                if (DateTime.Now.Hour == Time1510.Hour && DateTime.Now.Minute == Time1510.Minute && !TimeFlag1510)
                {
                    TimeFlag1510 = true;
                    var videoPath = Path.GetFullPath(
                        Path.Combine(AppContext.BaseDirectory, "../DiscordOgerBot/Videos"));
                    var channel = (ISocketMessageChannel)Client.GetChannel(759039858319687700);
                    await channel.SendFileAsync(videoPath + "/1510.mp4", embed: GetStandardSoundEmbed(), text: "15:10 Uhr <:bier:764052806160089138>");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, nameof(CheckForTime1510));
            }
        }

        private static async Task CheckForNewReleaseNotes()
        {
            var updateMessages = await GitHub.GetCommitMessagesSinceLastUpdate();
            if(updateMessages == null || updateMessages.Count == 0) return;

            var random = new Random();
            var embed = new EmbedBuilder
            {
                Title = $"Release Notes {Environment.GetEnvironmentVariable("HEROKU_RELEASE_VERSION")} :metal:",
            };

            foreach (var updateMessage in updateMessages)
            {
                embed.AddField("Commit", updateMessage);
            }

            var buildEmbed = embed

                .AddField("Links",
                    "[Github](https://github.com/MoriPastaPizza/DiscordOgerBotWeb) | " +
                    "[Lade den Bot auf deinen Server ein!](https://discord.com/api/oauth2/authorize?client_id=761895612291350538&permissions=383040&scope=bot) | " +
                    "[DrachenlordKoreaDiscord](https://discord.gg/jNkTrsZvW3)")

                .WithAuthor(Client.CurrentUser)
                .WithFooter(footer =>
                    footer.Text =
                        FooterDictionary[random.Next(FooterDictionary.Count)])
                .WithColor(Color.Gold)
                .WithCurrentTimestamp()
                .Build();
            var channel = (SocketTextChannel) Client.GetChannel(776846121653370921);
            await channel.SendMessageAsync(embed: buildEmbed);
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
