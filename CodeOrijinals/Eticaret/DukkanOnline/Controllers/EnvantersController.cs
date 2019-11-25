using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DukkanOnline.Data;
using DukkanOnline.Models;

namespace DukkanOnline.Controllers
{
    public class EnvantersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EnvantersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Envanters
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Envanter.Include(e => e.Alici).Include(e => e.Bakici).Include(e => e.Satici).Include(e => e.Yorumcu);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Envanters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var envanter = await _context.Envanter
                .Include(e => e.Alici)
                .Include(e => e.Bakici)
                .Include(e => e.Satici)
                .Include(e => e.Yorumcu)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (envanter == null)
            {
                return NotFound();
            }

            return View(envanter);
        }

        // GET: Envanters/Create
        public IActionResult Create()
        {
            ViewData["AliciId"] = new SelectList(_context.Set<Alici>(), "Id", "Id");
            ViewData["BakiciId"] = new SelectList(_context.Set<Bakici>(), "Id", "Id");
            ViewData["SaticiId"] = new SelectList(_context.Set<Satici>(), "Id", "Id");
            ViewData["YorumcuId"] = new SelectList(_context.Set<Yorumcu>(), "Id", "Id");
            return View();
        }

        // POST: Envanters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UrunId,SaticiId,BakiciId,AliciId,YorumcuId")] Envanter envanter)
        {
            if (ModelState.IsValid)
            {
                _context.Add(envanter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AliciId"] = new SelectList(_context.Set<Alici>(), "Id", "Id", envanter.AliciId);
            ViewData["BakiciId"] = new SelectList(_context.Set<Bakici>(), "Id", "Id", envanter.BakiciId);
            ViewData["SaticiId"] = new SelectList(_context.Set<Satici>(), "Id", "Id", envanter.SaticiId);
            ViewData["YorumcuId"] = new SelectList(_context.Set<Yorumcu>(), "Id", "Id", envanter.YorumcuId);
            return View(envanter);
        }

        // GET: Envanters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var envanter = await _context.Envanter.FindAsync(id);
            if (envanter == null)
            {
                return NotFound();
            }
            ViewData["AliciId"] = new SelectList(_context.Set<Alici>(), "Id", "Id", envanter.AliciId);
            ViewData["BakiciId"] = new SelectList(_context.Set<Bakici>(), "Id", "Id", envanter.BakiciId);
            ViewData["SaticiId"] = new SelectList(_context.Set<Satici>(), "Id", "Id", envanter.SaticiId);
            ViewData["YorumcuId"] = new SelectList(_context.Set<Yorumcu>(), "Id", "Id", envanter.YorumcuId);
            return View(envanter);
        }

        // POST: Envanters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UrunId,SaticiId,BakiciId,AliciId,YorumcuId")] Envanter envanter)
        {
            if (id != envanter.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(envanter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnvanterExists(envanter.Id))
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
            ViewData["AliciId"] = new SelectList(_context.Set<Alici>(), "Id", "Id", envanter.AliciId);
            ViewData["BakiciId"] = new SelectList(_context.Set<Bakici>(), "Id", "Id", envanter.BakiciId);
            ViewData["SaticiId"] = new SelectList(_context.Set<Satici>(), "Id", "Id", envanter.SaticiId);
            ViewData["YorumcuId"] = new SelectList(_context.Set<Yorumcu>(), "Id", "Id", envanter.YorumcuId);
            return View(envanter);
        }

        // GET: Envanters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var envanter = await _context.Envanter
                .Include(e => e.Alici)
                .Include(e => e.Bakici)
                .Include(e => e.Satici)
                .Include(e => e.Yorumcu)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (envanter == null)
            {
                return NotFound();
            }

            return View(envanter);
        }

        // POST: Envanters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var envanter = await _context.Envanter.FindAsync(id);
            _context.Envanter.Remove(envanter);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnvanterExists(int id)
        {
            return _context.Envanter.Any(e => e.Id == id);
        }
    }
}
