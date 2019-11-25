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
    public class SaticisController : Controller
    {
        private readonly KendiDukkanimContext _context;

        public SaticisController(KendiDukkanimContext context)
        {
            _context = context;
        }

        // GET: Saticis
        public async Task<IActionResult> Index()
        {
            return View(await _context.Satici.ToListAsync());
        }

        // GET: Saticis/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var satici = await _context.Satici
                .Include(s=>s.SatisLari)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (satici == null)
            {
                return NotFound();
            }

            return View(satici);
        }

        // GET: Saticis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Saticis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,LastName,FirstMidName")] Satici satici)
        {
            if (ModelState.IsValid)
            {
                _context.Add(satici);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(satici);
        }

        // GET: Saticis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var satici = await _context.Satici.FindAsync(id);
            if (satici == null)
            {
                return NotFound();
            }
            return View(satici);
        }

        // POST: Saticis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,LastName,FirstMidName")] Satici satici)
        {
            if (id != satici.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(satici);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SaticiExists(satici.ID))
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
            return View(satici);
        }

        // GET: Saticis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var satici = await _context.Satici
                .FirstOrDefaultAsync(m => m.ID == id);
            if (satici == null)
            {
                return NotFound();
            }

            return View(satici);
        }

        // POST: Saticis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var satici = await _context.Satici.FindAsync(id);
            _context.Satici.Remove(satici);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SaticiExists(int id)
        {
            return _context.Satici.Any(e => e.ID == id);
        }
    }
}
