namespace CustomerOrderAnalysis;

class Program
{
    static void Main(string[] args)
    {
        List<Customer> customers =
        [
            new Customer { Id = 1, Name = "Ajay", City = "Delhi" },
            new Customer { Id = 2, Name = "Sunita", City = "Mumbai" }
        ];

        List<Order> orders = 
        [
            new Order { Id = 1, CustomerId = 1, Amount = 20000 },
            new Order { Id = 2, CustomerId = 1, Amount = 40000 }
        ];

        Console.WriteLine("TOTAL ORDERS PER CUSTOMER:");
        customers.GroupJoin(
            orders,
            c => c.Id,
            s => s.CustomerId,
            (c, group) => new { c.Name, Count = group.Count() }
        )
        .ToList()
        .ForEach(r => Console.WriteLine($"{r.Name}: {r.Count}"));

        Console.WriteLine("\nCUSTOMERS WITH NO ORDER:");
        customers.GroupJoin(
            orders,
            c => c.Id,
            o => o.CustomerId,
            (c, group) => new { c.Name, group }
        )
        .Where(x => !x.group.Any())
        .ToList()
        .ForEach(r => Console.WriteLine($"{r.Name}"));

        Console.WriteLine("\nCUTOMERS WITH SPENDING OVER 50,000:");
        customers.GroupJoin(
            orders,
            c => c.Id,
            o => o.CustomerId,
            (c, group) => new { c.Name, Total = group.Sum(x => x.Amount) }
        )
        .Where(r => r.Total > 50-000)
        .ToList()
        .ForEach(r => Console.WriteLine($"{r.Name}: {r.Total}"));

        Console.WriteLine();
        customers.GroupJoin(
            orders,
            c => c.Id,
            o => o.CustomerId,
            (c, group) => new { c.Name, Total = group.Sum(x => x.Amount) }
        )
        .OrderByDescending(x => x.Total)
        .ToList()
        .ForEach(r => Console.WriteLine($"{r.Name}: {r.Total}"));
    }
}

class Customer
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? City { get; set; }
}

class Order
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public double Amount { get; set; }
    public DateTime OrderDate { get; set; }
}
