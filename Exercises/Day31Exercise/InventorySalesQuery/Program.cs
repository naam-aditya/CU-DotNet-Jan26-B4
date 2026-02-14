using System.Globalization;

namespace InventorySalesQuery;

class Program
{
    static void Main(string[] args)
    {
        CultureInfo indianCulture = new("en-IN");
        List<Product> products =
        [
            new Product { 
                Id = 1, 
                Name = "Laptop", 
                Category = "Electronics", 
                Price = 50000
            },
            new Product { 
                Id = 2, 
                Name = "Phone", 
                Category = "Electronics", 
                Price = 20000
            },
            new Product { 
                Id = 3, 
                Name = "Table", 
                Category = "Furniture", 
                Price = 5000
            }
        ];

        List<Sale> sales = [
            new Sale { ProductId = 1, QuantitySold = 10 },
            new Sale { ProductId = 2, QuantitySold = 20 }  
        ];
        
        Console.WriteLine(
            string.Concat(
                "PRODUCTS AND THEIR SALES:\n",
                $"{"NAME",-8}{"CATEGORY",-14}{"PRICE", 8}{"QUANTITY", 10}"
            )
        );
        products.Join(
            sales, 
            p => p.Id, 
            s => s.ProductId, 
            (p, s) => new { p.Name, p.Category, p.Price, s.QuantitySold }
        )
        .ToList()
        .ForEach(
            r => Console.WriteLine(
                string.Concat(
                    $"{r.Name, -8}",
                    $"{r.Category, -14}",
                    $"{r.Price.ToString("C0", indianCulture), 8} ",
                    $"{r.QuantitySold, 9}"
                )
            )
        );

        Console.WriteLine("\nTOTAL REVENUE BY PRODUCTS:");
        products.GroupJoin(
            sales,
            p => p.Id,
            s => s.ProductId,
            (p, group) => new 
            { 
                p.Name, 
                TotalQuantity = group.Sum(x => x.QuantitySold), 
                Revenue = group.Sum(x => x.QuantitySold) * p.Price
            }
        )
        .ToList()
        .ForEach(
            r => Console.WriteLine($"{r.Name, -8}: {r.Revenue.ToString("C0", indianCulture)}")
        );

        Console.WriteLine("\nBEST SELLING PRODUCT:");
        products.GroupJoin(
            sales,
            p => p.Id,
            s => s.ProductId,
            (p, group) => new 
            { 
                Product = p, 
                TotalQuantity = group.Sum(x => x.QuantitySold) 
            }
        )
        .OrderByDescending(r => r.TotalQuantity)
        .Take(1)
        .ToList()
        .ForEach(r => Console.WriteLine($"{r.Product.Name}: {r.TotalQuantity}"));

        Console.WriteLine("\nPRODUCT WITH NO SALES:");
        products.GroupJoin(
            sales,
            p => p.Id,
            s => s.ProductId,
            (p, group) => new { p, group }
        )
        .SelectMany(
            x => x.group.DefaultIfEmpty(),
            (x, s) => new { x.p.Name, s?.QuantitySold }
        )
        .Where(r => r.QuantitySold == null)
        .ToList()
        .ForEach(
            r => Console.WriteLine($"{r.Name}")
        );
    }
}

class Product
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Category { get; set; }
    public double Price { get; set; }
}

class Sale
{
    public int ProductId { get; set; }
    public int QuantitySold { get; set; }
}
