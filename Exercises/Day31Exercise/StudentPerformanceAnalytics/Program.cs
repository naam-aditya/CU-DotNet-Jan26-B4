namespace StudentPerformanceAnalytics;

class Program
{
    static void Main(string[] args)
    {
        List<Student> students =  
        [
            new Student { Id = 1, Name = "Amit", Class = "10A", Marks = 85 },
            new Student { Id = 2, Name = "Neha", Class = "10A", Marks = 72 },
            new Student { Id = 3, Name = "Rahul", Class = "10B", Marks = 90 },
            new Student { Id = 4, Name = "Pooja", Class = "10B", Marks = 60 },
            new Student { Id = 5, Name = "Kiran", Class = "10A", Marks = 95 },
        ];

        // Top three Students

        Console.WriteLine("TOP THREE STUDENTS:");
        students.OrderByDescending(x => x.Marks)
            .Take(3)
            .ToList()
            .ForEach(x => Console.WriteLine($"{x.Name, -8}: {x.Marks}"));
        
        // Average marks by Class

        Console.WriteLine("\nAVERAGE MARKS BY CLASS:");
        students.GroupBy(x => x.Class)
            .Select(x => new { Class = x.Key, Average = x.Average(x => x.Marks) })
            .ToList()
            .ForEach(x => Console.WriteLine($"Class {x.Class}: {x.Average}"));
        
        // Below average students by class

        Console.WriteLine("\nSTUDENTS BELOW CLASS AVERAGE:");
        students.GroupBy(s => s.Class)
            .SelectMany(g =>
            {
                var avg = g.Average(s => s.Marks);
                return g.Where(s => s.Marks < avg);
            })
            .ToList()
            .ForEach(s => Console.WriteLine($"{s.Name, -8}: {s.Class}, {s.Marks}"));
        
        // Order by Class and Marks

        Console.WriteLine("\nORDER BY CLASS, MARKS");
        students.OrderBy(s => s.Class)
            .ThenByDescending(s => s.Marks)
            .ToList()
            .ForEach(s => Console.WriteLine($"{s.Name, -8}: {s.Class}, {s.Marks}"));
    }

    class Student
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Class { get; set; }
        public int Marks { get; set; }

        public override string ToString() =>
            $"[Id: {Id}, Name: {Name}, Class: {Class}, Marks: {Marks}]";
    }
}
