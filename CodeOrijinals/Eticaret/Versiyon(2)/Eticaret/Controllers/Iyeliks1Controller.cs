using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Eticaret.Data;
using Eticaret.Models;

namespace Eticaret.Controllers
{
    public class Iyeliks1Controller : Controller
    {
        private readonly UygVTContext _context;

        public Iyeliks1Controller(UygVTContext context)
        {
            _context = context;
        }

        // GET: Iyeliks1
        public async Task<IActionResult> Index()
        {
            var uygVTContext = _context.Iyelik.Include(i => i.EticaretUser).Include(i => i.Urun);
            return View(await uygVTContext.ToListAsync());
        }

        // GET: Iyeliks1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var iyelik = await _context.Iyelik
                .Include(i => i.EticaretUser)
                .Include(i => i.Urun)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (iyelik == null)
            {
                return NotFound();
            }

            return View(iyelik);
        }

        // GET: Iyeliks1/Create
        public IActionResult Create()
        {
            ViewData["EticaretUserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["UrunId"] = new SelectList(_context.Urunler, "Id", "Id");
            return View();
        }

        // POST: Iyeliks1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Miktar,Fiyati,UrunId,EticaretUserId")] Iyelik iyelik)
        {
            if (ModelState.IsValid)
            {
                _context.Add(iyelik);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EticaretUserId"] = new SelectList(_context.Users, "Id", "Id", iyelik.EticaretUserId);
            ViewData["UrunId"] = new SelectList(_context.Urunler, "Id", "Id", iyelik.UrunId);
            return View(iyelik);
        }

        // GET: Iyeliks1/Edit/5
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
            ViewData["EticaretUserId"] = new SelectList(_context.Users, "Id", "Id", iyelik.EticaretUserId);
            ViewData["UrunId"] = new SelectList(_context.Urunler, "Id", "Id", iyelik.UrunId);
            return View(iyelik);
        }

        // POST: Iyeliks1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Miktar,Fiyati,UrunId,EticaretUserId")] Iyelik iyelik)
        {
            if (id != iyelik.Id)
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
                    if (!IyelikExists(iyelik.Id))
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
            ViewData["EticaretUserId"] = new SelectList(_context.Users, "Id", "Id", iyelik.EticaretUserId);
            ViewData["UrunId"] = new SelectList(_context.Urunler, "Id", "Id", iyelik.UrunId);
            return View(iyelik);
        }

        // GET: Iyeliks1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var iyelik = await _context.Iyelik
                .Include(i => i.EticaretUser)
                .Include(i => i.Urun)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (iyelik == null)
            {
                return NotFound();
            }

            return View(iyelik);
        }

        // POST: Iyeliks1/Delete/5
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
            return _context.Iyelik.Any(e => e.Id == id);
        }
    }
}
