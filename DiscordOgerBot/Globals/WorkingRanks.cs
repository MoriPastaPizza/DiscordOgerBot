using System;
using System.Collections.Generic;

namespace DiscordOgerBot.Globals
{
    public static class WorkingRanks
    {
        public static readonly List<WorkingRankProperties> TimeForRanks = new List<WorkingRankProperties>
        {
            new WorkingRankProperties{Time = TimeSpan.FromDays(5), Rank = 9, RankId = 791994133307850762, ImageUrl = "https://raw.githubusercontent.com/MoriPastaPizza/DiscordOgerBot/master/DiscordOgerBot/Images/youtube.png"},
            new WorkingRankProperties{Time = TimeSpan.FromDays(2), Rank = 8, RankId = 791993814436806676, ImageUrl = "https://raw.githubusercontent.com/MoriPastaPizza/DiscordOgerBot/master/DiscordOgerBot/Images/rendern.png"},
            new WorkingRankProperties{Time = TimeSpan.FromDays(1), Rank = 7, RankId = 791992773356027914, ImageUrl = "https://raw.githubusercontent.com/MoriPastaPizza/DiscordOgerBot/master/DiscordOgerBot/Images/schneider.png"},
            new WorkingRankProperties{Time = TimeSpan.FromHours(16), Rank = 6, RankId = 791992444287844354, ImageUrl = "https://raw.githubusercontent.com/MoriPastaPizza/DiscordOgerBot/master/DiscordOgerBot/Images/buddern.gif"},
            new WorkingRankProperties{Time = TimeSpan.FromHours(10), Rank = 5, RankId = 791992226347089951, ImageUrl = "https://raw.githubusercontent.com/MoriPastaPizza/DiscordOgerBot/master/DiscordOgerBot/Images/arbeitspulli.gif"},
            new WorkingRankProperties{Time = TimeSpan.FromHours(5), Rank = 4, RankId = 791991747252191282, ImageUrl = "https://raw.githubusercontent.com/MoriPastaPizza/DiscordOgerBot/master/DiscordOgerBot/Images/selbst%C3%A4ndiger_schichtarbeiter.png"},
            new WorkingRankProperties{Time =  TimeSpan.FromHours(1), Rank = 3, RankId = 791991296797048832, ImageUrl = "https://raw.githubusercontent.com/MoriPastaPizza/DiscordOgerBot/master/DiscordOgerBot/Images/schichtarbeiter.PNG"},
            new WorkingRankProperties{Time = TimeSpan.FromMinutes(30), Rank = 2, RankId = 791990439493500968, ImageUrl = "https://raw.githubusercontent.com/MoriPastaPizza/DiscordOgerBot/master/DiscordOgerBot/Images/frauchen.png"},
            new WorkingRankProperties{Time = TimeSpan.FromMinutes(1), Rank =  1, RankId = 791989775568732190, ImageUrl = "https://raw.githubusercontent.com/MoriPastaPizza/DiscordOgerBot/master/DiscordOgerBot/Images/student.png"},
        };

        public struct WorkingRankProperties
        {
            public TimeSpan Time { get; set; }

            public int Rank { get; set; }

            public ulong RankId { get; set; }

            public string ImageUrl { get; set; }
        }
    }
}