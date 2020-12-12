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
            => optionsBuilder.UseNpgsql($"Host={Globals.DataBaseConnection.Host};Database={Globals.DataBaseConnection.Name};" +
                                        $"Username={Globals.DataBaseConnection.UserName};Password={Globals.DataBaseConnection.Password}");


        public DbSet<DiscordUser> DiscordUsers { get; set; }
    }
}
