﻿using System;
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
        private static readonly OgerBotDataBaseContext Context = new();

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


        public static void IncreaseInteractionCount(IUser user, ulong guildId)
        {
            try
            {
                lock (Context)
                {
                    var userDataBase = Context.DiscordUsers.FirstOrDefault(m => m.Id == user.Id.ToString());

                    if (userDataBase == null)
                    {
                        CreateUser(user);
                        userDataBase = Context.DiscordUsers.FirstOrDefault(m => m.Id == user.Id.ToString());
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
                lock (Context)
                {
                    var userDataBase = Context.DiscordUsers.FirstOrDefault(m => m.Id == user.Id.ToString());
                    if (userDataBase != null) return userDataBase.TimesBotUsed;
                    CreateUser(user);
                    userDataBase = Context.DiscordUsers.FirstOrDefault(m => m.Id == user.Id.ToString());

                    return userDataBase?.TimesBotUsed ?? 0;
                }

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
                lock (Context)
                {
                    var userDataBase = Context.DiscordUsers.FirstOrDefault(m => m.Id == user.Id.ToString());
                    if (userDataBase != null) return userDataBase.TimeSpendWorking;
                    CreateUser(user);
                    userDataBase = Context.DiscordUsers.FirstOrDefault(m => m.Id == user.Id.ToString());

                    return userDataBase?.TimeSpendWorking ?? new TimeSpan();
                }
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
                lock (Context)
                {
                    var userDataBase = Context.DiscordUsers.FirstOrDefault(m => m.Id == userId.ToString());
                    if (userDataBase == null)
                    {
                        return;
                    }

                    userDataBase.TimeSpendWorking += additionalTime;


                    Context.DiscordUsers.Update(userDataBase);
                    Context.SaveChanges();

                    Log.Information($"Added time to user {userDataBase.Name}, Time: {additionalTime}");
                }

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
                lock (Context)
                {
                    var userDataBase = Context.DiscordUsers.FirstOrDefault(m => m.Id == userId.ToString());
                    if (userDataBase == null)
                    {
                        return;
                    }

                    userDataBase.QuizPointsTotal += pointsToAdd;

                    Context.DiscordUsers.Update(userDataBase);
                    Context.SaveChanges();

                    Log.Information($"Added Total Point to user {userDataBase.Name}");
                }
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
                lock (Context)
                {
                    var userDataBase = Context.DiscordUsers.FirstOrDefault(m => m.Id == userId.ToString());
                    if (userDataBase == null)
                    {
                        return;
                    }

                    userDataBase.QuizWonTotal++;

                    Context.DiscordUsers.Update(userDataBase);
                    Context.SaveChanges();

                    Log.Information($"Added Total Wins to user {userDataBase.Name}");
                }
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
                lock (Context)
                {
                    var userDataBase = Context.DiscordUsers.FirstOrDefault(m => m.Id == userId.ToString());
                    return userDataBase?.QuizWonTotal ?? 0;
                }
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
                lock (Context)
                {
                    var userDataBase = Context.DiscordUsers.FirstOrDefault(m => m.Id == userId.ToString());
                    return userDataBase?.QuizPointsTotal ?? 0;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, nameof(GetQuizPointsTotal));
                return 0;
            }
        }

        public static void IncreaseEdiTimeoutTotal(ulong userId, TimeSpan timeout)
        {
            try
            {
                lock (Context)
                {
                    var userDataBase = Context.DiscordUsers.FirstOrDefault(m => m.Id == userId.ToString());
                    if (userDataBase == null)
                    {
                        return;
                    }

                    userDataBase.EdiTimeOutTotal += timeout;


                    Context.DiscordUsers.Update(userDataBase);
                    Context.SaveChanges();

                    Log.Information($"Added Edi timeout to user {userDataBase.Name}, Time: {timeout}");
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Could not Increase the Edi Timeout hours {Environment.NewLine}" +
                              $"User: {userId} {Environment.NewLine}");
            }
        }

        public static TimeSpan GetEdiTimeoutTotal(ulong userId)
        {
            try
            {
                lock (Context)
                {
                    var userDataBase = Context.DiscordUsers.FirstOrDefault(m => m.Id == userId.ToString());
                    return userDataBase?.EdiTimeOutTotal ?? new TimeSpan();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, nameof(GetEdiTimeoutTotal));
                return new TimeSpan();
            }
        }

        public static void IncreaseEdiUsed(ulong userId)
        {
            try
            {
                lock (Context)
                {
                    var userDataBase = Context.DiscordUsers.FirstOrDefault(m => m.Id == userId.ToString());
                    if (userDataBase == null)
                    {
                        return;
                    }

                    userDataBase.EdiUsed++;

                    Context.DiscordUsers.Update(userDataBase);
                    Context.SaveChanges();

                    Log.Information($"Added Edi used {userDataBase.Name}");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, nameof(IncreaseEdiUsed));
            }
        }

        public static int GetEdiUsed(ulong userId)
        {
            try
            {
                lock (Context)
                {
                    var userDataBase = Context.DiscordUsers.FirstOrDefault(m => m.Id == userId.ToString());
                    return userDataBase?.EdiUsed ?? 0;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, nameof(GetEdiUsed));
                return 0;
            }
        }

        public static void IncreaseEdiSuccessfull(ulong userId)
        {
            try
            {
                lock (Context)
                {
                    var userDataBase = Context.DiscordUsers.FirstOrDefault(m => m.Id == userId.ToString());
                    if (userDataBase == null)
                    {
                        return;
                    }

                    userDataBase.EdiSuccessfull++;

                    Context.DiscordUsers.Update(userDataBase);
                    Context.SaveChanges();

                    Log.Information($"Added Edi succ. {userDataBase.Name}");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, nameof(IncreaseEdiSuccessfull));
            }
        }

        public static int GetEdiSuccessfull(ulong userId)
        {
            try
            {
                lock (Context)
                {
                    var userDataBase = Context.DiscordUsers.FirstOrDefault(m => m.Id == userId.ToString());
                    return userDataBase?.EdiSuccessfull ?? 0;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, nameof(GetEdiSuccessfull));
                return 0;
            }
        }

        public static TimeSpan GetSeason0Time(ulong userId)
        {
            try
            {
                lock (Context)
                {
                    var userDataBase = Context.DiscordUsers.FirstOrDefault(m => m.Id == userId.ToString());
                    return userDataBase?.EdiTimeOutTotalSeason0 ?? new TimeSpan();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, nameof(GetSeason0Time));
                return new TimeSpan();
            }
        }

        public static TimeSpan GetSeason1Time(ulong userId)
        {
            try
            {
                lock (Context)
                {
                    var userDataBase = Context.DiscordUsers.FirstOrDefault(m => m.Id == userId.ToString());
                    return userDataBase?.EdiTimeOutTotalSeason1 ?? new TimeSpan();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, nameof(GetSeason1Time));
                return new TimeSpan();
            }
        }

        public static void AddTimetoSeason1(ulong userId, TimeSpan time)
        {
            try
            {
                lock (Context)
                {
                    var userDataBase = Context.DiscordUsers.FirstOrDefault(m => m.Id == userId.ToString());
                    if (userDataBase == null)
                    {
                        return;
                    }

                    userDataBase.EdiTimeOutTotalSeason1 += time;
                    userDataBase.EdiTimeOutTotal += time;


                    Context.DiscordUsers.Update(userDataBase);
                    Context.SaveChanges();

                    Log.Information($"Added Edi timeout to user {userDataBase.Name}, Time: {time}");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, nameof(AddTimetoSeason1));
            }
        }

        public static void SetEdiTimeTillUnlock(ulong userId, long date)
        {
            try
            {
                lock (Context)
                {
                    var userDataBase = Context.DiscordUsers.FirstOrDefault(m => m.Id == userId.ToString());
                    if (userDataBase == null)
                    {
                        return;
                    }

                    userDataBase.EdiTimeTillUnlock = date;


                    Context.DiscordUsers.Update(userDataBase);
                    Context.SaveChanges();

                    Log.Information($"Set Edi timeout to user {userDataBase.Name}, till: {date}");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, nameof(SetEdiTimeTillUnlock));
            }
        }

        public static long GetEdiTimeTillUnlock(ulong userId)
        {
            try
            {
                lock (Context)
                {
                    var userDataBase = Context.DiscordUsers.FirstOrDefault(m => m.Id == userId.ToString());
                    return userDataBase?.EdiTimeTillUnlock ?? 0;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, nameof(GetEdiTimeTillUnlock));
                return 0;
            }
        }

        public static void CreateUser(IUser user)
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
                        ActiveGuildsId = new List<string>(),
                        TimesBotUsed = 0,
                        TimeSpendWorking = new TimeSpan()
                    });


                    Context.SaveChanges();

                    Log.Information($"Created new User! {Environment.NewLine}" +
                                    $"User: {user.Username} {Environment.NewLine}");
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Could not create User in DB {Environment.NewLine}" +
                                 $"User: {user.Username} {Environment.NewLine}");
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

        internal static PersistentData GetPersistentData()
        {
            try
            {
                lock (Context)
                {
                    var data = Context.PersistentData.FirstOrDefault(m => m.Id == "0");
                    if (data != null) return data;

                    Context.PersistentData.Add(new PersistentData
                    {
                        Id = "0",
                        BotVersion = Environment.GetEnvironmentVariable("HEROKU_RELEASE_VERSION"),
                        ComitHash = "68a7a69c67ca8a6a611ce24f2220312d50f778d0"
                    });

                    Context.SaveChanges();

                    data = Context.PersistentData.FirstOrDefault(m => m.Id == "0");
                    return data;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, nameof(GetPersistentData));
                return null;
            }
        }

        internal static void SetPersistentData(PersistentData data)
        {
            try
            {
                lock (Context)
                {
                    var dataBaseData = Context.PersistentData.FirstOrDefault(m => m.Id == data.Id);
                    if(dataBaseData == null) return;

                    dataBaseData = data;

                    Context.PersistentData.Update(dataBaseData);
                    Context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, nameof(SetPersistentData));
            }
        }
    }
}
