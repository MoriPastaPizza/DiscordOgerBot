using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DiscordOgerBotWeb.Models;

namespace DiscordOgerBotWeb.Database
{
    public class OgerBotDataBaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql($"Host={Environment.GetEnvironmentVariable("DATABASE_HOST")};Database={Environment.GetEnvironmentVariable("DATABASE_NAME")};" +
                                        $"Username={Environment.GetEnvironmentVariable("DATABASE_USERNAME")};Password={Environment.GetEnvironmentVariable("DATABASE_PASSWORD")}");


        public DbSet<DiscordUser> DiscordUsers { get; set; }
    }
}
