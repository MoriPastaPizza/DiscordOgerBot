using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordOgerBot.Models
{
    internal class Edi
    {
        internal string Name { get; set; }
        internal EdiType Type { get; set; }
        internal string ImageUrl { get; set; }
        internal (byte, byte, byte) Color { get; set; }
        internal TimeSpan BonusTime { get; set; }
        internal TimeSpan BasicTime { get; set; }
        internal double Roll { get; set; }
    }

    internal enum EdiType
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary,
        Mythic
    }
}
