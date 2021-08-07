using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Serilog;

namespace DiscordOgerBot.Controller
{
    public static class TimeManagement
    {
        private static readonly Dictionary<ulong, ActiveUserStruct> ActiveUsers = new();
        private static readonly TimeSpan CoolOfTime = TimeSpan.FromMinutes(1);


        public static void Measure(ulong userId)
        {
            try
            {
                if (ActiveUsers.TryGetValue(userId, out var activeUserStruct))
                {
                    activeUserStruct.CancellationTokenSource.Cancel();
                }
                else
                {
                    var newActiveUserStruct = new ActiveUserStruct
                    {
                        ActiveTime = new TimeSpan(),
                        CancellationTokenSource = new CancellationTokenSource()
                    };

                    ActiveUsers.Add(userId, newActiveUserStruct);

                    Task.Factory.StartNew(() =>
                    {
                        MeasureActiveTime(userId, newActiveUserStruct.CancellationTokenSource.Token);
                    }, newActiveUserStruct.CancellationTokenSource.Token);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Could not measure time of user {userId}");
            }
        }

        private static void MeasureActiveTime(ulong userId, CancellationToken cancellationToken)
        {
            try
            {
                var stopWatch = new Stopwatch();
                stopWatch.Start();

                if (cancellationToken.WaitHandle.WaitOne(CoolOfTime))
                {
                    stopWatch.Stop();
                    var user = ActiveUsers[userId];
                    user.ActiveTime += stopWatch.Elapsed;

                    user.CancellationTokenSource.Dispose();
                    user.CancellationTokenSource = new CancellationTokenSource();

                    ActiveUsers[userId] = user;

                    Task.Factory.StartNew(() => { MeasureActiveTime(userId, user.CancellationTokenSource.Token); },
                        user.CancellationTokenSource.Token);
                }
                else
                {
                    stopWatch.Stop();
                    var user = ActiveUsers[userId];
                    user.ActiveTime += stopWatch.Elapsed;

                    DataBase.IncreaseTimeSpendWorking(userId, user.ActiveTime);

                    ActiveUsers.Remove(userId);
                    user.CancellationTokenSource.Cancel();
                    user.CancellationTokenSource.Dispose();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Could not measure time of user {userId}");
            }
        }
    }

    internal struct ActiveUserStruct
    {
        internal CancellationTokenSource CancellationTokenSource { get; set; }
        internal TimeSpan ActiveTime { get; set; }
    }
}
