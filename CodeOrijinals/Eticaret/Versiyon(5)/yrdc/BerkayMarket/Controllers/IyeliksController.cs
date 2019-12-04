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
    public class IyeliksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IyeliksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Iyeliks
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Iyelik.Include(i => i.BmUser).Include(i => i.Urun);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Iyeliks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var iyelik = await _context.Iyelik
                .Include(i => i.BmUser)
                .Include(i => i.Urun)
                .FirstOrDefaultAsync(m => m.UrunId == id);
            if (iyelik == null)
            {
                return NotFound();
            }

            return View(iyelik);
        }

        // GET: Iyeliks/Create
        public IActionResult Create()
        {
            ViewData["BmUserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["UrunId"] = new SelectList(_context.Uruns, "Id", "Id");
            return View();
        }

        // POST: Iyeliks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UrunId,BmUserId")] Iyelik iyelik)
        {
            if (ModelState.IsValid)
            {
                _context.Add(iyelik);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BmUserId"] = new SelectList(_context.Users, "Id", "Id", iyelik.BmUserId);
            ViewData["UrunId"] = new SelectList(_context.Uruns, "Id", "Id", iyelik.UrunId);
            return View(iyelik);
        }

        // GET: Iyeliks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var iyelik = await _context.Iyelik.FindAsync(id);
            if (iyelik == null)
            {
                return NotFound();
            }
            ViewData["BmUserId"] = new SelectList(_context.Users, "Id", "Id", iyelik.BmUserId);
            ViewData["UrunId"] = new SelectList(_context.Uruns, "Id", "Id", iyelik.UrunId);
            return View(iyelik);
        }

        // POST: Iyeliks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UrunId,BmUserId")] Iyelik iyelik)
        {
            if (id != iyelik.UrunId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(iyelik);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IyelikExists(iyelik.UrunId))
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
            ViewData["BmUserId"] = new SelectList(_context.Users, "Id", "Id", iyelik.BmUserId);
            ViewData["UrunId"] = new SelectList(_context.Uruns, "Id", "Id", iyelik.UrunId);
            return View(iyelik);
        }

        // GET: Iyeliks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var iyelik = await _context.Iyelik
                .Include(i => i.BmUser)
                .Include(i => i.Urun)
                .FirstOrDefaultAsync(m => m.UrunId == id);
            if (iyelik == null)
            {
                return NotFound();
            }

            return View(iyelik);
        }

        // POST: Iyeliks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var iyelik = await _context.Iyelik.FindAsync(id);
            _context.Iyelik.Remove(iyelik);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IyelikExists(int id)
        {
            return _context.Iyelik.Any(e => e.UrunId == id);
        }
    }
}
