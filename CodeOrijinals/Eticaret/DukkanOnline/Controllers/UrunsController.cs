using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DukkanOnline.Data;
using DukkanOnline.Models;

namespace DukkanOnline.Controllers
{
    public class UrunsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UrunsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Uruns
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Uruns.Include(u => u.Alici).Include(u => u.Bakici).Include(u => u.Kategori).Include(u => u.Satici).Include(u => u.Toptanci).Include(u => u.Yorumcu);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Uruns/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var urun = await _context.Uruns
                .Include(u => u.Alici)
                .Include(u => u.Bakici)
                .Include(u => u.Kategori)
                .Include(u => u.Satici)
                .Include(u => u.Toptanci)
                .Include(u => u.Yorumcu)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (urun == null)
            {
                return NotFound();
            }

            return View(urun);
        }

        // GET: Uruns/Create
        public IActionResult Create()
        {
            ViewData["AliciId"] = new SelectList(_context.Set<Alici>(), "Id", "Id");
            ViewData["BakiciId"] = new SelectList(_context.Set<Bakici>(), "Id", "Id");
            ViewData["KategoriId"] = new SelectList(_context.Kategoris, "Id", "Id");
            ViewData["SaticiId"] = new SelectList(_context.Set<Satici>(), "Id", "Id");
            ViewData["ToptanciId"] = new SelectList(_context.Toptancis, "Id", "Id");
            ViewData["YorumcuId"] = new SelectList(_context.Set<Yorumcu>(), "Id", "Id");
            return View();
        }

        // POST: Uruns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Name,ShortDescription,LongDescription,Price,Stok,KategoriId,ToptanciId,SaticiId,BakiciId,AliciId,YorumcuId")] Urun urun)
        {
            if (ModelState.IsValid)
            {
                _context.Add(urun);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AliciId"] = new SelectList(_context.Set<Alici>(), "Id", "Id", urun.AliciId);
            ViewData["BakiciId"] = new SelectList(_context.Set<Bakici>(), "Id", "Id", urun.BakiciId);
            ViewData["KategoriId"] = new SelectList(_context.Kategoris, "Id", "Id", urun.KategoriId);
            ViewData["SaticiId"] = new SelectList(_context.Set<Satici>(), "Id", "Id", urun.SaticiId);
            ViewData["ToptanciId"] = new SelectList(_context.Toptancis, "Id", "Id", urun.ToptanciId);
            ViewData["YorumcuId"] = new SelectList(_context.Set<Yorumcu>(), "Id", "Id", urun.YorumcuId);
            return View(urun);
        }

        // GET: Uruns/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var urun = await _context.Uruns.FindAsync(id);
            if (urun == null)
            {
                return NotFound();
            }
            ViewData["AliciId"] = new SelectList(_context.Set<Alici>(), "Id", "Id", urun.AliciId);
            ViewData["BakiciId"] = new SelectList(_context.Set<Bakici>(), "Id", "Id", urun.BakiciId);
            ViewData["KategoriId"] = new SelectList(_context.Kategoris, "Id", "Id", urun.KategoriId);
            ViewData["SaticiId"] = new SelectList(_context.Set<Satici>(), "Id", "Id", urun.SaticiId);
            ViewData["ToptanciId"] = new SelectList(_context.Toptancis, "Id", "Id", urun.ToptanciId);
            ViewData["YorumcuId"] = new SelectList(_context.Set<Yorumcu>(), "Id", "Id", urun.YorumcuId);
            return View(urun);
        }

        // POST: Uruns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Name,ShortDescription,LongDescription,Price,Stok,KategoriId,ToptanciId,SaticiId,BakiciId,AliciId,YorumcuId")] Urun urun)
        {
            if (id != urun.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(urun);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UrunExists(urun.Id))
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
            ViewData["AliciId"] = new SelectList(_context.Set<Alici>(), "Id", "Id", urun.AliciId);
            ViewData["BakiciId"] = new SelectList(_context.Set<Bakici>(), "Id", "Id", urun.BakiciId);
            ViewData["KategoriId"] = new SelectList(_context.Kategoris, "Id", "Id", urun.KategoriId);
            ViewData["SaticiId"] = new SelectList(_context.Set<Satici>(), "Id", "Id", urun.SaticiId);
            ViewData["ToptanciId"] = new SelectList(_context.Toptancis, "Id", "Id", urun.ToptanciId);
            ViewData["YorumcuId"] = new SelectList(_context.Set<Yorumcu>(), "Id", "Id", urun.YorumcuId);
            return View(urun);
        }

        // GET: Uruns/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var urun = await _context.Uruns
                .Include(u => u.Alici)
                .Include(u => u.Bakici)
                .Include(u => u.Kategori)
                .Include(u => u.Satici)
                .Include(u => u.Toptanci)
                .Include(u => u.Yorumcu)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (urun == null)
            {
                return NotFound();
            }

            return View(urun);
        }

        // POST: Uruns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var urun = await _context.Uruns.FindAsync(id);
            _context.Uruns.Remove(urun);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UrunExists(int id)
        {
            return _context.Uruns.Any(e => e.Id == id);
        }
    }
}
