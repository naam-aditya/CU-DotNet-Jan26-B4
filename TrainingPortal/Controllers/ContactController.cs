using Microsoft.AspNetCore.Mvc;

namespace TrainingPortal.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}