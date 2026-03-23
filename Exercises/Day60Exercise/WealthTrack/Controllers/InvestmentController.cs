using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WealthTrack.Models;
using WealthTrack.Models.ViewModels;

namespace WealthTrack.Controllers
{
    public class InvestmentController : Controller
    {
        private readonly PortfolioContext _context;

        public InvestmentController(PortfolioContext context)
        {
            _context = context;
        }

        // GET: Investment
        public async Task<IActionResult> Index()
        {
            return View(await _context.Investment.ToListAsync());
        }

        // GET: Investment/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var investment = await _context.Investment
                .FirstOrDefaultAsync(m => m.Id == id);
            if (investment == null)
            {
                return NotFound();
            }

            return View(investment);
        }

        // GET: Investment/Create
        [Route("Investment/Create/{id:int}")]
        public IActionResult Create(int id)
        {
            var asset = AssetController.Assets.FirstOrDefault(a => a.Id == id);
            ViewBag.Asset = asset?.AssetName;
            ViewBag.Price = asset?.Price;
            return View();
        }

        // POST: Investment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Investment/Create/{id:int}")]
        public async Task<IActionResult> Create(int id, [Bind("Id,TickerSymbol,Price,Quantity")] InvestmentCreateViewModel investmentCreateViewModel)
        {
            if (ModelState.IsValid)
            {
                var asset = AssetController.Assets.FirstOrDefault(a => a.Id == id);
                _context.Add(
                    new Investment()
                    {
                        TickerSymbol = investmentCreateViewModel.TickerSymbol,
                        AssetName = asset?.AssetName!,
                        PurchasePrice = investmentCreateViewModel.Price,
                        Quantity = investmentCreateViewModel.Quantity,
                        PurchaseDate = DateTime.Now
                    }
                );
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Asset");
            }
            return View(investmentCreateViewModel);
        }

        // GET: Investment/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var investment = await _context.Investment.FindAsync(id);
            if (investment == null)
            {
                return NotFound();
            }
            return View(investment);
        }

        // POST: Investment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TickerSymbol,AssetName,PurchasePrice,Quantity,PurchaseDate")] Investment investment)
        {
            if (id != investment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(investment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvestmentExists(investment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(investment);
        }

        // GET: Investment/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var investment = await _context.Investment
                .FirstOrDefaultAsync(m => m.Id == id);
            if (investment == null)
            {
                return NotFound();
            }

            return View(investment);
        }

        // POST: Investment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var investment = await _context.Investment.FindAsync(id);
            if (investment != null)
            {
                _context.Investment.Remove(investment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvestmentExists(int id)
        {
            return _context.Investment.Any(e => e.Id == id);
        }
    }
}
