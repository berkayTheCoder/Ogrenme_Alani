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
    public class BmUserRolesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<BmRole> _roleManager;
        private readonly UserManager<BmUser> _userManager;
        private readonly IServiceProvider _service;

        public BmUserRolesController(ApplicationDbContext context,
            RoleManager<BmRole> roleManager,
            UserManager<BmUser> userManager,
            IServiceProvider service)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
            _service = service;
        }

        // GET: BmUserRoles
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserRoles.ToListAsync());
        }

        // GET: BmUserRoles/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bmUserRole = await _context.UserRoles
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (bmUserRole == null)
            {
                return NotFound();
            }

            return View(bmUserRole);
        }

        // GET: BmUserRoles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BmUserRoles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,RoleId")] BmUserRole bmUserRole)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bmUserRole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bmUserRole);
        }

        // GET: BmUserRoles/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bmUserRole = await _context.UserRoles.FindAsync(id);
            if (bmUserRole == null)
            {
                return NotFound();
            }
            return View(bmUserRole);
        }

        // POST: BmUserRoles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("UserId,RoleId")] BmUserRole bmUserRole)
        {
            if (id != bmUserRole.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bmUserRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BmUserRoleExists(bmUserRole.UserId))
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
            return View(bmUserRole);
        }

        // GET: BmUserRoles/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bmUserRole = await _context.UserRoles
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (bmUserRole == null)
            {
                return NotFound();
            }

            return View(bmUserRole);
        }

        // POST: BmUserRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var bmUserRole = await _context.UserRoles.FindAsync(id);
            _context.UserRoles.Remove(bmUserRole);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BmUserRoleExists(string id)
        {
            return _context.UserRoles.Any(e => e.UserId == id);
        }
    }
}
