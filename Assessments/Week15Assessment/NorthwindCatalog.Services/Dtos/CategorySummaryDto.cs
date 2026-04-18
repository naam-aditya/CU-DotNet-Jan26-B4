namespace NorthwindCatalog.Services.Dtos;

public class CategorySummaryDto
{
    public string CategoryName { get; set; } = string.Empty;
    public int ProductCount { get; set; }
    public decimal AvgPrice { get; set; }
    public string MostExpensiveProduct { get; set; } = string.Empty;
}
