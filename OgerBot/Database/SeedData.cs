using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiscordOgerBotWeb.Models;

namespace DiscordOgerBotWeb.Database
{
    public static class SeedData
    {
        public static void Initialize()
        {
            using var context = new OgerBotDataBaseContext();

            context.Database.EnsureCreated();
            context.SaveChanges();

            context.Add(new DiscordUser
            {
                Name = "Hello World!"
            });

            context.SaveChanges();
        }
    }
}
