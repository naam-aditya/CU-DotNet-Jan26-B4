using Microsoft.AspNetCore.Mvc;
using NorthwindCatalog.Services.Services.Interfaces;

namespace NorthwindCatalog.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(IProductService productService, ILogger<ProductsController> logger)
    {
        _productService = productService;
        _logger = logger;
    }

    /// <summary>
    /// Get products by category ID
    /// </summary>
    /// <param name="categoryId">The category ID</param>
    /// <returns>List of products in the specified category</returns>
    [HttpGet("by-category/{categoryId}")]
    public async Task<IActionResult> GetByCategoryId(int categoryId)
    {
        try
        {
            var products = await _productService.GetProductsByCategoryAsync(categoryId);
            return Ok(products);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving products for category {CategoryId}", categoryId);
            return StatusCode(500, new { message = "An error occurred while retrieving products." });
        }
    }

    /// <summary>
    /// Get category summaries with analytics
    /// </summary>
    /// <returns>List of category summaries with product count, average price, and most expensive product</returns>
    [HttpGet("summary")]
    public async Task<IActionResult> GetSummaries()
    {
        try
        {
            var summaries = await _productService.GetCategorySummariesAsync();
            return Ok(summaries);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving category summaries");
            return StatusCode(500, new { message = "An error occurred while retrieving summaries." });
        }
    }
}
