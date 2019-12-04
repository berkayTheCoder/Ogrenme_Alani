using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BerkayMarket.Data;
using BerkayMarket.Models;
using BerkayMarket.Models.SchoolViewModels;

namespace BerkayMarket.Controllers
{
    public class SaticisController : Controller
    {
        private readonly MarketContext _context;

        public SaticisController(MarketContext context)
        {
            _context = context;
        }

        // GET: Saticis
        public async Task<IActionResult> Index(int? id, int? UrunID)
        {
            var viewModel = new SaticiIndexData();
            viewModel.Saticis = await _context.Saticis
                  .Include(i => i.OfficeAssignment)
                  .Include(i => i.Envanters)
                    .ThenInclude(i => i.Urun)
                        .ThenInclude(i => i.Kategori)
                  .OrderBy(i => i.LastName)
                  .ToListAsync();

            if (id != null)
            {
                ViewData["SaticiID"] = id.Value;
                Satici Satici = viewModel.Saticis.Where(
                    i => i.ID == id.Value).Single();
                viewModel.Uruns = Satici.Envanters.Select(s => s.Urun);
            }

            if (UrunID != null)
            {
                ViewData["UrunID"] = UrunID.Value;
                var selectedUrun = viewModel.Uruns.Where(x => x.UrunID == UrunID).Single();
                await _context.Entry(selectedUrun).Collection(x => x.Sipariss).LoadAsync();
                foreach (Siparis Siparis in selectedUrun.Sipariss)
                {
                    await _context.Entry(Siparis).Reference(x => x.Musteri).LoadAsync();
                }
                viewModel.Sipariss = selectedUrun.Sipariss;
            }

            return View(viewModel);
        }

        // GET: Saticis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Satici = await _context.Saticis
                .FirstOrDefaultAsync(m => m.ID == id);
            if (Satici == null)
            {
                return NotFound();
            }

            return View(Satici);
        }

        // GET: Saticis/Create
        public IActionResult Create()
        {
            var Satici = new Satici();
            Satici.Envanters = new List<Envanter>();
            PopulateAssignedUrunData(Satici);
            return View();
        }

        // POST: Saticis/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstMidName,HireDate,LastName,OfficeAssignment")] Satici Satici, string[] selectedUruns)
        {
            if (selectedUruns != null)
            {
                Satici.Envanters = new List<Envanter>();
                foreach (var Urun in selectedUruns)
                {
                    var UrunToAdd = new Envanter { SaticiID = Satici.ID, UrunID = int.Parse(Urun) };
                    Satici.Envanters.Add(UrunToAdd);
                }
            }
            if (ModelState.IsValid)
            {
                _context.Add(Satici);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateAssignedUrunData(Satici);
            return View(Satici);
        }

        // GET: Saticis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Satici = await _context.Saticis
                .Include(i => i.OfficeAssignment)
                .Include(i => i.Envanters).ThenInclude(i => i.Urun)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (Satici == null)
            {
                return NotFound();
            }
            PopulateAssignedUrunData(Satici);
            return View(Satici);
        }

        private void PopulateAssignedUrunData(Satici Satici)
        {
            var allUruns = _context.Uruns;
            var SaticiUruns = new HashSet<int>(Satici.Envanters.Select(c => c.UrunID));
            var viewModel = new List<AssignedUrunData>();
            foreach (var Urun in allUruns)
            {
                viewModel.Add(new AssignedUrunData
                {
                    UrunID = Urun.UrunID,
                    Title = Urun.Title,
                    Assigned = SaticiUruns.Contains(Urun.UrunID)
                });
            }
            ViewData["Uruns"] = viewModel;
        }

        // POST: Saticis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] selectedUruns)
        {
            if (id == null)
            {
                return NotFound();
            }

            var SaticiToUpdate = await _context.Saticis
                .Include(i => i.OfficeAssignment)
                .Include(i => i.Envanters)
                    .ThenInclude(i => i.Urun)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (await TryUpdateModelAsync<Satici>(
                SaticiToUpdate,
                "",
                i => i.FirstMidName, i => i.LastName, i => i.HireDate, i => i.OfficeAssignment))
            {
                if (String.IsNullOrWhiteSpace(SaticiToUpdate.OfficeAssignment?.Location))
                {
                    SaticiToUpdate.OfficeAssignment = null;
                }
                UpdateSaticiUruns(selectedUruns, SaticiToUpdate);
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
            UpdateSaticiUruns(selectedUruns, SaticiToUpdate);
            PopulateAssignedUrunData(SaticiToUpdate);
            return View(SaticiToUpdate);
        }
        
        private void UpdateSaticiUruns(string[] selectedUruns, Satici SaticiToUpdate)
        {
            if (selectedUruns == null)
            {
                SaticiToUpdate.Envanters = new List<Envanter>();
                return;
            }

            var selectedUrunsHS = new HashSet<string>(selectedUruns);
            var SaticiUruns = new HashSet<int>
                (SaticiToUpdate.Envanters.Select(c => c.Urun.UrunID));
            foreach (var Urun in _context.Uruns)
            {
                if (selectedUrunsHS.Contains(Urun.UrunID.ToString()))
                {
                    if (!SaticiUruns.Contains(Urun.UrunID))
                    {
                        SaticiToUpdate.Envanters.Add(new Envanter { SaticiID = SaticiToUpdate.ID, UrunID = Urun.UrunID });
                    }
                }
                else
                {

                    if (SaticiUruns.Contains(Urun.UrunID))
                    {
                        Envanter UrunToRemove = SaticiToUpdate.Envanters.FirstOrDefault(i => i.UrunID == Urun.UrunID);
                        _context.Remove(UrunToRemove);
                    }
                }
            }
        }

        // GET: Saticis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Satici = await _context.Saticis
                .FirstOrDefaultAsync(m => m.ID == id);
            if (Satici == null)
            {
                return NotFound();
            }

            return View(Satici);
        }

        // POST: Saticis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Satici Satici = await _context.Saticis
                .Include(i => i.Envanters)
                .SingleAsync(i => i.ID == id);

            var Kategoris = await _context.Kategoris
                .Where(d => d.SaticiID == id)
                .ToListAsync();
            Kategoris.ForEach(d => d.SaticiID = null);

            _context.Saticis.Remove(Satici);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SaticiExists(int id)
        {
            return _context.Saticis.Any(e => e.ID == id);
        }
    }
}
