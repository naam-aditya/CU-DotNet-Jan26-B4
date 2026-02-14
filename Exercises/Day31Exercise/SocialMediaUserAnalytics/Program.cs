namespace SocialMediaUserAnalytics;

class Program
{
    static void Main(string[] args)
    {
        List<User> users = [
            new User { Id = 1, Name = "A", Country = "India" },
            new User { Id = 2, Name = "B", Country = "USA" }
        ];

        List<Post> posts = [
            new Post{ Id = 1, UserId = 1, Likes = 100 },
            new Post{ Id = 2, UserId = 1, Likes = 50 }
        ];

        Console.WriteLine("TOTAL LIKES BY USERS:");
        users.GroupJoin(
            posts,
            u => u.Id,
            p => p.UserId,
            (u, g) => new { u, TotalLikes = g.Sum(p => p.Likes) }
        )
        .ToList()
        .ForEach(r => Console.WriteLine($"{r.u}: {r.TotalLikes}"));

        Console.WriteLine("\nUSERS BY COUNTRY:");
        users.GroupBy(u => u.Country)
            .ToList()
            .ForEach(g => 
                Console.WriteLine($"{g.Key}:\n{string.Join("\n", g.ToList())}")
            );

        Console.WriteLine("\nINACTIVE USERS:");
        users.GroupJoin(
            posts,
            u => u.Id,
            p => p.UserId,
            (u, g) => new { User = u, Count = g.Count() }
        )
        .Where(x => x.Count == 0)
        .ToList()
        .ForEach(r => Console.WriteLine($"{r.User}"));

        Console.WriteLine("\nAVERAGE LIKES BY POST:");
        users.GroupJoin(
                posts,
                u => u.Id,
                p => p.UserId,
                (u, g) =>
                {
                    double average = 
                        g.Any() ? g.Average(x => x.Likes) : 0;
                    return new { User = u, AverageLikes = average };
                }
            )
            .ToList()
            .ForEach(r => 
                Console.WriteLine($"{r.User}: {r.AverageLikes}"));
    }
}

class User
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Country { get; set; }

    public override string ToString() =>
        $"{Id},{Name},{Country}";
}

class Post
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int Likes { get; set; }
    public string? Comments { get; set; }
}
