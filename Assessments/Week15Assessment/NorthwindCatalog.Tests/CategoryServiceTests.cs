using Moq;
using NorthwindCatalog.Services.Dtos;
using NorthwindCatalog.Services.Repositories.Interfaces;
using NorthwindCatalog.Services.Services;

namespace NorthwindCatalog.Tests;

/// <summary>
/// Unit tests for CategoryService
/// </summary>
public class CategoryServiceTests
{
    private readonly Mock<ICategoryRepository> _mockCategoryRepository;
    private readonly CategoryService _categoryService;

    public CategoryServiceTests()
    {
        _mockCategoryRepository = new Mock<ICategoryRepository>();
        _categoryService = new CategoryService(_mockCategoryRepository.Object);
    }

    [Fact]
    public async Task GetAllCategoriesAsync_ShouldReturnAllCategories()
    {
        // Arrange
        var expectedCategories = new List<CategoryDto>
        {
            new() { CategoryId = 1, CategoryName = "Beverages", ImageUrl = "/images/category-1.jpg" },
            new() { CategoryId = 2, CategoryName = "Produce", ImageUrl = "/images/category-2.jpg" },
            new() { CategoryId = 3, CategoryName = "Dairy", ImageUrl = "/images/category-3.jpg" }
        };

        _mockCategoryRepository
            .Setup(x => x.GetAllAsync())
            .ReturnsAsync(expectedCategories);

        // Act
        var result = await _categoryService.GetAllCategoriesAsync();

        // Assert
        Assert.Equal(3, result.Count());
        Assert.Contains(result, c => c.CategoryName == "Beverages");
        Assert.Contains(result, c => c.CategoryName == "Produce");
        _mockCategoryRepository.Verify(x => x.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task GetAllCategoriesAsync_ShouldReturnEmptyList_WhenNoCategoriesExist()
    {
        // Arrange
        var emptyList = new List<CategoryDto>();

        _mockCategoryRepository
            .Setup(x => x.GetAllAsync())
            .ReturnsAsync(emptyList);

        // Act
        var result = await _categoryService.GetAllCategoriesAsync();

        // Assert
        Assert.Empty(result);
    }
}

/// <summary>
/// Integration tests for CategorySummaryDto calculations
/// </summary>
public class CategorySummaryTests
{
    [Fact]
    public void CategorySummaryDto_ShouldStoreAnalyticsData()
    {
        // Arrange & Act
        var summary = new CategorySummaryDto
        {
            CategoryName = "Fruits",
            ProductCount = 15,
            AvgPrice = 5.50m,
            MostExpensiveProduct = "Premium Imported Fruits"
        };

        // Assert
        Assert.Equal("Fruits", summary.CategoryName);
        Assert.Equal(15, summary.ProductCount);
        Assert.Equal(5.50m, summary.AvgPrice);
        Assert.Equal("Premium Imported Fruits", summary.MostExpensiveProduct);
    }

    [Fact]
    public void CategorySummaryDto_ShouldHandleZeroValues()
    {
        // Arrange & Act
        var summary = new CategorySummaryDto
        {
            CategoryName = "Empty Category",
            ProductCount = 0,
            AvgPrice = 0,
            MostExpensiveProduct = "N/A"
        };

        // Assert
        Assert.Equal(0, summary.ProductCount);
        Assert.Equal(0, summary.AvgPrice);
    }
}
