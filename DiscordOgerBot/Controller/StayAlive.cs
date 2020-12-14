using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DiscordOgerBot.Controller
{
    public static class StayAlive
    {
        private const string Url = "https://oger-bot-discord.herokuapp.com/";
        private static readonly HttpClient Client =  new HttpClient();
        private static readonly TimeSpan HeartBeatRate = TimeSpan.FromMinutes(15);
        private static CancellationTokenSource _cancellationTokenSource;

        public static void StartHeartBeat()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            new Task(StayAliveTask, _cancellationTokenSource.Token, TaskCreationOptions.LongRunning).Start();
        }

        private static void StayAliveTask()
        {
            while (!_cancellationTokenSource.Token.IsCancellationRequested)
            {
                _cancellationTokenSource.Token.WaitHandle.WaitOne(HeartBeatRate);
                _ = Client.GetAsync(Url);
            }
        }
    }
}
