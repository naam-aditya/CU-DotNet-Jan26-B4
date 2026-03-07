namespace StreamBuzz;

class Program
{
    public static List<CreatorStats> EngagementBoard = [];

    static void Main(string[] args)
    {
        Console.WriteLine("1. Register Creator");
        Console.WriteLine("2. Show Top Posts");
        Console.WriteLine("3. Calculate Average Likes");
        Console.WriteLine("4. Exit");
        Console.WriteLine();

        while (true)
        {
            Console.WriteLine("Enter your choice:");
            string? input = Console.ReadLine();
            if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
                continue;

            if (input == "1")
            {
                CreatorStats creatorStats = new();
                while (true)
                {
                    Console.WriteLine("Enter Creator Name:");
                    string? name = Console.ReadLine();
                    if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
                    {
                        Console.WriteLine("\n##ENTER VALID NAME##");
                        continue;
                    }

                    creatorStats.CreatorName = name;
                    break;
                }

                Console.WriteLine("Enter weekly likes (Week 1 to 4):");
                while (true)
                {
                    string? value = Console.ReadLine();
                    if (
                        !string.IsNullOrEmpty(value) && 
                        !string.IsNullOrWhiteSpace(value) &&
                        int.TryParse(value, out int likes)
                    )
                    {
                        creatorStats.WeeklyLikes[0] = likes;
                        break;
                    }
                    Console.WriteLine("\n##ENTER VALID LIKES##");
                }

                while (true)
                {
                    string? value = Console.ReadLine();
                    if (
                        !string.IsNullOrEmpty(value) && 
                        !string.IsNullOrWhiteSpace(value) &&
                        int.TryParse(value, out int likes)
                    )
                    {
                        creatorStats.WeeklyLikes[1] = likes;
                        break;
                    }
                    Console.WriteLine("\n##ENTER VALID LIKES##");
                }

                while (true)
                {
                    string? value = Console.ReadLine();
                    if (
                        !string.IsNullOrEmpty(value) && 
                        !string.IsNullOrWhiteSpace(value) &&
                        int.TryParse(value, out int likes)
                    )
                    {
                        creatorStats.WeeklyLikes[2] = likes;
                        break;
                    }
                    Console.WriteLine("\n##ENTER VALID LIKES##");
                }

                while (true)
                {
                    string? value = Console.ReadLine();
                    if (
                        !string.IsNullOrEmpty(value) && 
                        !string.IsNullOrWhiteSpace(value) &&
                        int.TryParse(value, out int likes)
                    )
                    {
                        creatorStats.WeeklyLikes[3] = likes;
                        break;
                    }
                    Console.WriteLine("\n##ENTER VALID LIKES##");
                }

                EngagementBoard.Add(creatorStats);
                Console.WriteLine("Creator registered successfully");
            }
            else if (input == "2")
            {
                while (true)
                {
                    Console.WriteLine("Enter like threshold:");
                    string? value = Console.ReadLine();
                    if (
                        !string.IsNullOrEmpty(value) && 
                        !string.IsNullOrWhiteSpace(value) &&
                        int.TryParse(value, out int likes)
                    )
                    {
                        Program program = new();
                        var topPostCounts = program.GetTopPostCounts(EngagementBoard, likes);

                        if (topPostCounts.Count == 0)
                        {
                            Console.WriteLine("No top-performing posts this week");
                            break;
                        }

                        Console.WriteLine(
                            string.Join(
                                '\n',
                                program.GetTopPostCounts(EngagementBoard, likes)
                                    .Select(r => $"{r.Key} - {r.Value}")
                            )
                        );
                        break;
                    }
                    Console.WriteLine("\n##ENTER VALID LIKES##");
                }
            }
            else if (input == "3")
            {
                Program program = new();
                Console.WriteLine($"Overall average weekly likes: {program.CalculateAverageLikes()}");
            }
            else if (input == "4")
                break;
            else
                Console.WriteLine("##CHOOSE VALID OPTION##");
            
            Console.WriteLine(); 
        }
    }

    public void RegisterCreator(CreatorStats record) =>
        EngagementBoard.Add(record);

    public Dictionary<string, int> GetTopPostCounts(List<CreatorStats> records, double likeThreshold)
        => EngagementBoard.Select(
            r =>
            {
                var name = r.CreatorName;
                var count = r.WeeklyLikes.Count(l => l >= likeThreshold);
                return new KeyValuePair<string, int>(name, count);
            }
        )
        .Where(r => r.Value != 0)
        .ToDictionary();

    public double CalculateAverageLikes() =>
        EngagementBoard.SelectMany(r => r.WeeklyLikes)
            .Average();
}

public class CreatorStats
{
    public string CreatorName { get; set; }
    public double[] WeeklyLikes { get; set; }

    public CreatorStats()
    {
        CreatorName = string.Empty;
        WeeklyLikes = new double[4];
    }
}