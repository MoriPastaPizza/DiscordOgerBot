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

            if(context.DiscordUsers.Any(m => m.Id == 1234565)) return;

            context.Add(new DiscordUser
            {
                Id = 1234565,
                Name = "Hello World!"
            });

            context.SaveChanges();
        }
    }
}
