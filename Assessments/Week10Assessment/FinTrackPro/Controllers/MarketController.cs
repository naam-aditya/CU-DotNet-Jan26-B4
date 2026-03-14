using Microsoft.AspNetCore.Mvc;

namespace FinTrackPro.Controllers
{
    public class MarketController : Controller
    {
        // GET: MarketController
        public ActionResult Index()
        {
            ViewBag["Market Status"] = "OPEN";
            ViewBag["Top Gainer"] = "A1";
            ViewBag["Volume"] = 12000;

            return View();
        }

        [HttpGet("Analyze/{ticker}/{days:int?}")]
        public IActionResult Analyze(string ticker, int? days)
        {
            int analysisDays = days ?? 30;

            ViewBag.Ticker = ticker;
            ViewBag.Days = analysisDays;
            return View();
        }
    }
}
