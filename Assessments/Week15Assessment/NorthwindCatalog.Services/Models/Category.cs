namespace NorthwindCatalog.Services.Models;

public class Category
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public byte[]? Picture { get; set; }

    // Navigation property
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
