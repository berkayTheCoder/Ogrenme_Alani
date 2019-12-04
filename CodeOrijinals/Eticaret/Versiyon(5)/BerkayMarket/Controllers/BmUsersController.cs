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
    public class BmUsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<BmUser> _userManager;

        public BmUsersController(ApplicationDbContext context,
            UserManager<BmUser> userManager
            )
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: BmUsers
        public async Task<IActionResult> Index()
        {
            return View(await _userManager.Users.ToListAsync());
        }
        
        // GET: BmUsers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bmUser = await _userManager.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bmUser == null)
            {
                return NotFound();
            }

            return View(bmUser);
        }

        // GET: BmUsers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BmUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] BmUser bmUser)
        {
            if (ModelState.IsValid)
            {
                await _userManager.CreateAsync(bmUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bmUser);
        }

        // GET: BmUsers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bmUser = await _userManager.FindByIdAsync(id);
            if (bmUser == null)
            {
                return NotFound();
            }
            return View(bmUser);
        }

        // POST: BmUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] BmUser bmUser)
        {
            if (id != bmUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _userManager.UpdateAsync(bmUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BmUserExists(bmUser.Id))
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
            return View(bmUser);
        }

        // GET: BmUsers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bmUser = await _userManager.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bmUser == null)
            {
                return NotFound();
            }

            return View(bmUser);
        }

        // POST: BmUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var bmUser = await _userManager.FindByIdAsync(id);
            await _userManager.DeleteAsync(bmUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BmUserExists(string id)
        {
            return _userManager.Users.Any(e => e.Id == id);
        }
    }
}
