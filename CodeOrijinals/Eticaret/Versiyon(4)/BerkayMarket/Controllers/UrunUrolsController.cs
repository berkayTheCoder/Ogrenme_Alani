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
    public class UrunUrolsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UrunUrolsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UrunUrols
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.UrunUrol
                .Include(u => u.Urol)
                .Include(u => u.Urun);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: UrunUrols/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var urunUrol = await _context.UrunUrol
                .Include(u => u.Urol)
                .Include(u => u.Urun)
                .FirstOrDefaultAsync(m => m.UrunId == id);
            if (urunUrol == null)
            {
                return NotFound();
            }

            return View(urunUrol);
        }

        // GET: UrunUrols/Create
        public IActionResult Create()
        {
            ViewData["UrolId"] = new SelectList(_context.Urol, "Id", "Id");
            ViewData["UrunId"] = new SelectList(_context.Uruns, "Id", "Id");
            return View();
        }

        // POST: UrunUrols/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UrunId,UrolId")] UrunUrol urunUrol)
        {
            if (ModelState.IsValid)
            {
                _context.Add(urunUrol);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UrolId"] = new SelectList(_context.Urol, "Id", "Id", urunUrol.UrolId);
            ViewData["UrunId"] = new SelectList(_context.Uruns, "Id", "Id", urunUrol.UrunId);
            return View(urunUrol);
        }

        // GET: UrunUrols/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var urunUrol = await _context.UrunUrol.FindAsync(id);
            if (urunUrol == null)
            {
                return NotFound();
            }
            ViewData["UrolId"] = new SelectList(_context.Urol, "Id", "Id", urunUrol.UrolId);
            ViewData["UrunId"] = new SelectList(_context.Uruns, "Id", "Id", urunUrol.UrunId);
            return View(urunUrol);
        }

        // POST: UrunUrols/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UrunId,UrolId")] UrunUrol urunUrol)
        {
            if (id != urunUrol.UrunId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(urunUrol);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UrunUrolExists(urunUrol.UrunId))
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
            ViewData["UrolId"] = new SelectList(_context.Urol, "Id", "Id", urunUrol.UrolId);
            ViewData["UrunId"] = new SelectList(_context.Uruns, "Id", "Id", urunUrol.UrunId);
            return View(urunUrol);
        }

        // GET: UrunUrols/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var urunUrol = await _context.UrunUrol
                .Include(u => u.Urol)
                .Include(u => u.Urun)
                .FirstOrDefaultAsync(m => m.UrunId == id);
            if (urunUrol == null)
            {
                return NotFound();
            }

            return View(urunUrol);
        }

        // POST: UrunUrols/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var urunUrol = await _context.UrunUrol.FindAsync(id);
            _context.UrunUrol.Remove(urunUrol);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UrunUrolExists(int id)
        {
            return _context.UrunUrol.Any(e => e.UrunId == id);
        }
    }
}
