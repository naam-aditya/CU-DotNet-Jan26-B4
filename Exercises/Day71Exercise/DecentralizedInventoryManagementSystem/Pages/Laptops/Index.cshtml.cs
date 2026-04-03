using Microsoft.AspNetCore.Mvc.RazorPages;
using DecentralizedInventoryManagementSystem.Models;
using DecentralizedInventoryManagementSystem.Services;

namespace DecentralizedInventoryManagementSystem.Pages.Laptops
{
    public class IndexModel : PageModel
    {
        private readonly ILaptopService _laptopService;

        public IList<Laptop> Laptops { get; set; } = [];

        public IndexModel(ILaptopService laptopService)
        {
            _laptopService = laptopService;
        }

        public async Task OnGetAsync()
        {
            Laptops = await _laptopService.GetLaptopsAsync();
        }
    }
}