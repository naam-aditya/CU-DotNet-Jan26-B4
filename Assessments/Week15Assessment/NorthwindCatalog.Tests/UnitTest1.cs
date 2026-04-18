using NorthwindCatalog.Services.Dtos;

namespace NorthwindCatalog.Tests;

/// <summary>
/// Unit tests for ProductDto InventoryValue calculation
/// </summary>
public class ProductDtoTests
{
    [Fact]
    public void InventoryValue_ShouldCalculateCorrectly_WithValidInputs()
    {
        // Arrange
        var product = new ProductDto
        {
            ProductId = 1,
            ProductName = "Test Product",
            UnitPrice = 10.50m,
            UnitsInStock = 100
        };

        // Act
        var inventoryValue = product.InventoryValue;

        // Assert
        Assert.Equal(1050.00m, inventoryValue);
    }

    [Fact]
    public void InventoryValue_ShouldReturnZero_WithZeroUnits()
    {
        // Arrange
        var product = new ProductDto
        {
            ProductId = 2,
            ProductName = "Empty Stock",
            UnitPrice = 25.00m,
            UnitsInStock = 0
        };

        // Act
        var inventoryValue = product.InventoryValue;

        // Assert
        Assert.Equal(0, inventoryValue);
    }

    [Fact]
    public void InventoryValue_ShouldReturnZero_WithZeroPrice()
    {
        // Arrange
        var product = new ProductDto
        {
            ProductId = 3,
            ProductName = "Free Product",
            UnitPrice = 0,
            UnitsInStock = 500
        };

        // Act
        var inventoryValue = product.InventoryValue;

        // Assert
        Assert.Equal(0, inventoryValue);
    }

    [Fact]
    public void InventoryValue_ShouldHandleDecimalPrecision()
    {
        // Arrange
        var product = new ProductDto
        {
            ProductId = 4,
            ProductName = "Precision Test",
            UnitPrice = 10.75m,
            UnitsInStock = 33
        };

        // Act
        var inventoryValue = product.InventoryValue;

        // Assert
        Assert.Equal(354.75m, inventoryValue);
    }
}