using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Eticaret.Data;
using Eticaret.Models;
using Microsoft.AspNetCore.Identity;

namespace Eticaret.Controllers
{
    public class EticaretRolesController : Controller
    {
        private readonly UygVTContext _context;
        private readonly RoleManager<EticaretRole> _roleManager;

        public EticaretRolesController(
                UygVTContext context,
                RoleManager<EticaretRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }

        // GET: EticaretRoles
        public async Task<IActionResult> Index()
        {
            return View(await _roleManager.Roles.ToListAsync());
        }

        // GET: EticaretRoles/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eticaretRole = await _roleManager.Roles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eticaretRole == null)
            {
                return NotFound();
            }

            return View(eticaretRole);
        }

        // GET: EticaretRoles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EticaretRoles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,NormalizedName")] EticaretRole eticaretRole)
        {
            if (ModelState.IsValid)
            {
                await _roleManager.CreateAsync(eticaretRole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eticaretRole);
        }

        // GET: EticaretRoles/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eticaretRole = await _roleManager.Roles.SingleAsync(r=>r.Id==id);
            if (eticaretRole == null)
            {
                return NotFound();
            }
            return View(eticaretRole);
        }

        // POST: EticaretRoles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,NormalizedName,ConcurrencyStamp")] EticaretRole eticaretRole)
        {
            if (id != eticaretRole.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _roleManager.UpdateAsync(eticaretRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EticaretRoleExists(eticaretRole.Id))
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
            return View(eticaretRole);
        }

        // GET: EticaretRoles/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eticaretRole = await _roleManager.Roles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eticaretRole == null)
            {
                return NotFound();
            }

            return View(eticaretRole);
        }

        // POST: EticaretRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var eticaretRole = await _roleManager.FindByIdAsync(id);
            await _roleManager.DeleteAsync(eticaretRole);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EticaretRoleExists(string id)
        {
            return _roleManager.Roles.Any(e => e.Id == id);
        }
    }
}
