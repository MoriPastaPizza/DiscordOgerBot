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
    }
}
