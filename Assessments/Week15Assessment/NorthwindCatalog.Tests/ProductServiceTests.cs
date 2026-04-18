using Moq;
using NorthwindCatalog.Services.Dtos;
using NorthwindCatalog.Services.Repositories.Interfaces;
using NorthwindCatalog.Services.Services;

namespace NorthwindCatalog.Tests;

/// <summary>
/// Unit tests for ProductService
/// </summary>
public class ProductServiceTests
{
    private readonly Mock<IProductRepository> _mockProductRepository;
    private readonly ProductService _productService;

    public ProductServiceTests()
    {
        _mockProductRepository = new Mock<IProductRepository>();
        _productService = new ProductService(_mockProductRepository.Object);
    }

    [Fact]
    public async Task GetProductsByCategoryAsync_ShouldReturnProducts_WhenCategoryExists()
    {
        // Arrange
        int categoryId = 1;
        var expectedProducts = new List<ProductDto>
        {
            new() { ProductId = 1, ProductName = "Product 1", UnitPrice = 10, UnitsInStock = 100 },
            new() { ProductId = 2, ProductName = "Product 2", UnitPrice = 20, UnitsInStock = 50 }
        };

        _mockProductRepository
            .Setup(x => x.GetByCategoryIdAsync(categoryId))
            .ReturnsAsync(expectedProducts);

        // Act
        var result = await _productService.GetProductsByCategoryAsync(categoryId);

        // Assert
        Assert.Equal(2, result.Count());
        Assert.Contains(result, p => p.ProductName == "Product 1");
        _mockProductRepository.Verify(x => x.GetByCategoryIdAsync(categoryId), Times.Once);
    }

    [Fact]
    public async Task GetProductsByCategoryAsync_ShouldReturnEmptyList_WhenNoProductsExist()
    {
        // Arrange
        int categoryId = 999;
        var emptyList = new List<ProductDto>();

        _mockProductRepository
            .Setup(x => x.GetByCategoryIdAsync(categoryId))
            .ReturnsAsync(emptyList);

        // Act
        var result = await _productService.GetProductsByCategoryAsync(categoryId);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetCategorySummariesAsync_ShouldReturnSummaries()
    {
        // Arrange
        var expectedSummaries = new List<CategorySummaryDto>
        {
            new() { CategoryName = "Beverages", ProductCount = 12, AvgPrice = 15.50m, MostExpensiveProduct = "Premium Coffee" },
            new() { CategoryName = "Produce", ProductCount = 8, AvgPrice = 8.75m, MostExpensiveProduct = "Organic Vegetables" }
        };

        _mockProductRepository
            .Setup(x => x.GetCategorySummariesAsync())
            .ReturnsAsync(expectedSummaries);

        // Act
        var result = await _productService.GetCategorySummariesAsync();

        // Assert
        Assert.Equal(2, result.Count());
        Assert.Equal("Beverages", result.First().CategoryName);
        _mockProductRepository.Verify(x => x.GetCategorySummariesAsync(), Times.Once);
    }
}
