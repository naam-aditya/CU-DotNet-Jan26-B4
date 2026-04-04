using Microsoft.AspNetCore.Mvc;
using Vagabond.Mvc.Services;

namespace Vagabond.Mvc.Controllers
{
    public class TravelController : Controller
    {
        private readonly IDestinationService _service;
        public TravelController(IDestinationService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index() =>
            View(await _service.GetAllAsync());
    }
}