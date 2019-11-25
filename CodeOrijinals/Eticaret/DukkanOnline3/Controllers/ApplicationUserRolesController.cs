using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DukkanOnline3.Data;
using DukkanOnline3.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
/*
Namespace DukkanOnline3.Controllers
{
    public class ApplicationUserRolesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IServiceProvider _serviceProvider;

        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public ApplicationUserRolesController(ApplicationDbContext context,
            RoleManager<ApplicationRole> roleManager,
            UserManager<IdentityUser> userManager,
            IServiceProvider serviceProvider)
        {
            _context = context;
            //_roleManager = roleManager;
            _userManager = userManager;
            _serviceProvider = serviceProvider;
        }

        // GET: ApplicationUserRoles
        public async Task<IActionResult> Index()
        {
            return View(await _context.ApplicationUserRole.ToListAsync());
        }

        // GET: ApplicationUserRoles/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUserRole = await _context.ApplicationUserRole
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (applicationUserRole == null)
            {
                return NotFound();
            }

            return View(applicationUserRole);
        }

        // GET: ApplicationUserRoles/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName");
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name");
            return View();
        }

        // POST: ApplicationUserRoles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,RoleId")] ApplicationUserRole applicationUserRole, 
            ApplicationDbContext context,
            RoleManager<ApplicationRole> roleManager,
            UserManager<IdentityUser> userManager)
        {
            if (ModelState.IsValid)
            {

                //_context.Add(applicationUserRole);
                var user = await _userManager.FindByIdAsync(applicationUserRole.UserId);
                var role = await _roleManager.FindByIdAsync(applicationUserRole.RoleId);
                await _userManager.AddToRoleAsync(user, role.Name);
                await _context.SaveChangesAsync();
                return RedirectToAction(Nameof(Index));
            }
            return View(applicationUserRole);
        }

        // GET: ApplicationUserRoles/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUserRole = await _context.ApplicationUserRole.FindAsync(id);
            if (applicationUserRole == null)
            {
                return NotFound();
            }
            return View(applicationUserRole);
        }

        // POST: ApplicationUserRoles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("UserId,RoleId")] ApplicationUserRole applicationUserRole)
        {
            if (id != applicationUserRole.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicationUserRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationUserRoleExists(applicationUserRole.UserId))
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
            return View(applicationUserRole);
        }

        // GET: ApplicationUserRoles/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUserRole = await _context.ApplicationUserRole
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (applicationUserRole == null)
            {
                return NotFound();
            }

            return View(applicationUserRole);
        }

        // POST: ApplicationUserRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var applicationUserRole = await _context.ApplicationUserRole.FindAsync(id);
            _context.ApplicationUserRole.Remove(applicationUserRole);
            await _context.SaveChangesAsync();
            return RedirectToAction(Nameof(Index));
        }

        private bool ApplicationUserRoleExists(string id)
        {
            return _context.ApplicationUserRole.Any(e => e.UserId == id);
        }
    }
}
*/