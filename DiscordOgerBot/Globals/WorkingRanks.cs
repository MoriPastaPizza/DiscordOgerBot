using System;
using System.Collections.Generic;

namespace DiscordOgerBot.Globals
{
    public static class WorkingRanks
    {
        public static readonly Dictionary<TimeSpan, ulong> TimeForRanks = new Dictionary<TimeSpan, ulong>
        {
            {TimeSpan.FromMinutes(1), 791989775568732190},
            {TimeSpan.FromMinutes(30), 791990439493500968},
            {TimeSpan.FromHours(1), 791991296797048832 },
            {TimeSpan.FromHours(5), 791991747252191282 },
            {TimeSpan.FromHours(10), 791992226347089951 },
            {TimeSpan.FromHours(16), 791992444287844354 },
            {TimeSpan.FromDays(1), 791992773356027914 },
            {TimeSpan.FromDays(2), 791993814436806676 },
            {TimeSpan.FromDays(5), 791994133307850762 }
        };
    }
}