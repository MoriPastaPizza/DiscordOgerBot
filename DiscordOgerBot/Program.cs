using System;
using DiscordOgerBot.Controller;
using Serilog;
using Serilog.Sinks.Discord;

namespace DiscordOgerBot
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .WriteTo.Discord(956603932157812776,Environment.GetEnvironmentVariable("WEBHOOK_TOKEN"))
                .CreateLogger();

            LoadDatabaseConfig();
            DataBase.StartupDataBase();
            OgerBot.StartBot().GetAwaiter().GetResult();
        }


        private static void LoadDatabaseConfig()
        {
            var url = Environment.GetEnvironmentVariable("DATABASE_URL");
            if (url == null) return;
            Globals.DataBaseConnection.Host = url.Substring(url.IndexOf('@') + 1, url.LastIndexOf(':') - url.IndexOf('@') - 1);
            Globals.DataBaseConnection.Name = url.Substring(url.LastIndexOf('/') + 1);
            Globals.DataBaseConnection.UserName = url.Substring(11, url.IndexOf(':', 11) - 11);
            Globals.DataBaseConnection.Password = url.Substring(url.IndexOf(':', 11) + 1, url.IndexOf('@') - url.IndexOf(':', 11) - 1);
        }
    }
}
