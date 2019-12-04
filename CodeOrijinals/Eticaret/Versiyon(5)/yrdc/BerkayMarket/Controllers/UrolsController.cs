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
    public class UrolsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UrolsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Urols
        public async Task<IActionResult> Index()
        {
            return View(await _context.Urol.ToListAsync());
        }

        // GET: Urols/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var urol = await _context.Urol
                .FirstOrDefaultAsync(m => m.Id == id);
            if (urol == null)
            {
                return NotFound();
            }

            return View(urol);
        }

        // GET: Urols/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Urols/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Adi")] Urol urol)
        {
            if (ModelState.IsValid)
            {
                _context.Add(urol);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(urol);
        }

        // GET: Urols/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var urol = await _context.Urol.FindAsync(id);
            if (urol == null)
            {
                return NotFound();
            }
            return View(urol);
        }

        // POST: Urols/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Adi")] Urol urol)
        {
            if (id != urol.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(urol);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UrolExists(urol.Id))
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
            return View(urol);
        }

        // GET: Urols/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var urol = await _context.Urol
                .FirstOrDefaultAsync(m => m.Id == id);
            if (urol == null)
            {
                return NotFound();
            }

            return View(urol);
        }

        // POST: Urols/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var urol = await _context.Urol.FindAsync(id);
            _context.Urol.Remove(urol);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UrolExists(int id)
        {
            return _context.Urol.Any(e => e.Id == id);
        }
    }
}
