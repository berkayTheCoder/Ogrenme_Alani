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
    public class SiparisController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<BmUser> _userManager;

        public SiparisController(ApplicationDbContext context,
            UserManager<BmUser> userManager
            )
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Siparis
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Siparis.Include(s => s.SiparisAlanUser).Include(s => s.SiparisVerenUser).Include(s => s.Urun);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Siparis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siparis = await _context.Siparis
                .Include(s => s.SiparisAlanUser)
                .Include(s => s.SiparisVerenUser)
                .Include(s => s.Urun)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (siparis == null)
            {
                return NotFound();
            }

            return View(siparis);
        }
        private void UserAcilirlistesiBesleme(object secilenUser = null)
        {
            var UserlarQuery = from d in _context.Users
                                   orderby d.UserName
                                   select d;
            ViewBag.DepartmentID = new SelectList(UserlarQuery.AsNoTracking(), "Id", "UserName", secilenUser);
        }
        // GET: Siparis/Create
        public IActionResult Create(int? id, Urun urun, Siparis siparis)
        {
            if (id != null)
            {
                urun = _context.Uruns.Single(u => u.Id == id);
                siparis.SiparisAlanUserId = urun.BmUserId;
                siparis.SiparisVerenUserId = _userManager.GetUserId(User);
                siparis.UrunId = urun.Id;
                ViewData["SiparisAlanUserId"] = new SelectList(_context.Users, "Id", "UserName",siparis.SiparisAlanUserId);
                ViewData["SiparisVerenUserId"] = new SelectList(_context.Users, "Id", "UserName", siparis.SiparisVerenUserId);
                ViewData["UrunId"] = new SelectList(_context.Uruns, "Id", "Adi", id);
                return View(siparis);
            }
            ViewData["SiparisAlanUserId"] = new SelectList(_context.Users, "Id", "UserName");
            ViewData["SiparisVerenUserId"] = new SelectList(_context.Users, "Id", "UserName");
            ViewData["UrunId"] = new SelectList(_context.Uruns, "Id", "Adi",id);
            return View();
        }

        // POST: Siparis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SiparisVerenUserId,UrunId,SiparisAlanUserId,SiparisDurumu,Miktari,SiparisMetni")] Siparis siparis)
        {
            if (ModelState.IsValid)
            {
                _context.Add(siparis);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SiparisAlanUserId"] = new SelectList(_context.Users, "Id", "Id", siparis.SiparisAlanUserId);
            ViewData["SiparisVerenUserId"] = new SelectList(_context.Users, "Id", "Id", siparis.SiparisVerenUserId);
            ViewData["UrunId"] = new SelectList(_context.Uruns, "Id", "Id", siparis.UrunId);
            return View(siparis);
        }

        // GET: Siparis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siparis = await _context.Siparis.FindAsync(id);
            if (siparis == null)
            {
                return NotFound();
            }
            ViewData["SiparisAlanUserId"] = new SelectList(_context.Users, "Id", "UserName", siparis.SiparisAlanUserId);
            ViewData["SiparisVerenUserId"] = new SelectList(_context.Users, "Id", "UserName", siparis.SiparisVerenUserId);
            ViewData["UrunId"] = new SelectList(_context.Uruns, "Id", "Adi", siparis.UrunId);
            return View(siparis);
        }

        // POST: Siparis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SiparisVerenUserId,UrunId,SiparisAlanUserId,SiparisDurumu,Miktari,SiparisMetni")] Siparis siparis)
        {
            if (id != siparis.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(siparis);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SiparisExists(siparis.Id))
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
            ViewData["SiparisAlanUserId"] = new SelectList(_context.Users, "Id", "Id", siparis.SiparisAlanUserId);
            ViewData["SiparisVerenUserId"] = new SelectList(_context.Users, "Id", "Id", siparis.SiparisVerenUserId);
            ViewData["UrunId"] = new SelectList(_context.Uruns, "Id", "Id", siparis.UrunId);
            return View(siparis);
        }

        // GET: Siparis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siparis = await _context.Siparis
                .Include(s => s.SiparisAlanUser)
                .Include(s => s.SiparisVerenUser)
                .Include(s => s.Urun)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (siparis == null)
            {
                return NotFound();
            }

            return View(siparis);
        }

        // POST: Siparis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var siparis = await _context.Siparis.FindAsync(id);
            _context.Siparis.Remove(siparis);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SiparisExists(int id)
        {
            return _context.Siparis.Any(e => e.Id == id);
        }
    }
}
