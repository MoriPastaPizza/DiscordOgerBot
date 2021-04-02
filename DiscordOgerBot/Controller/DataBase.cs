using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using DiscordOgerBot.Database;
using DiscordOgerBot.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace DiscordOgerBot.Controller
{
    public static class DataBase
    {
        private static readonly OgerBotDataBaseContext Context = new OgerBotDataBaseContext();

        public static void StartupDataBase()
        {
            try
            {
                Context.Database.Migrate();
            }
            catch (Exception ex)
            {
                Log.Error(ex,"Could not Start/Create Database!");
            }
        }

        private static async Task<DiscordUser> GetDiscordUserFromId(ulong userId, ulong? guildId = null)
        {
            try
            {
                var user = await Context.DiscordUsers.FindAsync(userId.ToString());

                if (user == null) return null;
                if (guildId == null) return user;

                if (!user.ActiveGuildsId.Contains(guildId.ToString()))
                {
                    user.ActiveGuildsId.Add(guildId.ToString());
                }

                Context.DiscordUsers.Update(user);
                await Context.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Could not create User in DB {Environment.NewLine}" +
                                    $"UserId: {userId} {Environment.NewLine}" +
                                    $"Guild: {guildId}");
                throw;
            }
        }

        public static async Task IncreaseInteractionCount(IUser user, ulong guildId)
        {
            try
            {

                var userDataBase = await GetDiscordUserFromId(user.Id, guildId);
                if (userDataBase == null)
                {
                    await CreateUser(user, guildId);
                    userDataBase = await GetDiscordUserFromId(user.Id, guildId);
                }

                if (userDataBase == null)
                {
                    Log.Warning($"User not Found! {Environment.NewLine}" +
                                       $"Id: {user.Id} {Environment.NewLine}" +
                                       $"Name: {user.Username} {Environment.NewLine}" +
                                       $"Guild: {guildId}");
                    return;
                }

                userDataBase.TimesBotUsed++;

                Context.DiscordUsers.Update(userDataBase);
                await Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Could not Increase User Interaction Count {Environment.NewLine}" +
                                 $"User: {user.Username} {Environment.NewLine}" +
                                 $"Guild: {guildId}");
            }
        }

        public static async Task<uint> GetTimesBotUsed(IUser user, ulong guildId)
        {
            try
            {
                var userDataBase = await GetDiscordUserFromId(user.Id, guildId);
                if (userDataBase == null)
                {
                    await CreateUser(user, guildId);
                    userDataBase = await GetDiscordUserFromId(user.Id, guildId);
                }

                return userDataBase.TimesBotUsed;

            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Could not get the Bot Used Count {Environment.NewLine}" +
                                 $"User: {user.Username} {Environment.NewLine}" +
                                 $"Guild: {guildId}");
                return 0;
            }
        }

        public static async Task<TimeSpan> GetTimeSpendWorking(IUser user, ulong guildId)
        {
            try
            {
                var userDataBase = await GetDiscordUserFromId(user.Id, guildId);
                if (userDataBase == null)
                {
                    await CreateUser(user, guildId);
                    userDataBase = await GetDiscordUserFromId(user.Id, guildId);
                }

                return userDataBase.TimeSpendWorking;

            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Could not get the Working Time {Environment.NewLine}" +
                                 $"User: {user.Username} {Environment.NewLine}" +
                                 $"Guild: {guildId}");
                throw;
            }
        }

        public static async Task IncreaseTimeSpendWorking(ulong userId, TimeSpan additionalTime)
        {
            try
            {

                var userDataBase = await GetDiscordUserFromId(userId);
                if (userDataBase == null)
                {
                    return;
                }

                userDataBase.TimeSpendWorking += additionalTime;

                Context.DiscordUsers.Update(userDataBase);
                await Context.SaveChangesAsync();

                Log.Information($"Added time to user {userDataBase.Name}, Time: {additionalTime}");
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Could not Increase the working hours {Environment.NewLine}" +
                                 $"User: {userId} {Environment.NewLine}");
            }
        }

        public static async Task IncreaseQuizPointsTotal(ulong userId, int pointsToAdd)
        {
            try
            {

                var userDataBase = await GetDiscordUserFromId(userId);
                if (userDataBase == null)
                {
                    return;
                }

                userDataBase.QuizPointsTotal += pointsToAdd;

                Context.DiscordUsers.Update(userDataBase);
                await Context.SaveChangesAsync();

                Log.Information($"Added Total Point to user {userDataBase.Name}");
            }
            catch (Exception ex)
            {
                Log.Error(ex, nameof(IncreaseQuizPointsTotal));
            }
        }

        public static async Task IncreaseQuizWonTotal(ulong userId)
        {
            try
            {

                var userDataBase = await GetDiscordUserFromId(userId);
                if (userDataBase == null)
                {
                    return;
                }

                userDataBase.QuizWonTotal++;

                Context.DiscordUsers.Update(userDataBase);
                await Context.SaveChangesAsync();

                Log.Information($"Added Total Wins to user {userDataBase.Name}");
            }
            catch (Exception ex)
            {
                Log.Error(ex, nameof(IncreaseQuizWonTotal));
            }
        }

        public static async Task<int> GetTimesQuizWonTotal(ulong userId)
        {
            try
            {
                var userDataBase = await GetDiscordUserFromId(userId);
                return userDataBase?.QuizWonTotal ?? 0;
            }
            catch (Exception ex)
            {
                Log.Error(ex, nameof(GetTimesQuizWonTotal));
                return 0;
            }
        }

        public static async Task<int> GetQuizPointsTotal(ulong userId)
        {
            try
            {
                var userDataBase = await GetDiscordUserFromId(userId);
                return userDataBase?.QuizPointsTotal ?? 0;
            }
            catch (Exception ex)
            {
                Log.Error(ex, nameof(GetQuizPointsTotal));
                return 0;
            }
        }

        public static async Task DecreaseTimeSpendWorking(ulong userId, TimeSpan additionalTime)
        {
            try
            {

                var userDataBase = await GetDiscordUserFromId(userId);
                if (userDataBase == null)
                {
                    return;
                }

                userDataBase.TimeSpendWorking -= additionalTime;

                Context.DiscordUsers.Update(userDataBase);
                await Context.SaveChangesAsync();

                Log.Information($"Removed time from user {userDataBase.Name}, Time: {additionalTime}");
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Could not Increase the working hours {Environment.NewLine}" +
                              $"User: {userId} {Environment.NewLine}");
            }
        }

        public static async Task CreateUser(IUser user, ulong guildId)
        {

            try
            {

                if(await AsyncEnumerable.AnyAsync(Context.DiscordUsers, m => m.Id == user.Id.ToString())) return;

                await Context.DiscordUsers.AddRangeAsync(new DiscordUser
                {
                    Id = user.Id.ToString(),
                    Name = user.Username,
                    ActiveGuildsId = new List<string>{ guildId.ToString()},
                    TimesBotUsed = 0,
                    TimeSpendWorking = new TimeSpan()
                });


                await Context.SaveChangesAsync();

                Log.Information($"Created new User! {Environment.NewLine}" +
                                   $"User: {user.Username} {Environment.NewLine}" +
                                   $"Guild: {guildId}");
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Could not create User in DB {Environment.NewLine}" +
                                 $"User: {user.Username} {Environment.NewLine}" +
                                 $"Guild: {guildId}");
            }
        }

        internal static List<DiscordUser> GetAllUsers()
        {
            try
            {
                return Context.DiscordUsers.ToList();
            }
            catch (Exception ex)
            {
                Log.Error(ex, nameof(GetAllUsers));
                return new List<DiscordUser>();
            }
        }
    }
}
