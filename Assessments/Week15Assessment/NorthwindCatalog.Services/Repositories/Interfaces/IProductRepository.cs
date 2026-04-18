using NorthwindCatalog.Services.Dtos;

namespace NorthwindCatalog.Services.Repositories.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<ProductDto>> GetByCategoryIdAsync(int categoryId);
    Task<IEnumerable<CategorySummaryDto>> GetCategorySummariesAsync();
}
