using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KendiDukkanim.Models;

namespace KendiDukkanim.Controllers
{
    public class UrunsController : Controller
    {
        private readonly KendiDukkanimContext _context;

        public UrunsController(KendiDukkanimContext context)
        {
            _context = context;
        }

        // GET: Uruns
        public async Task<IActionResult> Index()
        {
            var kendiDukkanimContext = _context.Urun.Include(u => u.Kategorisi);
            return View(await kendiDukkanimContext.ToListAsync());
        }

        // GET: Uruns/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var urun = await _context.Urun
                .Include(u => u.Kategorisi)
                .FirstOrDefaultAsync(m => m.UrunID == id);
            if (urun == null)
            {
                return NotFound();
            }

            return View(urun);
        }

        // GET: Uruns/Create
        public IActionResult Create()
        {
            ViewData["KategoriID"] = new SelectList(_context.Set<Kategori>(), "KategoriID", "KategoriID");
            return View();
        }

        // POST: Uruns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UrunID,Title,Adi,Stok,KisaAciklama,UzunAciklama,Puani,KategoriID")] Urun urun)
        {
            if (ModelState.IsValid)
            {
                _context.Add(urun);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KategoriID"] = new SelectList(_context.Set<Kategori>(), "KategoriID", "KategoriID", urun.KategoriID);
            return View(urun);
        }

        // GET: Uruns/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var urun = await _context.Urun.FindAsync(id);
            if (urun == null)
            {
                return NotFound();
            }
            ViewData["KategoriID"] = new SelectList(_context.Set<Kategori>(), "KategoriID", "KategoriID", urun.KategoriID);
            return View(urun);
        }

        // POST: Uruns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UrunID,Title,Adi,Stok,KisaAciklama,UzunAciklama,Puani,KategoriID")] Urun urun)
        {
            if (id != urun.UrunID)
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
                    if (!UrunExists(urun.UrunID))
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
            ViewData["KategoriID"] = new SelectList(_context.Set<Kategori>(), "KategoriID", "KategoriID", urun.KategoriID);
            return View(urun);
        }

        // GET: Uruns/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var urun = await _context.Urun
                .Include(u => u.Kategorisi)
                .FirstOrDefaultAsync(m => m.UrunID == id);
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
            var urun = await _context.Urun.FindAsync(id);
            _context.Urun.Remove(urun);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UrunExists(int id)
        {
            return _context.Urun.Any(e => e.UrunID == id);
        }
    }
}
