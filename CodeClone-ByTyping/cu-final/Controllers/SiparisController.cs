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
    public class SiparisController : Controller
    {
        private readonly MarketContext _context;

        public SiparisController(MarketContext context)
        {
            _context = context;
        }

        // GET: Siparis
        public async Task<IActionResult> Index()
        {
            var marketContext = _context.Sipariss.Include(s => s.Musteri).Include(s => s.Urun);
            return View(await marketContext.ToListAsync());
        }

        // GET: Siparis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siparis = await _context.Sipariss
                .Include(s => s.Musteri)
                .Include(s => s.Urun)
                .FirstOrDefaultAsync(m => m.SiparisID == id);
            if (siparis == null)
            {
                return NotFound();
            }

            return View(siparis);
        }
        #region Aradakiler

        // GET: Siparis/Create
        public IActionResult Create()
        {
            ViewData["MusteriID"] = new SelectList(_context.Musteris, "ID", "FullName");
            ViewData["UrunID"] = new SelectList(_context.Uruns, "UrunID", "Title");
            return View();
        }

        // POST: Siparis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SiparisID,UrunID,MusteriID,Grade")] Siparis siparis)
        {
            if (ModelState.IsValid)
            {
                _context.Add(siparis);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MusteriID"] = new SelectList(_context.Musteris, "ID", "FullName", siparis.MusteriID);
            ViewData["UrunID"] = new SelectList(_context.Uruns, "UrunID", "Title", siparis.UrunID);
            return View(siparis);
        }

        // GET: Siparis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siparis = await _context.Sipariss.FindAsync(id);
            if (siparis == null)
            {
                return NotFound();
            }
            ViewData["MusteriID"] = new SelectList(_context.Musteris, "ID", "FullName", siparis.MusteriID);
            ViewData["UrunID"] = new SelectList(_context.Uruns, "UrunID", "Title", siparis.UrunID);
            return View(siparis);
        }

        // POST: Siparis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SiparisID,UrunID,MusteriID,Grade")] Siparis siparis)
        {
            if (id != siparis.SiparisID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(siparis);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SiparisExists(siparis.SiparisID))
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
            ViewData["MusteriID"] = new SelectList(_context.Musteris, "ID", "FullName", siparis.MusteriID);
            ViewData["UrunID"] = new SelectList(_context.Uruns, "UrunID", "Title", siparis.UrunID);
            return View(siparis);
        }

        #endregion
        // GET: Siparis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siparis = await _context.Sipariss
                .Include(s => s.Musteri)
                .Include(s => s.Urun)
                .FirstOrDefaultAsync(m => m.SiparisID == id);
            if (siparis == null)
            {
                return NotFound();
            }

            return View(siparis);
        }

        // POST: Siparis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var siparis = await _context.Sipariss.FindAsync(id);
            _context.Sipariss.Remove(siparis);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SiparisExists(int id)
        {
            return _context.Sipariss.Any(e => e.SiparisID == id);
        }
    }
}
