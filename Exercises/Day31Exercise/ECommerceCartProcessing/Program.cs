using System.Globalization;

namespace ECommerceCartProcessing;

class Program
{
    static void Main(string[] args)
    {
        CultureInfo indianCulture = new("en-IN");
        List<CartItem> cart = [
            new CartItem { Name = "TV", Category = "Electronics", Price = 30000, Quantity = 1 },
            new CartItem { Name = "Sofa", Category = "Furniture", Price = 15000, Quantity = 1 }
        ];

        Console.WriteLine(
            $"TOTAL CART VALUE: {cart.Sum(c => c.Price).ToString("C2", indianCulture)}");

        Console.WriteLine("\nGROUP BY CATEGORY:");
        cart.GroupBy(c => c.Category)
            .Select(g => new { g.Key, Total = g.Sum(c => c.Price) })
            .ToList()
            .ForEach(r => Console.WriteLine($"{r.Key}: {r.Total.ToString("C2", indianCulture)}"));

        Console.WriteLine("\nDISCOUNTS ON ELECTRONIC ITEMS:");
        cart.Where(c => c.Category == "Electronics")
            .Select(c => new { Cart = c, Discount = c.Price * 0.10 })
            .ToList()
            .ForEach(r => Console.WriteLine($"{r.Cart.Name}: {r.Discount.ToString("C2", indianCulture)}"));

        Console.WriteLine("\nCART SUMMARY:");
        cart.ForEach(Console.WriteLine);
    }
}

class CartItem
{
    public string? Name { get; set; }
    public string? Category { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }

    public override string ToString() =>
        $"{Name} - {Category} - {Price:N2} - {Quantity}";
}