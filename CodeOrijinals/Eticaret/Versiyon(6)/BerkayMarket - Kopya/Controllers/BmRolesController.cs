using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BerkayMarket.Data;
using BerkayMarket.Models;
using Microsoft.AspNetCore.Identity;

namespace BerkayMarket.Controllers
{
    public class BmRolesController : Controller
    {
        //private readonly ApplicationDbContext _context;
        private readonly RoleManager<BmRole> _roleManager;

        public BmRolesController(
            //ApplicationDbContext context,
            RoleManager<BmRole> roleManager)
        {
            //_context = context;
            _roleManager = roleManager;
        }

        // GET: BmRoles
        public async Task<IActionResult> Index()
        {
            return View(await _roleManager.Roles.ToListAsync());
        }

        // GET: BmRoles/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bmRole = await _roleManager.Roles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bmRole == null)
            {
                return NotFound();
            }

            return View(bmRole);
        }

        // GET: BmRoles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BmRoles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,NormalizedName,ConcurrencyStamp")] BmRole bmRole)
        {
            if (ModelState.IsValid)
            {
                await _roleManager.CreateAsync(bmRole);////////////////
                //await _roleManager.
                await _roleManager.UpdateAsync(bmRole);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bmRole);
        }

        // GET: BmRoles/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bmRole = await _roleManager.FindByIdAsync(id);
            if (bmRole == null)
            {
                return NotFound();
            }
            return View(bmRole);
        }

        // POST: BmRoles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,NormalizedName,ConcurrencyStamp")] BmRole bmRole)
        {
            if (id != bmRole.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(bmRole);
                    await _roleManager.UpdateAsync(bmRole);/////////////
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BmRoleExists(bmRole.Id))
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
            return View(bmRole);
        }

        // GET: BmRoles/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bmRole = await _roleManager.Roles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bmRole == null)
            {
                return NotFound();
            }

            return View(bmRole);
        }

        // POST: BmRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var bmRole = await _roleManager.FindByIdAsync(id);
            await _roleManager.DeleteAsync(bmRole);
            //_context.Roles.Remove(bmRole);///////////////
            await _roleManager.UpdateAsync(bmRole);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BmRoleExists(string id)
        {
            return _roleManager.Roles.Any(e => e.Id == id);
        }
    }
}
