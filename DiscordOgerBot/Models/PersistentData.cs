using System.ComponentModel.DataAnnotations;

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
