using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DiscordOgerBotWeb.Models
{
    public class DiscordUser
    {
        [Required]
        public ulong Id { get; set; }

        public string Name { get; set; }
    }
}
