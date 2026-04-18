using NorthwindCatalog.Services.Dtos;

namespace NorthwindCatalog.Services.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(int categoryId);
    Task<IEnumerable<CategorySummaryDto>> GetCategorySummariesAsync();
}
