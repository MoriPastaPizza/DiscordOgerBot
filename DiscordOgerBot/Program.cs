using DiscordOgerBot.Controller;
using Serilog;
using Serilog.Sinks.Discord;

namespace DiscordOgerBot
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .Enrich.FromLogContext()
                .CreateLogger();

            Log.Information("Logger Created!");

            LoadDatabaseConfig();
            DataBase.StartupDataBase();
            await OgerBot.StartBot();
                            
            await Task.Delay(-1);
        }


        private static void LoadDatabaseConfig()
        {
            Globals.DataBaseConnection.Host = "127.0.0.1:5432";
            Globals.DataBaseConnection.Name = "appdb";
            Globals.DataBaseConnection.UserName = "appuser";
            Globals.DataBaseConnection.Password = Environment.GetEnvironmentVariable("dbPassword");
        }
    }
}
