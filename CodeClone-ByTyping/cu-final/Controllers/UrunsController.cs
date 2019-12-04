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
    public class UrunsController : Controller
    {
        private readonly MarketContext _context;

        public UrunsController(MarketContext context)
        {
            _context = context;
        }

        // GET: Uruns
        public async Task<IActionResult> Index()
        {
            var Uruns = _context.Uruns
                .Include(c => c.Kategori)
                .AsNoTracking();
            return View(await Uruns.ToListAsync());
        }

        // GET: Uruns/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Urun = await _context.Uruns
                .Include(c => c.Kategori)
                .Include(c=>c.Envanters)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.UrunID == id);
            if (Urun == null)
            {
                return NotFound();
            }

            return View(Urun);
        }

        // GET: Uruns/Create
        public IActionResult Create()
        {
            PopulateKategorisDropDownList();
            return View();
        }

        // POST: Uruns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UrunID,Credits,KategoriID,Title")] Urun Urun)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Urun);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateKategorisDropDownList(Urun.KategoriID);
            return View(Urun);
        }

        // GET: Uruns/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Urun = await _context.Uruns
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.UrunID == id);
            if (Urun == null)
            {
                return NotFound();
            }
            PopulateKategorisDropDownList(Urun.KategoriID);
            return View(Urun);
        }

        // POST: Uruns/Edit/5
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

            var UrunToUpdate = await _context.Uruns
                .FirstOrDefaultAsync(c => c.UrunID == id);

            if (await TryUpdateModelAsync<Urun>(UrunToUpdate,
                "",
                c => c.Credits, c => c.KategoriID, c => c.Title))
            {
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
                return RedirectToAction(nameof(Index));
            }
            PopulateKategorisDropDownList(UrunToUpdate.KategoriID);
            return View(UrunToUpdate);
        }

        private void PopulateKategorisDropDownList(object selectedKategori = null)
        {
            var KategorisQuery = from d in _context.Kategoris
                                   orderby d.Name
                                   select d;
            ViewBag.KategoriID = new SelectList(KategorisQuery.AsNoTracking(), "KategoriID", "Name", selectedKategori);
        }

        // GET: Uruns/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Urun = await _context.Uruns
                .Include(c => c.Kategori)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.UrunID == id);
            if (Urun == null)
            {
                return NotFound();
            }

            return View(Urun);
        }

        // POST: Uruns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Urun = await _context.Uruns.FindAsync(id);
            _context.Uruns.Remove(Urun);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult UpdateUrunCredits()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUrunCredits(int? multiplier)
        {
            if (multiplier != null)
            {
                ViewData["RowsAffected"] =
                    await _context.Database.ExecuteSqlCommandAsync(
                        "UPDATE Urun SET Credits = Credits * {0}",
                        parameters: multiplier);
            }
            return View();
        }

        private bool UrunExists(int id)
        {
            return _context.Uruns.Any(e => e.UrunID == id);
        }
    }
}
