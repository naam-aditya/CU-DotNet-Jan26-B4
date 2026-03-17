using Microsoft.AspNetCore.Mvc;
using QuickLoan.Models;

namespace QuickLoan.Controllers
{
    public class LoanController : Controller
    {
        private static List<Loan> _loans = [];

        // GET: LoanController
        public ActionResult Index()
        {
            return View(_loans);
        }

        // GET: LoanController/Details/5
        public ActionResult Details(int id)
        {
            var loan = _loans.FirstOrDefault(x => x.Id == id);
            return View(loan);
        }

        // GET: LoanController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoanController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Loan loan)
        {
            try
            {
                loan.Id = Loan.NextId;
                _loans.Add(loan);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LoanController/Edit/5
        public ActionResult Edit(int id)
        {
            var loan = _loans.FirstOrDefault(x => x.Id == id);
            return View(loan);
        }

        // POST: LoanController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Loan loan)
        {
            try
            {
                var tmp = _loans.FirstOrDefault(x => x.Id == id);
                if (tmp != null)
                {
                    tmp.BorrowerName = loan.BorrowerName;
                    tmp.LenderName = loan.LenderName;
                    tmp.Amount = loan.Amount;
                    tmp.IsSettled = loan.IsSettled;
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LoanController/Delete/5
        public ActionResult Delete(int id)
        {
            var loan = _loans.FirstOrDefault(x => x.Id == id);
            return View(loan);
        }

        // POST: LoanController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Loan loan)
        {
            try
            {
                _loans.RemoveAll(x => x.Id == id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
