using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordOgerBot.Models
{
    public class PersistentData
    {
        [Required, Key]
        public string Id { get; set; }
        public string BotVersion { get; set; }
        public string ComitHash { get; set; }
        public int VlogCount { get; set; }
    }
}
