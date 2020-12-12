using System;
using DiscordOgerBotWeb.Controller;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;


namespace DiscordOgerBotWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            LoadDatabaseConfig();
            OgerBot.StartBot().GetAwaiter().GetResult();
            StayAlive.StartHeartBeat();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseKestrel(options =>
                    {
                        options.ListenAnyIP(int.Parse(Environment.GetEnvironmentVariable("PORT")!));
                    });
                });

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
