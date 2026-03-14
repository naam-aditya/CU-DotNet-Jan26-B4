using FinTrackPro.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinTrackPro.Controllers
{
    public class PortfolioController : Controller
    {
        private readonly static List<Asset> _assets = [
            new() { Id=1, Name="A1", Price=800, Quantity=20 },
            new() { Id=2, Name="A2", Price=700, Quantity=87 },
            new() { Id=3, Name="A3", Price=600, Quantity=37 },
            new() { Id=4, Name="A4", Price=500, Quantity=55 },
            new() { Id=5, Name="A5", Price=400, Quantity=78 },
            new() { Id=6, Name="A6", Price=300, Quantity=80 },
            new() { Id=7, Name="A7", Price=200, Quantity=10 }
        ];

        public static List<Asset> Assets { get => _assets; }

        // GET: PortfolioController
        public ActionResult Index()
        {
            return View(_assets);
        }

        // GET: PortfolioController/Details/5
        [Route("Assets/Info/{id:int}")]
        public ActionResult Details(int id)
        {
            var asset = _assets.Find(a => a.Id == id);
            return View(asset);
        }

        // GET: PortfolioController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PortfolioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PortfolioController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PortfolioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PortfolioController/Delete/5
        public ActionResult Delete(int id)
        {
            var asset = _assets.Find(a => a.Id == id);
            return View(asset);
        }

        // POST: PortfolioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var count = _assets.RemoveAll(a => a.Id == id);
                if (count == 0)
                    TempData["Message"] = "Failed";
                else
                    TempData["Message"] = "Failed";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
