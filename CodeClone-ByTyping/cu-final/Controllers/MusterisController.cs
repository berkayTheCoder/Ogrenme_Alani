using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BerkayMarket.Data;
using BerkayMarket.Models;

namespace BerkayMarket.Controllers
{
    public class MusterisController : Controller
    {
        private readonly MarketContext _context;

        public MusterisController(MarketContext context)
        {
            _context = context;
        }

        // GET: Musteris
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var Musteris = from s in _context.Musteris
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                Musteris = Musteris.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstMidName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    Musteris = Musteris.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    Musteris = Musteris.OrderBy(s => s.SiparisDate);
                    break;
                case "date_desc":
                    Musteris = Musteris.OrderByDescending(s => s.SiparisDate);
                    break;
                default:
                    Musteris = Musteris.OrderBy(s => s.LastName);
                    break;
            }

            int pageSize = 3;
            return View(await PaginatedList<Musteri>.CreateAsync(Musteris.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Musteris/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Musteri = await _context.Musteris
                 .Include(s => s.Sipariss)
                     .ThenInclude(e => e.Urun)
                 .AsNoTracking()
                 .FirstOrDefaultAsync(m => m.ID == id);

            if (Musteri == null)
            {
                return NotFound();
            }

            return View(Musteri);
        }

        // GET: Musteris/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Musteris/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("SiparisDate,FirstMidName,LastName")] Musteri Musteri)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(Musteri);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(Musteri);
        }

        // GET: Musteris/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Musteri = await _context.Musteris.FindAsync(id);
            if (Musteri == null)
            {
                return NotFound();
            }
            return View(Musteri);
        }

        // POST: Musteris/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var MusteriToUpdate = await _context.Musteris.FirstOrDefaultAsync(s => s.ID == id);
            if (await TryUpdateModelAsync<Musteri>(
                MusteriToUpdate,
                "",
                s => s.FirstMidName, s => s.LastName, s => s.SiparisDate))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return View(MusteriToUpdate);
        }

        // GET: Musteris/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Musteri = await _context.Musteris
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (Musteri == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(Musteri);
        }
        // POST: Musteris/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Musteri = await _context.Musteris.FindAsync(id);
            if (Musteri == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Musteris.Remove(Musteri);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        private bool MusteriExists(int id)
        {
            return _context.Musteris.Any(e => e.ID == id);
        }
    }
}
