using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DukkanOnline2.Models;

namespace DukkanOnline2.Controllers
{
    public class SepetsController : Controller
    {
        private readonly DukkanOnline2Context _context;

        public SepetsController(DukkanOnline2Context context)
        {
            _context = context;
        }

        // GET: Sepets
        public async Task<IActionResult> Index()
        {
            var dukkanOnline2Context = _context.Sepet.Include(s => s.Alici).Include(s => s.Bakici).Include(s => s.Yorumcu);
            return View(await dukkanOnline2Context.ToListAsync());
        }

        // GET: Sepets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sepet = await _context.Sepet
                .Include(s => s.Alici)
                .Include(s => s.Bakici)
                .Include(s => s.Yorumcu)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sepet == null)
            {
                return NotFound();
            }

            return View(sepet);
        }

        // GET: Sepets/Create
        public IActionResult Create()
        {
            ViewData["AliciId"] = new SelectList(_context.Set<Alici>(), "Id", "Id");
            ViewData["BakiciId"] = new SelectList(_context.Set<Bakici>(), "Id", "Id");
            ViewData["YorumcuId"] = new SelectList(_context.Set<Yorumcu>(), "Id", "Id");
            return View();
        }

        // POST: Sepets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EnvanterId,UrunId,BakiciId,AliciId,YorumcuId")] Sepet sepet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sepet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AliciId"] = new SelectList(_context.Set<Alici>(), "Id", "Id", sepet.AliciId);
            ViewData["BakiciId"] = new SelectList(_context.Set<Bakici>(), "Id", "Id", sepet.BakiciId);
            ViewData["YorumcuId"] = new SelectList(_context.Set<Yorumcu>(), "Id", "Id", sepet.YorumcuId);
            return View(sepet);
        }

        // GET: Sepets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sepet = await _context.Sepet.FindAsync(id);
            if (sepet == null)
            {
                return NotFound();
            }
            ViewData["AliciId"] = new SelectList(_context.Set<Alici>(), "Id", "Id", sepet.AliciId);
            ViewData["BakiciId"] = new SelectList(_context.Set<Bakici>(), "Id", "Id", sepet.BakiciId);
            ViewData["YorumcuId"] = new SelectList(_context.Set<Yorumcu>(), "Id", "Id", sepet.YorumcuId);
            return View(sepet);
        }

        // POST: Sepets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EnvanterId,UrunId,BakiciId,AliciId,YorumcuId")] Sepet sepet)
        {
            if (id != sepet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sepet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SepetExists(sepet.Id))
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
            ViewData["AliciId"] = new SelectList(_context.Set<Alici>(), "Id", "Id", sepet.AliciId);
            ViewData["BakiciId"] = new SelectList(_context.Set<Bakici>(), "Id", "Id", sepet.BakiciId);
            ViewData["YorumcuId"] = new SelectList(_context.Set<Yorumcu>(), "Id", "Id", sepet.YorumcuId);
            return View(sepet);
        }

        // GET: Sepets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sepet = await _context.Sepet
                .Include(s => s.Alici)
                .Include(s => s.Bakici)
                .Include(s => s.Yorumcu)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sepet == null)
            {
                return NotFound();
            }

            return View(sepet);
        }

        // POST: Sepets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sepet = await _context.Sepet.FindAsync(id);
            _context.Sepet.Remove(sepet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SepetExists(int id)
        {
            return _context.Sepet.Any(e => e.Id == id);
        }
    }
}
