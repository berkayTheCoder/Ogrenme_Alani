using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DukkanOnline3.Data;
using DukkanOnline3.Models;
/*
Namespace DukkanOnline3.Controllers
{
    public class ApplicationRolesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApplicationRolesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ApplicationRoles
        public async Task<IActionResult> Index()
        {
            return View(await _context.MyRole.ToListAsync());
        }

        // GET: ApplicationRoles/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var MyRole = await _context.MyRole
                .FirstOrDefaultAsync(m => m.Id == id);
            if (MyRole == null)
            {
                return NotFound();
            }

            return View(MyRole);
        }

        // GET: MyRoles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MyRoles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,NormalizedName,ConcurrencyStamp")] MyRole MyRole)
        {
            if (ModelState.IsValid)
            {
                _context.Add(MyRole);
                await _context.SaveChangesAsync();
                return RedirectToAction(Nameof(Index));
            }
            return View(MyRole);
        }

        // GET: MyRoles/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var MyRole = await _context.MyRole.FindAsync(id);
            if (MyRole == null)
            {
                return NotFound();
            }
            return View(MyRole);
        }

        // POST: MyRoles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,NormalizedName,ConcurrencyStamp")] MyRole MyRole)
        {
            if (id != MyRole.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(MyRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MyRoleExists(MyRole.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(Nameof(Index));
            }
            return View(MyRole);
        }

        // GET: MyRoles/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var MyRole = await _context.MyRole
                .FirstOrDefaultAsync(m => m.Id == id);
            if (MyRole == null)
            {
                return NotFound();
            }

            return View(MyRole);
        }

        // POST: MyRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var MyRole = await _context.MyRole.FindAsync(id);
            _context.MyRole.Remove(MyRole);
            await _context.SaveChangesAsync();
            return RedirectToAction(Nameof(Index));
        }

        private bool MyRoleExists(string id)
        {
            return _context.MyRole.Any(e => e.Id == id);
        }
    }
}
*/