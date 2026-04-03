using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DecentralizedInventoryManagementSystem.Models;
using DecentralizedInventoryManagementSystem.Services;

namespace DecentralizedInventoryManagementSystem.Pages.Laptops
{
    public class CreateModel : PageModel
    {
        private readonly ILaptopService _laptopService;

        [BindProperty]
        public Laptop Laptop { get; set; } = new();

        public CreateModel(ILaptopService laptopService)
        {
            _laptopService = laptopService;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            await _laptopService.CreateLaptopAsync(Laptop);

            TempData["success"] = "Laptop added successfully";

            return RedirectToPage("Index");
        }
    }
}