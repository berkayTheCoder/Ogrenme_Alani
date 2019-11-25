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
    public class DukkansController : Controller
    {
        private readonly KendiDukkanimContext _context;

        public DukkansController(KendiDukkanimContext context)
        {
            _context = context;
        }

        // GET: Dukkans
        public async Task<IActionResult> Index()
        {
            var kendiDukkanimContext = _context.Dukkan.Include(d => d.Satici);
            return View(await kendiDukkanimContext.ToListAsync());
        }

        // GET: Dukkans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dukkan = await _context.Dukkan
                .Include(d => d.Satici)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dukkan == null)
            {
                return NotFound();
            }

            return View(dukkan);
        }

        // GET: Dukkans/Create
        public IActionResult Create()
        {
            ViewData["SaticiId"] = new SelectList(_context.Satici, "ID", "FirstMidName");
            return View();
        }

        // POST: Dukkans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Adi,Yerleske,SaticiId")] Dukkan dukkan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dukkan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SaticiId"] = new SelectList(_context.Satici, "ID", "FirstMidName", dukkan.SaticiId);
            return View(dukkan);
        }

        // GET: Dukkans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dukkan = await _context.Dukkan.FindAsync(id);
            if (dukkan == null)
            {
                return NotFound();
            }
            ViewData["SaticiId"] = new SelectList(_context.Satici, "ID", "FirstMidName", dukkan.SaticiId);
            return View(dukkan);
        }

        // POST: Dukkans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Adi,Yerleske,SaticiId")] Dukkan dukkan)
        {
            if (id != dukkan.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dukkan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DukkanExists(dukkan.Id))
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
            ViewData["SaticiId"] = new SelectList(_context.Satici, "ID", "FirstMidName", dukkan.SaticiId);
            return View(dukkan);
        }

        // GET: Dukkans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dukkan = await _context.Dukkan
                .Include(d => d.Satici)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dukkan == null)
            {
                return NotFound();
            }

            return View(dukkan);
        }

        // POST: Dukkans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dukkan = await _context.Dukkan.FindAsync(id);
            _context.Dukkan.Remove(dukkan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DukkanExists(int id)
        {
            return _context.Dukkan.Any(e => e.Id == id);
        }
    }
}
