using System.Collections;

namespace HighScoreLeaderboard
{
    class Program
    {
        static void Main(string[] args)
        {
            SortedDictionary<double, string> leaderboard = [];
            leaderboard.Add(55.42, "SwiftRacer");
            leaderboard.Add(52.10, "SpeedDemon");
            leaderboard.Add(58.91, "SteadyEddie");
            leaderboard.Add(51.05, "TurboTom");

            foreach (var item in leaderboard)
                Console.WriteLine($"{item.Key}: {item.Value}");
            
            Console.WriteLine($"\nGold Medalist: {leaderboard.First().Value}");

            leaderboard.Remove(58.91);
            leaderboard.Add(54.00, "SteadyEddie");
        }
    }
}