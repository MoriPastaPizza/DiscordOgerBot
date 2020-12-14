using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiscordOgerBot.Models
{
    public class DiscordUser
    {
        [Required]
        public string Id { get; set; }

        public List<string> ActiveGuildsId { get; set; }

        public string Name { get; set; }

        public uint TimesBotUsed { get; set; }

        public TimeSpan TimeSpendWorking { get; set; }
    }
}
