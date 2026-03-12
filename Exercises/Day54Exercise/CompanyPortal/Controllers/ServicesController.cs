using Microsoft.AspNetCore.Mvc;

namespace CompanyPortal.Controllers;

public class ServicesController : Controller
{
    // GET: ServicesController
    public ActionResult Index()
    {
        return View();
    }

    public IActionResult Consulting()
    {
        return View();
    }

    public IActionResult Training()
    {
        return View();
    }
}
