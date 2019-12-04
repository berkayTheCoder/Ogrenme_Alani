using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DukkanOnline9.Data;
using DukkanOnline9.Models;
using DukkanOnline9.Models.DukkanViewModels;

namespace DukkanOnline9.Controllers
{
    public class KullaniciRolsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KullaniciRolsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: KullaniciRols
        public async Task<IActionResult> Index()
        {
            return View(await _context.KullaniciRol.ToListAsync());
        }

        // GET: KullaniciRols/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kullaniciRol = await _context.KullaniciRol
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (kullaniciRol == null)
            {
                return NotFound();
            }

            return View(kullaniciRol);
        }

        // GET: KullaniciRols/Create1
        public IActionResult Create1(Kullanici kullanici, Rol rol)
        {
            ViewData["Kullanici"] = new SelectList(_context.Kullanicis, kullanici.Id.ToString(), kullanici.UserName);
            ViewData["Rol"] = new SelectList(_context.Roles, rol.Id.ToString(), rol.Name);
            return View();
        }
        public IActionResult Create(KullaniciRol kullaniciRol)
        {
            ViewData["Kullanici"] = new SelectList(_context.Kullanicis, kullaniciRol.UserId.ToString(), kullaniciRol.RolKullanicisi.UserName.ToString());
            ViewData["Rol"] = new SelectList(_context.Roles, kullaniciRol.RoleId.ToString(), kullaniciRol.KullaniciRolu.Name.ToString());
            return View(kullaniciRol);
        }
        public IActionResult Create2(
                                        KullaniciRolCreateData viewModel,
                                        KullaniciRol kullaniciRol
                                    )
        {
            ViewData["Kullanici"] = new SelectList(viewModel.Kullanicis, kullaniciRol.UserId.ToString(), kullaniciRol.RolKullanicisi.UserName.ToString());
            ViewData["Rol"] = new SelectList(viewModel.Rols, kullaniciRol.RoleId.ToString(), kullaniciRol.KullaniciRolu.Name.ToString());
            return View(viewModel);
        }

        // POST: KullaniciRols/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost([Bind("UserId,RoleId")] KullaniciRol kullaniciRol)
        {
            if (ModelState.IsValid)
            {
                kullaniciRol.UserId = Guid.NewGuid();
                _context.Add(kullaniciRol);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kullaniciRol);
        }

        // GET: KullaniciRols/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kullaniciRol = await _context.KullaniciRol.FindAsync(id);
            if (kullaniciRol == null)
            {
                return NotFound();
            }
            return View(kullaniciRol);
        }

        // POST: KullaniciRols/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("UserId,RoleId")] KullaniciRol kullaniciRol)
        {
            if (id != kullaniciRol.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kullaniciRol);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KullaniciRolExists(kullaniciRol.UserId))
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
            return View(kullaniciRol);
        }

        // GET: KullaniciRols/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kullaniciRol = await _context.KullaniciRol
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (kullaniciRol == null)
            {
                return NotFound();
            }

            return View(kullaniciRol);
        }

        // POST: KullaniciRols/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var kullaniciRol = await _context.KullaniciRol.FindAsync(id);
            _context.KullaniciRol.Remove(kullaniciRol);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KullaniciRolExists(Guid id)
        {
            return _context.KullaniciRol.Any(e => e.UserId == id);
        }
    }
}
