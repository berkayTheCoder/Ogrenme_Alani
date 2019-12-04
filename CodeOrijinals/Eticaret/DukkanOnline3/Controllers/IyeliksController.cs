using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DukkanOnline3.Data;
using DukkanOnline3.Models;

namespace DukkanOnline3.Controllers
{
    public class IyeliksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IyeliksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Iyeliks
        public async Task<IActionResult> Index(int? id)
        {
            if (id != null)
            {
                var iyelikler = _context.Iyeliks.Where(u => u.UrunID == id)
                    .Include(i => i.MyUsers).Include(i => i.Urun);
                return View(await iyelikler.ToListAsync());
            }
            var applicationDbContext = _context.Iyeliks
                .Include(i => i.MyUsers).Include(i => i.Urun);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Iyeliks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var iyelik = await _context.Iyeliks
                .Include(i => i.MyUsers)
                .Include(i => i.Urun)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (iyelik == null)
            {
                return NotFound();
            }

            return View(iyelik);
        }

        // GET: Iyeliks/Create
        public IActionResult Create()
        {
            ViewData["MyUserID"] = new SelectList(_context.Users, "Id", "UserName");
            ViewData["UrunID"] = new SelectList(_context.Uruns, "Id", "Name");
            return View();
        }

        // POST: Iyeliks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,UrunID,MyUserID")] Iyelik iyelik)
        {
            if (ModelState.IsValid)
            {
                _context.Add(iyelik);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MyUserID"] = new SelectList(_context.Users, "Id", "UserName", iyelik.MyUserID);
            ViewData["UrunID"] = new SelectList(_context.Uruns, "Id", "Name", iyelik.UrunID);
            return View(iyelik);
        }

        // GET: Iyeliks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var iyelik = await _context.Iyeliks.FindAsync(id);
            if (iyelik == null)
            {
                return NotFound();
            }
            ViewData["MyUserID"] = new SelectList(_context.Users, "Id", "UserName", iyelik.MyUserID);
            ViewData["UrunID"] = new SelectList(_context.Uruns, "Id", "Name", iyelik.UrunID);
            return View(iyelik);
        }

        // POST: Iyeliks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,UrunID,MyUserID")] Iyelik iyelik)
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
            ViewData["MyUserID"] = new SelectList(_context.Users, "Id", "UserName", iyelik.MyUserID);
            ViewData["UrunID"] = new SelectList(_context.Uruns, "Id", "Name", iyelik.UrunID);
            return View(iyelik);
        }

        // GET: Iyeliks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var iyelik = await _context.Iyeliks
                .Include(i => i.MyUsers)
                .Include(i => i.Urun)
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var iyelik = await _context.Iyeliks.FindAsync(id);
            _context.Iyeliks.Remove(iyelik);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IyelikExists(int id)
        {
            return _context.Iyeliks.Any(e => e.Id == id);
        }
    }
}
