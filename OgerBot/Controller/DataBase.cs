using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using DiscordOgerBotWeb.Database;
using Microsoft.Extensions.Logging;

namespace DiscordOgerBotWeb.Controller
{
    public static class DataBase
    {
        private static readonly ILogger Logger = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();

        }).CreateLogger("Database Logger");


        public static async Task IncreaseInteractionCount(IUser user, SocketCommandContext commandContext)
        {
            try
            {
                using var context = new OgerBotDataBaseContext();

                var userDataBase = await context.DiscordUsers
                    .FindAsync(user.Id);

                if (userDataBase == null)
                {
                    Logger.LogWarning($"User not Found! {Environment.NewLine}" +
                                       $"Id: {user.Id} {Environment.NewLine}" +
                                       $"Name: {user.Username} {Environment.NewLine}" +
                                       $"Guild: {commandContext.Guild.Name}");
                    return;
                }

                userDataBase.TimesBotUsed++;

                context.DiscordUsers.Update(userDataBase);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Could not Increase User Interaction Count {Environment.NewLine}" +
                                     $"User: {user.Username} {Environment.NewLine}" +
                                     $"Guild: {commandContext.Guild.Name}");
            }
        }
    }
}
