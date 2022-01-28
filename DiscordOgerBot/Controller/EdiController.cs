using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DiscordOgerBot.Models;
using Serilog;

namespace DiscordOgerBot.Controller
{
    internal static class EdiController
    {

        internal static List<Items<Edi>> EdisWithProbs { get; private set; }

        private static List<Items<Edi>> Edis { get; set; } = new()
        {
            new Items<Edi>() { Probability = 0.75, Item = new Edi{Type = EdiType.Common, Name = "Common Edi", BonusTime = TimeSpan.Zero, Color = (255, 255, 255)}},
            new Items<Edi>() { Probability = 0.15, Item = new Edi { Type = EdiType.Uncommon, Name = "Uncommon Edi", BonusTime = TimeSpan.FromHours(3), Color = (30, 255, 0)} },
            new Items<Edi>() { Probability = 0.07, Item = new Edi { Type = EdiType.Rare, Name = "Rare Edi", BonusTime = TimeSpan.FromHours(6), Color = (0, 112, 221)} },
            new Items<Edi>() { Probability = 0.025, Item = new Edi { Type = EdiType.Epic, Name = "Epic Edi", BonusTime = TimeSpan.FromHours(12), Color = (163, 53, 238)} },
            new Items<Edi>() { Probability = 0.0048, Item = new Edi { Type = EdiType.Legendary, Name = "Legendary Edi", BonusTime = TimeSpan.FromHours(24), Color = (255, 128, 0)} },
            new Items<Edi>() { Probability = 0.0002, Item = new Edi { Type = EdiType.Mythic, Name = "Mythic Edi", BonusTime = TimeSpan.FromHours(48), Color = (0, 255, 255)} },
        };

        private static readonly Random Rand = new();

        private const string GitHubBaseUrl = "https://raw.githubusercontent.com/MoriPastaPizza/DiscordOgerBot/master/DiscordOgerBot/Images/Edi";
        private const ulong EdiTimeoutRole = 934758329228591104;
        private static string _commonEdiImagePath = Path.GetFullPath(
            Path.Combine(AppContext.BaseDirectory, "../DiscordOgerBot/Images/Edi/Common"));

        internal static void Init()
        {
            try
            {
                EdisWithProbs = Edis;
                var temp = new List<Items<Edi>>(Edis.Count);
                var sum = 0.0;
                foreach (var item in Edis.Take(Edis.Count -1))
                {
                    sum += item.Probability;
                    temp.Add(new Items<Edi> { Probability = sum, Item = item.Item });
                }
                temp.Add(new Items<Edi> { Probability = 1.0, Item = Edis.Last().Item });

                Edis = temp;

                Task.Factory.StartNew(async () =>
                {
                    await CheckForTimeoutTask();
                },TaskCreationOptions.LongRunning);
            }
            catch (Exception ex)
            {
                Log.Error(ex, nameof(Init));
            }
        }

        internal static Edi GetAnEdi()
        {
            var roll = Rand.NextDouble();
            var selected = Edis.SkipWhile(i => i.Probability < roll).First();

            switch (selected.Item.Type)
            {
                case EdiType.Common:
                    var edis = Directory.GetFiles(_commonEdiImagePath);
                    var selectedEdi = edis[Rand.Next(edis.Length)];
                    selected.Item.ImageUrl = GitHubBaseUrl + "/Common/" + selectedEdi.Substring(selectedEdi.LastIndexOf("/", StringComparison.Ordinal));
                    break;
                case EdiType.Uncommon:
                    selected.Item.ImageUrl = GitHubBaseUrl + "/edi_uncommon.png";
                    break;
                case EdiType.Rare:
                    selected.Item.ImageUrl = GitHubBaseUrl + $"/edi_rare{Rand.Next(1, 4)}.png";
                    break;
                case EdiType.Epic:
                    selected.Item.ImageUrl = GitHubBaseUrl + $"/edi_epic{Rand.Next(1, 5)}.gif";
                    break;
                case EdiType.Legendary:
                    selected.Item.ImageUrl = GitHubBaseUrl + "/edi_leg.gif";
                    break;
                case EdiType.Mythic:
                    selected.Item.ImageUrl = GitHubBaseUrl + "/edi_myth.gif";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            selected.Item.BasicTime = TimeSpan.FromMinutes(Rand.Next(5, 181));
            selected.Item.Roll = roll;

            return selected.Item;
        }

        private static async Task CheckForTimeoutTask()
        {
            while (true)
            {
                try
                {
                    await Task.Delay(TimeSpan.FromMinutes(1));

                    var server = OgerBot.Client.GetGuild(758745761566818314);
                    var ediRole = server?.GetRole(EdiTimeoutRole);
                    if (ediRole == null) return;

                    var timeNowUnix = DateTimeOffset.Now.ToUnixTimeSeconds();

                    foreach (var member in ediRole.Members)
                    {
                        try
                        {
                            var timeTillunlock = DataBase.GetEdiTimeTillUnlock(member.Id);
                            if (timeNowUnix > timeTillunlock)
                            {
                                await member.RemoveRoleAsync(EdiTimeoutRole);
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.Error(ex, member.Username);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex, nameof(CheckForTimeoutTask));
                }

            }
        }
    }

    internal class Items<T>
    {
        public double Probability { get; init; }
        public T Item { get; init; }
    }
}
