using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NorthwindCatalog.Services.Data;
using NorthwindCatalog.Services.Dtos;
using NorthwindCatalog.Services.Repositories.Interfaces;

namespace NorthwindCatalog.Services.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly NorthwindContext _context;
    private readonly IMapper _mapper;

    public ProductRepository(NorthwindContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductDto>> GetByCategoryIdAsync(int categoryId)
    {
        var products = await _context.Products
            .Where(p => p.CategoryId == categoryId && !p.Discontinued)
            .ToListAsync();
        
        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }

    public async Task<IEnumerable<CategorySummaryDto>> GetCategorySummariesAsync()
    {
        var summaries = await _context.Categories
            .Include(c => c.Products)
            .Where(c => c.Products.Any(p => !p.Discontinued))
            .Select(c => new CategorySummaryDto
            {
                CategoryName = c.CategoryName,
                ProductCount = c.Products.Count(p => !p.Discontinued),
                AvgPrice = c.Products
                    .Where(p => !p.Discontinued)
                    .Average(p => p.UnitPrice ?? 0),
                MostExpensiveProduct = c.Products
                    .Where(p => !p.Discontinued)
                    .OrderByDescending(p => p.UnitPrice)
                    .Select(p => p.ProductName)
                    .FirstOrDefault() ?? "N/A"
            })
            .OrderBy(s => s.CategoryName)
            .ToListAsync();

        return summaries;
    }
}
