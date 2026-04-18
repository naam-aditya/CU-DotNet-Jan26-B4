using NorthwindCatalog.Services.Dtos;
using NorthwindCatalog.Services.Repositories.Interfaces;
using NorthwindCatalog.Services.Services.Interfaces;

namespace NorthwindCatalog.Services.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(int categoryId)
    {
        return await _productRepository.GetByCategoryIdAsync(categoryId);
    }

    public async Task<IEnumerable<CategorySummaryDto>> GetCategorySummariesAsync()
    {
        return await _productRepository.GetCategorySummariesAsync();
    }
}
