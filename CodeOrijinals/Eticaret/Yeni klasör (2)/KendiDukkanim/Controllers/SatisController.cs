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
    public class SatisController : Controller
    {
        private readonly KendiDukkanimContext _context;

        public SatisController(KendiDukkanimContext context)
        {
            _context = context;
        }

        // GET: Satis
        public async Task<IActionResult> Index()
        {
            var kendiDukkanimContext = _context.Satis.Include(s => s.Satici).Include(s => s.Urun);
            return View(await kendiDukkanimContext.ToListAsync());
        }

        // GET: Satis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var satis = await _context.Satis
                .Include(s => s.Satici)
                .Include(s => s.Urun)
                .FirstOrDefaultAsync(m => m.SatisId == id);
            if (satis == null)
            {
                return NotFound();
            }

            return View(satis);
        }

        // GET: Satis/Create
        public IActionResult Create()
        {
            ViewData["SaticiID"] = new SelectList(_context.Satici, "ID", "FirstMidName");
            ViewData["UrunID"] = new SelectList(_context.Urun, "UrunID", "UrunID");
            return View();
        }

        // POST: Satis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SatisId,SatisZamani,SaticiID,UrunID")] Satis satis)
        {
            if (ModelState.IsValid)
            {
                _context.Add(satis);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SaticiID"] = new SelectList(_context.Satici, "ID", "FirstMidName", satis.SaticiID);
            ViewData["UrunID"] = new SelectList(_context.Urun, "UrunID", "UrunID", satis.UrunID);
            return View(satis);
        }

        // GET: Satis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var satis = await _context.Satis.FindAsync(id);
            if (satis == null)
            {
                return NotFound();
            }
            ViewData["SaticiID"] = new SelectList(_context.Satici, "ID", "FirstMidName", satis.SaticiID);
            ViewData["UrunID"] = new SelectList(_context.Urun, "UrunID", "UrunID", satis.UrunID);
            return View(satis);
        }

        // POST: Satis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SatisId,SatisZamani,SaticiID,UrunID")] Satis satis)
        {
            if (id != satis.SatisId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(satis);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SatisExists(satis.SatisId))
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
            ViewData["SaticiID"] = new SelectList(_context.Satici, "ID", "FirstMidName", satis.SaticiID);
            ViewData["UrunID"] = new SelectList(_context.Urun, "UrunID", "UrunID", satis.UrunID);
            return View(satis);
        }

        // GET: Satis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var satis = await _context.Satis
                .Include(s => s.Satici)
                .Include(s => s.Urun)
                .FirstOrDefaultAsync(m => m.SatisId == id);
            if (satis == null)
            {
                return NotFound();
            }

            return View(satis);
        }

        // POST: Satis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var satis = await _context.Satis.FindAsync(id);
            _context.Satis.Remove(satis);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SatisExists(int id)
        {
            return _context.Satis.Any(e => e.SatisId == id);
        }
    }
}
