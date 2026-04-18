using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NorthwindCatalog.Web.Models;
using System.Net.Http;
using NorthwindCatalog.Services.Dtos;
using System.Text.Json;

namespace NorthwindCatalog.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> Categories()
    {
        try
        {
            var baseUrl = _configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7001";
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{baseUrl}/api/categories");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var categories = JsonSerializer.Deserialize<List<CategoryDto>>(json, options);
                return View(categories ?? new List<CategoryDto>());
            }
            else
            {
                _logger.LogError("Failed to fetch categories from API. Status: {StatusCode}", response.StatusCode);
                return View(new List<CategoryDto>());
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching categories");
            return View(new List<CategoryDto>());
        }
    }

    public async Task<IActionResult> Products(int categoryId)
    {
        try
        {
            var baseUrl = _configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7001";
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{baseUrl}/api/products/by-category/{categoryId}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var products = JsonSerializer.Deserialize<List<ProductDto>>(json, options);
                ViewBag.CategoryId = categoryId;
                return View(products ?? new List<ProductDto>());
            }
            else
            {
                _logger.LogError("Failed to fetch products from API. Status: {StatusCode}", response.StatusCode);
                ViewBag.CategoryId = categoryId;
                return View(new List<ProductDto>());
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching products");
            ViewBag.CategoryId = categoryId;
            return View(new List<ProductDto>());
        }
    }

    public async Task<IActionResult> Summary()
    {
        try
        {
            var baseUrl = _configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7001";
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{baseUrl}/api/products/summary");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var summaries = JsonSerializer.Deserialize<List<CategorySummaryDto>>(json, options);
                return View(summaries ?? new List<CategorySummaryDto>());
            }
            else
            {
                _logger.LogError("Failed to fetch summaries from API. Status: {StatusCode}", response.StatusCode);
                return View(new List<CategorySummaryDto>());
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching summaries");
            return View(new List<CategorySummaryDto>());
        }
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
