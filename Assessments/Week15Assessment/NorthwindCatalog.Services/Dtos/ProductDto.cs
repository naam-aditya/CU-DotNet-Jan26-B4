namespace NorthwindCatalog.Services.Dtos;

public class ProductDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public short UnitsInStock { get; set; }
    
    /// <summary>
    /// Calculated field: UnitPrice × UnitsInStock
    /// </summary>
    public decimal InventoryValue => UnitPrice * UnitsInStock;
}
