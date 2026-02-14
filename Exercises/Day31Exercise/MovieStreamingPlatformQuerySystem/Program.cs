namespace MovieStreamingPlatformQuerySystem;

class Program
{
    static void Main(string[] args)
    {
        List<Movie> movies = [
            new Movie { Title = "Inception", Genre = "SciFi", Rating = 9, Year = 2010 },
            new Movie { Title = "Avatar", Genre = "SciFi", Rating = 8.5, Year = 2009 },
            new Movie { Title = "Titanic", Genre = "Drama", Rating = 8, Year = 1997 }
        ];
        
        Console.WriteLine("MOVIES HAVING RATING ABOVE 8:");
        movies.Where(m => m.Rating > 8)
            .ToList()
            .ForEach(Console.WriteLine);
        
        Console.WriteLine("\nAVERAGE RATING OF MOVIES BY GENRE:");
        movies.GroupBy(m => m.Genre)
            .Select(g => new { g.Key, AverageRating = g.Average(m => m.Rating) })
            .ToList()
            .ForEach(g => Console.WriteLine($"{g.Key}: {g.AverageRating:F1}"));
        
        Console.WriteLine("\nLATEST MOVIE BY GENRE:");
        movies.GroupBy(m => m.Genre)
            .Select(g => new { g.Key, Latest = g.MaxBy(m => m.Year) })
            .ToList()
            .ForEach(r => 
                Console.WriteLine($"{r.Key}: {r.Latest?.Title}, {r.Latest?.Year}")
            );
        
        Console.WriteLine("\nTOP 5 HIGHEST RATED MOVIES:");
        movies.OrderByDescending(m => m.Rating)
            .Take(5)
            .ToList()
            .ForEach(Console.WriteLine);
    }
}

class Movie
{
    public string? Title { get; set; }
    public string? Genre { get; set; }
    public double Rating { get; set; }
    public int Year { get; set; }

    public override string ToString() =>
        $"{Title}, {Genre}, {Rating}, {Year}";
}