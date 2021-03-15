using DiscordOgerBot.Models;
using Microsoft.EntityFrameworkCore;

namespace DiscordOgerBot.Database
{
    public class OgerBotDataBaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql($"Host={Globals.DataBaseConnection.Host};Database={Globals.DataBaseConnection.Name};" +
                                        $"Username={Globals.DataBaseConnection.UserName};Password={Globals.DataBaseConnection.Password}; SSL Mode=Require");


        public DbSet<DiscordUser> DiscordUsers { get; set; }
    }
}
