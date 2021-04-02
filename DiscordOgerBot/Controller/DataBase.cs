using System;
using System.Collections.Generic;
using System.Linq;
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
                lock (Context)
                {
                    Context.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex,"Could not Start/Create Database!");
            }
        }

        private static DiscordUser GetDiscordUserFromId(ulong userId, ulong? guildId = null)
        {
            try
            {
                lock (Context)
                {
                    var user = Context.DiscordUsers.Find(userId.ToString());

                    if (user == null) return null;
                    if (guildId == null) return user;

                    if (!user.ActiveGuildsId.Contains(guildId.ToString()))
                    {
                        user.ActiveGuildsId.Add(guildId.ToString());
                    }

                    Context.DiscordUsers.Update(user);
                    Context.SaveChanges();
                    return user;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Could not create User in DB {Environment.NewLine}" +
                                    $"UserId: {userId} {Environment.NewLine}" +
                                    $"Guild: {guildId}");
                throw;
            }
        }

        public static void IncreaseInteractionCount(IUser user, ulong guildId)
        {
            try
            {

                var userDataBase = GetDiscordUserFromId(user.Id, guildId);
                if (userDataBase == null)
                {
                    CreateUser(user, guildId);
                    userDataBase = GetDiscordUserFromId(user.Id, guildId);
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

                lock (Context)
                {
                    Context.DiscordUsers.Update(userDataBase);
                    Context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Could not Increase User Interaction Count {Environment.NewLine}" +
                                 $"User: {user.Username} {Environment.NewLine}" +
                                 $"Guild: {guildId}");
            }
        }

        public static uint GetTimesBotUsed(IUser user, ulong guildId)
        {
            try
            {

                var userDataBase = GetDiscordUserFromId(user.Id, guildId);
                if (userDataBase != null) return userDataBase.TimesBotUsed;
                CreateUser(user, guildId);
                userDataBase = GetDiscordUserFromId(user.Id, guildId);

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

        public static TimeSpan GetTimeSpendWorking(IUser user, ulong guildId)
        {
            try
            {
                var userDataBase = GetDiscordUserFromId(user.Id, guildId);
                if (userDataBase != null) return userDataBase.TimeSpendWorking;
                CreateUser(user, guildId);
                userDataBase = GetDiscordUserFromId(user.Id, guildId);

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

        public static void IncreaseTimeSpendWorking(ulong userId, TimeSpan additionalTime)
        {
            try
            {
                var userDataBase = GetDiscordUserFromId(userId);
                if (userDataBase == null)
                {
                    return;
                }

                userDataBase.TimeSpendWorking += additionalTime;

                lock (Context)
                {
                    Context.DiscordUsers.Update(userDataBase);
                    Context.SaveChanges();
                }


                Log.Information($"Added time to user {userDataBase.Name}, Time: {additionalTime}");

            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Could not Increase the working hours {Environment.NewLine}" +
                                 $"User: {userId} {Environment.NewLine}");
            }
        }

        public static void IncreaseQuizPointsTotal(ulong userId, int pointsToAdd)
        {
            try
            {
                var userDataBase = GetDiscordUserFromId(userId);
                if (userDataBase == null)
                {
                    return;
                }

                userDataBase.QuizPointsTotal += pointsToAdd;

                lock (Context)
                {
                    Context.DiscordUsers.Update(userDataBase);
                    Context.SaveChanges();
                }

                Log.Information($"Added Total Point to user {userDataBase.Name}");
            }
            catch (Exception ex)
            {
                Log.Error(ex, nameof(IncreaseQuizPointsTotal));
            }
        }

        public static void IncreaseQuizWonTotal(ulong userId)
        {
            try
            {
                var userDataBase = GetDiscordUserFromId(userId);
                if (userDataBase == null)
                {
                    return;
                }

                userDataBase.QuizWonTotal++;

                lock (Context)
                {
                    Context.DiscordUsers.Update(userDataBase);
                    Context.SaveChanges();
                }

                Log.Information($"Added Total Wins to user {userDataBase.Name}");
            }
            catch (Exception ex)
            {
                Log.Error(ex, nameof(IncreaseQuizWonTotal));
            }
        }

        public static int GetTimesQuizWonTotal(ulong userId)
        {
            try
            {
                var userDataBase = GetDiscordUserFromId(userId);
                return userDataBase?.QuizWonTotal ?? 0;
            }
            catch (Exception ex)
            {
                Log.Error(ex, nameof(GetTimesQuizWonTotal));
                return 0;
            }
        }

        public static int GetQuizPointsTotal(ulong userId)
        {
            try
            {
                var userDataBase = GetDiscordUserFromId(userId);
                return userDataBase?.QuizPointsTotal ?? 0;
            }
            catch (Exception ex)
            {
                Log.Error(ex, nameof(GetQuizPointsTotal));
                return 0;
            }
        }

        public static void DecreaseTimeSpendWorking(ulong userId, TimeSpan additionalTime)
        {
            try
            {

                var userDataBase = GetDiscordUserFromId(userId);
                if (userDataBase == null)
                {
                    return;
                }

                userDataBase.TimeSpendWorking -= additionalTime;

                lock (Context)
                {
                    Context.DiscordUsers.Update(userDataBase);
                    Context.SaveChanges();
                }

                Log.Information($"Removed time from user {userDataBase.Name}, Time: {additionalTime}");
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Could not Increase the working hours {Environment.NewLine}" +
                              $"User: {userId} {Environment.NewLine}");
            }
        }

        public static void CreateUser(IUser user, ulong guildId)
        {

            try
            {
                lock (Context)
                {
                    if (Context.DiscordUsers.Any(m => m.Id == user.Id.ToString())) return;

                    Context.DiscordUsers.AddRange(new DiscordUser
                    {
                        Id = user.Id.ToString(),
                        Name = user.Username,
                        ActiveGuildsId = new List<string> { guildId.ToString() },
                        TimesBotUsed = 0,
                        TimeSpendWorking = new TimeSpan()
                    });


                    Context.SaveChanges();

                    Log.Information($"Created new User! {Environment.NewLine}" +
                                    $"User: {user.Username} {Environment.NewLine}" +
                                    $"Guild: {guildId}");
                }

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
                lock (Context)
                {
                    return Context.DiscordUsers.ToList();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, nameof(GetAllUsers));
                return new List<DiscordUser>();
            }
        }

        internal static void ResetQuizDatabase()
        {
            try
            {
                lock (Context)
                {
                    var users = Context.DiscordUsers.ToList();
                    foreach (var user in users)
                    {
                        user.QuizPointsTotal = 0;
                        user.QuizWonTotal = 0;
                    }

                    Context.DiscordUsers.UpdateRange(users);
                    Context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex, nameof(ResetQuizDatabase));
            }
        }
    }
}
