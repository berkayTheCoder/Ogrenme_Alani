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
using Microsoft.AspNetCore.Authorization;

namespace BerkayMarket.Controllers
{
    [Authorize(Roles = "Admin")]
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
            ViewData["UserId"] = new SelectList(_userManager.Users, "Id", "UserName");
            ViewData["RoleId"] = new SelectList(_roleManager.Roles, "Id", "Name");
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
                var user = await _userManager.FindByIdAsync(bmUserRole.UserId);
                var rol = await _roleManager.FindByIdAsync(bmUserRole.RoleId);
                await _userManager.AddToRoleAsync(user, rol.Name);
                //_context.Add(bmUserRole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bmUserRole);
        }
#if false
        //gerek kalmadı
        // GET: BmUserRoles/Edit/5
        public async Task<IActionResult> Edit(string id, string rid)
        {
            if (id == null)
            {
                return NotFound();
            }


            var bmUserRole = await _context.UserRoles
                .FirstOrDefaultAsync(m => m.UserId == id && m.RoleId == rid);
            //var bmUserRole = await _context.UserRoles.FindAsync(id);
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

            var user = await _userManager.FindByIdAsync(bmUserRole.UserId);
            var rol = await _roleManager.FindByIdAsync(bmUserRole.RoleId);
            if (ModelState.IsValid)
            {
                try
                {
                    await _userManager.role();
                    //_context.Update(bmUserRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    //if (!_context.UserRoles.Single(ur=>ur.UserId==bmUserRole.UserId && ur.RoleId == bmUserRole.RoleId)==null)
                    if (bmUserRole == null)
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
#endif

        // GET: BmUserRoles/Delete/5
        public async Task<IActionResult> Delete(string id, string rid)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bmUserRole = await _context.UserRoles
                .FirstOrDefaultAsync(m => m.UserId == id&&m.RoleId==rid);
            if (bmUserRole == null)
            {
                return NotFound();
            }

            //return RedirectToAction("Delete", "BmUserRoles", });
            //return RedirectToAction("Delete","BmUserRoles", new { id, rid });
            return View(bmUserRole);
        }

        // POST: BmUserRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(BmUserRole bmUserRole)
        {
            //rid = bmUserRole
            //var bmUserRole = _context.UserRoles
            //    .FirstOrDefault(ur=>ur.UserId == id && ur.RoleId==rid);

            var user = await _userManager.FindByIdAsync(bmUserRole.UserId);
            var rol = await _roleManager.FindByIdAsync(bmUserRole.RoleId);
            await _userManager.RemoveFromRoleAsync(user, rol.Name);
            //_context.UserRoles.Remove(bmUserRole);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
#if false //gerek kalmadı

        private bool BmUserRoleExists(string id)
        {
            return _context.UserRoles.Any(e => e.UserId == id);
        } 
#endif
    }
}
