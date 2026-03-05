namespace CargoManifestOptimizer;

class Program
{
    static void Main(string[] args)
    {
        List<List<Container>> cargoBay = [
            [
                new(
                    "C001", 
                    [
                        new("Laptop", 2.5, "Tech"),
                        new("Monitor", 5.0, "Tech"),
                        new("Smartphone", 0.5, "Tech")
                    ]
                ),
                new(
                    "C104",
                    [
                        new("Server Rack", 45.0, "Tech"),
                        new("Cables", 1.2, "Tech"),
                    ]
                )
            ],
            [
                new(
                    "C002",
                    [
                        new("Apple", 0.2, "Food"),
                        new("Banana", 0.2, "Food"),
                        new("Milk", 1.0, "Food"),
                    ]
                ),
                new(
                    "C003",
                    [
                        new("Table", 15.0, "Furniture"),
                        new("Chair", 7.5, "Furniture"),
                    ]
                )
            ],
            [
                new(
                    "C205",
                    [
                        new("Vase", 3.0, "Decor"),
                        new("Mirror", 12.0, "Decor"),
                    ]
                ),
                new("C206", []),
            ],
            [
                new(
                    "C001", 
                    [
                        new("Laptop", 2.5, "Tech"),
                        new("Monitor", 5.0, "Tech"),
                        new("Smartphone", 0.5, "Tech")
                    ]
                ),
                new(
                    "C104",
                    [
                        new("Server Rack", 45.0, "Tech"),
                        new("Cables", 1.2, "Tech"),
                    ]
                )
            ],
            [
                new(
                    "C205",
                    [
                        new("Vase", 3.0, "Decor"),
                        new("Mirror", 12.0, "Decor"),
                        new("Lamp", 4.5, "Decor"),
                    ]
                ),
                new("C206", [])
            ],
            [
                new(
                    "C301",
                    [
                        new("Generator", 80.0, "Industrial"),
                        new("Compressor", 60.0, "Industrial"),
                    ]
                ),
                new(
                    "C302",
                    [
                        new("Steel Beam", 120.0, "Industrial"),
                        new("Drill", 8.0, "Tools"),
                    ]
                ),
            ],
            [
                new(
                    "C401",
                    [
                        new("Laptop", 2.5, "Tech"),
                        new("Apple", 0.2, "Food"),
                        new("Chair", .5, "Furniture"),
                    ]
                ),
                new(
                    "C402",
                    [
                        new("Water Bottle", 1.0, "Food"),
                        new("Drill", 8.0, "Tools"),
                    ]
                )
            ],
            [],
        ];

        CargoManager.Cargo = cargoBay;

        try
        {
            Console.WriteLine(
                string.Join(',', CargoManager.FindHeavyContainers(2))
            );

            Console.WriteLine();
            Console.WriteLine(
                string.Join(
                    '\n', 
                    CargoManager.GetItemCountByCategory()
                        .Select(r => $"{r.Key}: {r.Value}")
                )
            );
            
            Console.WriteLine();
            Console.WriteLine(
                string.Join(
                    '\n', 
                    CargoManager.FlattenAndSortShipment()
                        .Select(i => $"{i.Name} - {i.Weight} - {i.Category}")
                )
            );
        }
        catch (NullReferenceException ex)
        {
            Console.WriteLine($"ERROR OCCURED: {ex.Message}");
        }

    }
}

public class Item(string name, double weight, string category)
{
    public string Name { get; set; } = name;
    public double Weight { get; set; } = weight;
    public string Category { get; set; } = category;
}

public class Container(string id, List<Item> items)
{
    public string Id { get; set; } = id;
    public List<Item> Items { get; set; } = items;
}

public class CargoManager
{
    public static List<List<Container>> Cargo { get; set; } = [];
    public static List<string> FindHeavyContainers(double weightThreshold)
        => Cargo
            .SelectMany(c => c)
            .Where(
                c => c.Items.Sum(i => i.Weight) > weightThreshold
            )
            .Select(c => c.Id)
            .ToList();
    
    public static Dictionary<string, int> GetItemCountByCategory()
        => Cargo
            .SelectMany(c => c)
            .SelectMany(c => c.Items)
            .GroupBy(i => i.Category)
            .ToDictionary(
                g => g.Key, 
                g => g.Count()
            );
    
    public static List<Item> FlattenAndSortShipment()
        => Cargo
            .SelectMany(c => c)
            .SelectMany(c => c.Items)
            .DistinctBy(i => i.Name)
            .OrderBy(i => i.Category)
            .ThenByDescending(i => i.Weight)
            .ToList();
}