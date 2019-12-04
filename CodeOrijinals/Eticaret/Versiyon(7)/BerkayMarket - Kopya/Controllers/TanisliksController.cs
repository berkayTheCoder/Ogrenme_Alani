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
using BerkayMarket.Authorization;

namespace BerkayMarket.Controllers
{
    public class TanisliksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<BmUser> _userManager;
        private readonly IAuthorizationService _authorizationService;

        public TanisliksController(ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<BmUser> userManager
            )
        {
            _context = context;
            _userManager = userManager;
            _authorizationService = authorizationService;
        }

        // GET: Tanisliks
        public async Task<IActionResult> Index()
        {
            //var applicationDbContext = _context.Tanislik.Include(t => t.Tanis);
            var tanisliks = from c in _context.Tanislik.Include(t => t.Tanis)
                            select c;
            var isAuthorized = User.IsInRole(Constants.TanisManagerRole) ||
                   User.IsInRole(Constants.TanisAdminRolu);

            var currentUserId = _userManager.GetUserId(User);

            // Only approved contacts are shown UNLESS you're authorized to see them
            // or you are the owner.
            if (!isAuthorized)
            {
                tanisliks = tanisliks.Where(c => c.Durumlar == TanislikDurumlari.Onaylı
                                            || c.IyeId == currentUserId || c.TanisId == currentUserId) ;
            }
            return View(await tanisliks.ToListAsync());
            //return View(await applicationDbContext.ToListAsync());
        }

        // GET: Tanisliks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tanislik = await _context.Tanislik
                .Include(t => t.Tanis)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tanislik == null)
            {
                return NotFound();
            }

            return View(tanislik);
        }
        [HttpPost]
        public async Task<IActionResult> Details(int id, TanislikDurumlari durum)
        {
            var tanislik = await _context.Tanislik.FindAsync(id);

            if (tanislik == null)
            {
                return NotFound();
            }

            var tanislikIslemi = (durum == TanislikDurumlari.Onaylı)
                                                       ? TanisIslemleri.Onayla
                                                       : TanisIslemleri.Reject;

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, tanislik,
                                        tanislikIslemi);
            if (!isAuthorized.Succeeded)
            {
                return new ChallengeResult();
            }
            tanislik.Durumlar = durum;
            _context.Tanislik.Update(tanislik);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", new { id });
        }
        // GET: Tanisliks/Create
        public IActionResult Create()
        {
            //var user = User.ToString();
            //ViewData["Iye"] = new SelectList(user, "Id", "Username");
            //var users = _userManager.Users.ToList();
            //ViewData["TanisId"] = new list(users, "Id", "Username");
            return View();
        }

        // POST: Tanisliks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IyeId,TanisId,Durumlar")] Tanislik tanislik)
        {
            if (ModelState.IsValid)
            {

                tanislik.IyeId = _userManager.GetUserId(User);
                var isAuthorized = await _authorizationService.AuthorizeAsync(
                                            User, tanislik,
                                            TanisIslemleri.Create);
                if (!isAuthorized.Succeeded)
                {
                    return new ChallengeResult();
                }
                var eskiTanislik = _context.Tanislik.FirstOrDefault(t => t.IyeId == tanislik.IyeId
                                                                        && t.TanisId == tanislik.TanisId);
                if (eskiTanislik!=null)
                {
                    TempData["Ileti"] = $"Bu Tanışlık daha önce kurulmuş";
                    return RedirectToAction(nameof(Index));
                }
                _context.Add(tanislik);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TanisId"] = new SelectList(_context.Set<BmUser>(), "Id", "Id", tanislik.TanisId);
            return View(tanislik);
        }

        // GET: Tanisliks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tanislik = await _context.Tanislik.FindAsync(id);
            if (tanislik == null)
            {
                return NotFound();
            }
            var Yetkilendirme = await _authorizationService.AuthorizeAsync(
                                                      User, tanislik,
                                                      TanisIslemleri.Update);
            if (!Yetkilendirme.Succeeded)
            {
                return new ChallengeResult();
            }
            ViewData["TanisId"] = new SelectList(_userManager.Users, "Id", "UserName", tanislik.TanisId);
            return View(tanislik);
        }

        // POST: Tanisliks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IyeId,TanisId,Durumlar")] Tanislik tanislik)
        {
            if (id != tanislik.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    var Yetkilendirme = await _authorizationService.AuthorizeAsync(
                                                     User, tanislik,
                                                     TanisIslemleri.Update);
                    if (!Yetkilendirme.Succeeded)
                    {
                        return new ChallengeResult();
                    }


                    _context.Attach(tanislik).State = EntityState.Modified;

                    if (tanislik.Durumlar == TanislikDurumlari.Onaylı)
                    {
                        // If the contact is updated after approval, 
                        // and the user cannot approve,
                        // set the status back to submitted so the update can be
                        // checked and approved.
                        var canApprove = await _authorizationService.AuthorizeAsync(User,
                                                tanislik,
                                                TanisIslemleri.Onayla);

                        if (!canApprove.Succeeded)
                        {
                            tanislik.Durumlar = TanislikDurumlari.Onaylı;
                        }
                    }
                    _context.Update(tanislik);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TanislikExists(tanislik.Id))
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
            ViewData["TanisId"] = new SelectList(_context.Set<BmUser>(), "Id", "Id", tanislik.TanisId);
            return View(tanislik);
        }

        // GET: Tanisliks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tanislik = await _context.Tanislik
                .Include(t => t.Tanis)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tanislik == null)
            {
                return NotFound();
            }

            return View(tanislik);
        }

        // POST: Tanisliks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tanislik = await _context.Tanislik.FindAsync(id);
            _context.Tanislik.Remove(tanislik);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TanislikExists(int id)
        {
            return _context.Tanislik.Any(e => e.Id == id);
        }
    }
}
