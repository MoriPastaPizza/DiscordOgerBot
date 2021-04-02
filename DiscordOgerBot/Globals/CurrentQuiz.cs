using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;
using DiscordOgerBot.Models;

namespace DiscordOgerBot.Globals
{
    internal enum QuizState
    {
        NotRunning,
        PrepPhase,
        Running,
        EndPhase
    }

    internal static class CurrentQuiz
    {
        internal static QuizState QuizState { get; set; } = QuizState.NotRunning;

        internal static List<QuizUser> CurrentQuizUsers { get; set; } = new();

        internal static DiscordUser CurrentQuizMaster { get; set; }

        internal static ISocketMessageChannel CurrentQuizChannel { get; set; }
    }
}
