using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using DiscordOgerBotWeb.Database;
using DiscordOgerBotWeb.Models;
using Microsoft.Extensions.Logging;

namespace DiscordOgerBotWeb.Controller
{
    public static class DataBase
    {
        private static readonly ILogger Logger = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();

        }).CreateLogger("Database Logger");

        private static readonly OgerBotDataBaseContext Context = new OgerBotDataBaseContext();

        public static void StartupDataBase()
        {
            try
            {
                Context.Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex,"Could not Start/Create Database!");
            }
        }

        private static async Task<DiscordUser> GetDiscordUserFromId(ulong userId, ulong? guildId = null)
        {
            try
            {
                var user = await Context.DiscordUsers.FindAsync(userId);

                if (user == null) return null;
                if (guildId == null) return user;

                if (!user.ActiveGuildsId.Contains((ulong) guildId))
                {
                    user.ActiveGuildsId.Add((ulong) guildId);
                }

                Context.DiscordUsers.Update(user);
                await Context.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Could not create User in DB {Environment.NewLine}" +
                                    $"UserId: {userId} {Environment.NewLine}" +
                                    $"Guild: {guildId}");
                throw;
            }
        }

        public static async Task IncreaseInteractionCount(IUser user, SocketCommandContext commandContext)
        {
            try
            {

                var userDataBase = await GetDiscordUserFromId(user.Id, commandContext.Guild.Id);
                if (userDataBase == null)
                {
                    await CreateUser(user, commandContext);
                    userDataBase = await GetDiscordUserFromId(user.Id, commandContext.Guild.Id);
                }

                if (userDataBase == null)
                {
                    Logger.LogWarning($"User not Found! {Environment.NewLine}" +
                                       $"Id: {user.Id} {Environment.NewLine}" +
                                       $"Name: {user.Username} {Environment.NewLine}" +
                                       $"Guild: {commandContext.Guild.Name}");
                    return;
                }

                userDataBase.TimesBotUsed++;

                Context.DiscordUsers.Update(userDataBase);
                await Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Could not Increase User Interaction Count {Environment.NewLine}" +
                                     $"User: {user.Username} {Environment.NewLine}" +
                                     $"Guild: {commandContext.Guild.Name}");
            }
        }

        public static async Task CreateUser(IUser user, SocketCommandContext commandContext)
        {

            try
            {
                await Context.DiscordUsers.AddAsync(new DiscordUser
                {
                    Id = user.Id,
                    Name = user.Username,
                    ActiveGuildsId = new List<ulong>(),
                    TimesBotUsed = 0,
                    TimeSpendWorking = new TimeSpan()
                });


                await Context.SaveChangesAsync();

                var userDb = await Context.DiscordUsers.FindAsync(user.Id);
                userDb.ActiveGuildsId.Add(commandContext.Guild.Id);

                Context.DiscordUsers.Update(userDb);
                await Context.SaveChangesAsync();

                Logger.LogInformation($"Created new User! {Environment.NewLine}" +
                                      $"User: {user.Username} {Environment.NewLine}" +
                                      $"Guild: {commandContext.Guild.Name}");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Could not create User in DB {Environment.NewLine}" +
                                    $"User: {user.Username} {Environment.NewLine}" +
                                    $"Guild: {commandContext.Guild.Name}");
            }
        }
    }
}
