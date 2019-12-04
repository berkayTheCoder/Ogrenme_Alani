using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DukkanOnline3.Data;
using DukkanOnline3.Models;

namespace DukkanOnline3.Controllers
{
    public class MamulsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MamulsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Mamuls
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Mamuls.Include(m => m.Kategori);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Mamuls/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mamul = await _context.Mamuls
                .Include(m => m.Kategori)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mamul == null)
            {
                return NotFound();
            }

            return View(mamul);
        }

        // GET: Mamuls/Create
        public IActionResult Create()
        {
            ViewData["KategoriId"] = new SelectList(_context.Kategoris, "Id", "Id");
            return View();
        }

        // POST: Mamuls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Name,ShortDescription,LongDescription,Maliyeti,Stok,Resim,KategoriId")] Mamul mamul)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mamul);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KategoriId"] = new SelectList(_context.Kategoris, "Id", "Id", mamul.KategoriId);
            return View(mamul);
        }

        // GET: Mamuls/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mamul = await _context.Mamuls.FindAsync(id);
            if (mamul == null)
            {
                return NotFound();
            }
            ViewData["KategoriId"] = new SelectList(_context.Kategoris, "Id", "Id", mamul.KategoriId);
            return View(mamul);
        }

        // POST: Mamuls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Name,ShortDescription,LongDescription,Maliyeti,Stok,Resim,KategoriId")] Mamul mamul)
        {
            if (id != mamul.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mamul);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MamulExists(mamul.Id))
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
            ViewData["KategoriId"] = new SelectList(_context.Kategoris, "Id", "Id", mamul.KategoriId);
            return View(mamul);
        }

        // GET: Mamuls/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mamul = await _context.Mamuls
                .Include(m => m.Kategori)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mamul == null)
            {
                return NotFound();
            }

            return View(mamul);
        }

        // POST: Mamuls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mamul = await _context.Mamuls.FindAsync(id);
            _context.Mamuls.Remove(mamul);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MamulExists(int id)
        {
            return _context.Mamuls.Any(e => e.Id == id);
        }
    }
}
