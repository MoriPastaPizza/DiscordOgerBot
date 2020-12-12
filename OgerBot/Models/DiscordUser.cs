using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiscordOgerBotWeb.Models
{
    public class DiscordUser
    {
        [Required]
        public ulong Id { get; set; }

        public List<ulong> ActiveGuildsId { get; set; }

        public string Name { get; set; }

        public uint TimesBotUsed { get; set; }

        public TimeSpan TimeSpendWorking { get; set; }
    }
}
