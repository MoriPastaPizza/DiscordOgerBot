using DiscordOgerBot.Models;
using Microsoft.EntityFrameworkCore;

namespace DiscordOgerBot.Database
{
    public class OgerBotDataBaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql($"Host={Globals.DataBaseConnection.Host};Database={Globals.DataBaseConnection.Name};" +
                                        $"Username={Globals.DataBaseConnection.UserName};Password={Globals.DataBaseConnection.Password}; SSL Mode=Require; Trust Server Certificate=true;");


        public DbSet<DiscordUser> DiscordUsers { get; set; }

        public DbSet<PersistentData> PersistentData { get; set; }
    }
}
