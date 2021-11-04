using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octokit;
using Serilog;

namespace DiscordOgerBot.Controller
{
    internal static class GitHub
    {
        internal static async Task<List<string>> GetCommitMessagesSinceLastUpdate()
        {
            try
            {
                var client = new GitHubClient(new ProductHeaderValue("discord-oger-bot"));

                var tokenAuth = new Credentials(Environment.GetEnvironmentVariable("GITHUB_TOKEN"));
                client.Credentials = tokenAuth;

                var commits = await client.Repository.Commit.GetAll("MoriPastaPizza", "DiscordOgerBot");

                var persData = DataBase.GetPersistentData();

                if (persData == null) return null;

                var oldHash = persData.ComitHash;
                var currHash = commits[0].Sha;

                if (string.Equals(oldHash, currHash)) return null;

                persData.ComitHash = currHash;
                DataBase.SetPersistentData(persData);

                var missedCommits = commits.TakeWhile(m => m.Sha != oldHash);
                return missedCommits.Select(m => m.Commit.Message).ToList();

            }
            catch (Exception ex)
            {
                Log.Error(ex, nameof(GetCommitMessagesSinceLastUpdate));
                return null;
            }
        }
    }
}
