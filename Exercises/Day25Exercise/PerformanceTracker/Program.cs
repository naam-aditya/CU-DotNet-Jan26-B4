namespace PerformanceTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = @"PerformanceTracker/players.csv";
            List<Player> players = [];
            
            try
            {
                using FileStream fileStream = new(fileName, FileMode.Open);
                using StreamReader stringReader = new(fileStream);

                string? line = string.Empty;

                while ((line = stringReader.ReadLine()) != null)
                {
                    string[] playerDetails = line.Split(',');
                    Player player = new(
                        name: playerDetails[0],
                        runsScored: Convert.ToInt32(playerDetails[1]),
                        ballsFaced: Convert.ToInt32(playerDetails[2]),
                        isOut: Convert.ToBoolean(playerDetails[3])
                    );

                    players.Add(player);    
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine(
                $"{"Name",-12}{"Runs",6}{"SR",6}{"Avg",8}\n" +
                $"--------------------------------"
            );

            foreach (var player in players)
                Console.WriteLine(player.ToString());
        }
    }

    class Player
    {
        public string Name { get; set; }
        public int RunsScored { get; set; }
        public int BallsFaced { get; set; }
        public bool IsOut { get; set; }
        public double StrikeRate { get; set; }
        public double Average { get; set; }

        public Player(string name, int runsScored, int ballsFaced, bool isOut)
        {
            Name = name;
            RunsScored = runsScored;
            BallsFaced = ballsFaced;
            IsOut = isOut;
            StrikeRate = (double) RunsScored / BallsFaced * 100.0;
            Average = RunsScored;
        }

        public override string ToString() =>
            $"{Name,-12}{RunsScored,6}{StrikeRate,6:F2}{Average,8:F2}";
    }
}
