namespace BookManagementSystem;

class Program
{
    static void Main(string[] args)
    {
        List<Book> books =
        [
            new Book 
            {
                Title = "C# Basics", 
                Author = "John", 
                Genre = "Tech", 
                Year = 2018, 
                Price = 500
            },
            new Book 
            {
                Title = "Java Advanced", 
                Author = "Mike", 
                Genre = "Tech", 
                Year = 2016, 
                Price = 700
            },
            new Book 
            {
                Title = "History India",
                Author = "Raj", 
                Genre = "History", 
                Year = 2019, 
                Price = 400
            }
        ];


        Console.WriteLine("BOOKS PUBLISHED AFTER 2015:");
        books.Where(b => b.Year > 2015)
            .ToList()
            .ForEach(r => Console.WriteLine($"{r.Title}: {r.Year}"));
        
        Console.WriteLine("\nGENRE AND COUNT OF BOOKS:");
        books.GroupBy(b => b.Genre)
            .Select(
                x => new
                { x.Key, Count = x.Count() }
            )
            .ToList()
            .ForEach(r => Console.WriteLine($"{r.Key}: {r.Count}"));
        
        Console.WriteLine("\nMOST EXPENSIVE BOOK BY GENRE:");
        books.GroupBy(b => b.Genre)
            .Select(
                x => new
                { x.Key, MostExpensive = x.MaxBy(b => b.Price) }
            )
            .ToList()
            .ForEach(r => Console.WriteLine($"{r.Key}: {r.MostExpensive?.Title}"));
        
        Console.WriteLine("\nALL AUTHORS LIST:");
        books.DistinctBy(b => b.Author)
            .ToList()
            .ForEach(r => Console.WriteLine(r.Author));
    }
}

class Book
{
    public string? Title { get; set; }
    public string? Author { get; set; }
    public string? Genre { get; set; }
    public int Year { get; set; }
    public double Price { get; set; }
}
