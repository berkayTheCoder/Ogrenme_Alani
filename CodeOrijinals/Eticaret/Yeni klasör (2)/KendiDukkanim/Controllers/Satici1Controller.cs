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
    public class Satici1Controller : Controller
    {
        private readonly KendiDukkanimContext _context;

        public Satici1Controller(KendiDukkanimContext context)
        {
            _context = context;
        }

        // GET: Satici1
        public async Task<IActionResult> Index()
        {
            return View(await _context.Satici1.ToListAsync());
        }

        // GET: Satici1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var satici1 = await _context.Satici1
                .Include(n=>n.Satislari)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (satici1 == null)
            {
                return NotFound();
            }

            return View(satici1);
        }

        // GET: Satici1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Satici1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,LastName,FirstMidName")] Satici1 satici1)
        {
            if (ModelState.IsValid)
            {
                _context.Add(satici1);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(satici1);
        }

        // GET: Satici1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var satici1 = await _context.Satici1.FindAsync(id);
            if (satici1 == null)
            {
                return NotFound();
            }
            return View(satici1);
        }

        // POST: Satici1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,LastName,FirstMidName")] Satici1 satici1)
        {
            if (id != satici1.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(satici1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Satici1Exists(satici1.ID))
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
            return View(satici1);
        }

        // GET: Satici1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var satici1 = await _context.Satici1
                .FirstOrDefaultAsync(m => m.ID == id);
            if (satici1 == null)
            {
                return NotFound();
            }

            return View(satici1);
        }

        // POST: Satici1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var satici1 = await _context.Satici1.FindAsync(id);
            _context.Satici1.Remove(satici1);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Satici1Exists(int id)
        {
            return _context.Satici1.Any(e => e.ID == id);
        }
    }
}
