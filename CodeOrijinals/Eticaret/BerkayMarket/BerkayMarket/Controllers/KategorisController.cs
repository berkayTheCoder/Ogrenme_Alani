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
    public class KategorisController : Controller
    {
        private readonly MarketContext _context;

        public KategorisController(MarketContext context)
        {
            _context = context;
        }

        // GET: Kategoris
        public async Task<IActionResult> Index()
        {
            var MarketContext = _context.Kategoris.Include(d => d.Administrator);
            return View(await MarketContext.ToListAsync());
        }

        // GET: Kategoris/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            string query = "SELECT * FROM Kategori WHERE KategoriID = {0}";
            var Kategori = await _context.Kategoris
                .FromSql(query, id)
                .Include(d => d.Administrator)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Kategori == null)
            {
                return NotFound();
            }

            return View(Kategori);
        }

        // GET: Kategoris/Create
        public IActionResult Create()
        {
            ViewData["SaticiID"] = new SelectList(_context.Saticis, "ID", "FullName");
            return View();
        }

        // POST: Kategoris/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KategoriID,Name,Budget,StartDate,SaticiID,RowVersion")] Kategori Kategori)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Kategori);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SaticiID"] = new SelectList(_context.Saticis, "ID", "FullName", Kategori.SaticiID);
            return View(Kategori);
        }

        // GET: Kategoris/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Kategori = await _context.Kategoris
                .Include(i => i.Administrator)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.KategoriID == id);

            if (Kategori == null)
            {
                return NotFound();
            }
            ViewData["SaticiID"] = new SelectList(_context.Saticis, "ID", "FullName", Kategori.SaticiID);
            return View(Kategori);
        }

        // POST: Kategoris/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, byte[] rowVersion)
        {
            if (id == null)
            {
                return NotFound();
            }

            var KategoriToUpdate = await _context.Kategoris.Include(i => i.Administrator).FirstOrDefaultAsync(m => m.KategoriID == id);

            if (KategoriToUpdate == null)
            {
                Kategori deletedKategori = new Kategori();
                await TryUpdateModelAsync(deletedKategori);
                ModelState.AddModelError(string.Empty,
                    "Unable to save changes. The Kategori was deleted by another user.");
                ViewData["SaticiID"] = new SelectList(_context.Saticis, "ID", "FullName", deletedKategori.SaticiID);
                return View(deletedKategori);
            }

            _context.Entry(KategoriToUpdate).Property("RowVersion").OriginalValue = rowVersion;

            if (await TryUpdateModelAsync<Kategori>(
                KategoriToUpdate,
                "",
                s => s.Name, s => s.StartDate, s => s.Budget, s => s.SaticiID))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var exceptionEntry = ex.Entries.Single();
                    var clientValues = (Kategori)exceptionEntry.Entity;
                    var databaseEntry = exceptionEntry.GetDatabaseValues();
                    if (databaseEntry == null)
                    {
                        ModelState.AddModelError(string.Empty,
                            "Unable to save changes. The Kategori was deleted by another user.");
                    }
                    else
                    {
                        var databaseValues = (Kategori)databaseEntry.ToObject();

                        if (databaseValues.Name != clientValues.Name)
                        {
                            ModelState.AddModelError("Name", $"Current value: {databaseValues.Name}");
                        }
                        if (databaseValues.Budget != clientValues.Budget)
                        {
                            ModelState.AddModelError("Budget", $"Current value: {databaseValues.Budget:c}");
                        }
                        if (databaseValues.StartDate != clientValues.StartDate)
                        {
                            ModelState.AddModelError("StartDate", $"Current value: {databaseValues.StartDate:d}");
                        }
                        if (databaseValues.SaticiID != clientValues.SaticiID)
                        {
                            Satici databaseSatici = await _context.Saticis.FirstOrDefaultAsync(i => i.ID == databaseValues.SaticiID);
                            ModelState.AddModelError("SaticiID", $"Current value: {databaseSatici?.FullName}");
                        }

                        ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                                + "was modified by another user after you got the original value. The "
                                + "edit operation was canceled and the current values in the database "
                                + "have been displayed. If you still want to edit this record, click "
                                + "the Save button again. Otherwise click the Back to List hyperlink.");
                        KategoriToUpdate.RowVersion = (byte[])databaseValues.RowVersion;
                        ModelState.Remove("RowVersion");
                    }
                }
            }
            ViewData["SaticiID"] = new SelectList(_context.Saticis, "ID", "FullName", KategoriToUpdate.SaticiID);
            return View(KategoriToUpdate);
        }

        // GET: Kategoris/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? concurrencyError)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Kategori = await _context.Kategoris
                .Include(d => d.Administrator)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.KategoriID == id);
            if (Kategori == null)
            {
                if (concurrencyError.GetValueOrDefault())
                {
                    return RedirectToAction(nameof(Index));
                }
                return NotFound();
            }

            if (concurrencyError.GetValueOrDefault())
            {
                ViewData["ConcurrencyErrorMessage"] = "The record you attempted to delete "
                    + "was modified by another user after you got the original values. "
                    + "The delete operation was canceled and the current values in the "
                    + "database have been displayed. If you still want to delete this "
                    + "record, click the Delete button again. Otherwise "
                    + "click the Back to List hyperlink.";
            }

            return View(Kategori);
        }
        // POST: Kategoris/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Kategori Kategori)
        {
            try
            {
                if (await _context.Kategoris.AnyAsync(m => m.KategoriID == Kategori.KategoriID))
                {
                    _context.Kategoris.Remove(Kategori);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { concurrencyError = true, id = Kategori.KategoriID });
            }
        }

        private bool KategoriExists(int id)
        {
            return _context.Kategoris.Any(e => e.KategoriID == id);
        }
    }
}
