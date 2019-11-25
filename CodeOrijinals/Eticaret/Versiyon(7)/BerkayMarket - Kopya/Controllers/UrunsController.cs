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
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace BerkayMarket.Controllers
{
    public class UrunsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<BmUser> _userManager;
        private readonly IHostingEnvironment hosting;

        public UrunsController(ApplicationDbContext context,
            UserManager<BmUser> userManager,
            IHostingEnvironment hosting
            )
        {
            _context = context;
            _userManager = userManager;
            this.hosting = hosting;
        }

#if false
        // GET: Uruns
        public async Task<IActionResult> Index()
        {
            //var urols = _context.Urol;
            //foreach (var item in urols)
            //{
            //    var urunUrol = item.Adi;
            var applicationDbContext = _context.Uruns
                .Include(u => u.BmUser)
                .Include(u => u.Kategori);
            return View(await applicationDbContext.ToListAsync());
        }
        //}
#elif true // sıralama kodları
        // GET: Students
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {
            var applicationDbContext = _context.Uruns
                .Include(u => u.BmUser)
                .Include(u => u.Kategori);

            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["KategoriSortParm"] = String.IsNullOrEmpty(sortOrder) ? "kategori_desc" : "";
            ViewData["StokSortParm"] = sortOrder == "Stok" ? "stok_desc" : "Stok";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var uruns = from s in _context.Uruns
                            .Include(u => u.BmUser)
                            .Include(u => u.Kategori)
                            select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                uruns = uruns.Where(s => s.Adi.Contains(searchString)
                                       || s.Aciklama.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    uruns = uruns.OrderByDescending(s => s.Adi);
                    break;
                case "kategori_desc":
                    uruns = uruns.OrderByDescending(s => s.Kategori.Adi);
                    break;
                case "Stok":
                    uruns = uruns.OrderBy(s => s.Stok);
                    break;
                case "stok_desc":
                    uruns = uruns.OrderByDescending(s => s.Stok);
                    break;
                default:
                    uruns = uruns.OrderBy(s => s.Aciklama);
                    break;
            }

            int pageSize = 3;
            return View(await PaginatedList<Urun>.CreateAsync(uruns.AsNoTracking(), pageNumber ?? 1, pageSize));
        }
#endif

        public async Task<IActionResult> KategoriUrunleri(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kategori = await _context.Kategoris
                .Include(u => u.Urunleri)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kategori == null)
            {
                return NotFound();
            }

            return View(kategori);
        }
        public async Task<IActionResult> SaticiUrunleri(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                //.Include(u => u.Iyeliks)
                .Include(u => u.Uruns)
                    //.ThenInclude(u =>u.)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        // GET: Uruns/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var urun = await _context.Uruns
                .Include(u => u.BmUser)
                .Include(u => u.Kategori)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (urun == null)
            {
                return NotFound();
            }

            return View(urun);
        }
        
        // GET: Uruns/Create
        public IActionResult Create(string uid,int? kid)
        {
            if (uid!=null)
            {
                uid = _userManager.GetUserId(User); 
            }
            ViewData["BmUserId"] = new SelectList(_context.Users, "Id", "UserName",uid);
            ViewData["KategoriId"] = new SelectList(_context.Kategoris, "Id", "Adi",kid);
            return View();
        }

        // POST: Uruns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Adi,Stok,KategoriId,Dosya,Aciklama,BmUserId")] Urun urun)
        {
            if (ModelState.IsValid)
            {
                var dosyaYolu = Path.Combine(hosting.WebRootPath, "images");
                if (!Directory.Exists(dosyaYolu))
                {
                    Directory.CreateDirectory(dosyaYolu);
                }

                using (var dosyaAkisi = new FileStream(Path.Combine(dosyaYolu, urun.Dosya.FileName), FileMode.Create))
                {
                    await urun.Dosya.CopyToAsync(dosyaAkisi);
                }

                urun.Resim = urun.Dosya.FileName;
                
                //var urol = new UrunUrol(urun, User);
                _context.Add(urun);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BmUserId"] = new SelectList(_context.Users, "Id", "UserName", urun.BmUserId);
            ViewData["KategoriId"] = new SelectList(_context.Kategoris, "Id", "Adi", urun.KategoriId);
            return View(urun);
        }

        // GET: Uruns/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var urun = await _context.Uruns.FindAsync(id);
            if (urun == null)
            {
                return NotFound();
            }
            ViewData["BmUserId"] = new SelectList(_context.Users, "Id", "UserName", urun.BmUserId);
            ViewData["KategoriId"] = new SelectList(_context.Kategoris, "Id", "Adi", urun.KategoriId);
            return View(urun);
        }

        // POST: Uruns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Adi,Stok,KategoriId,Resim,Aciklama,BmUserId")] Urun urun)
        {
            if (id != urun.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(urun);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UrunExists(urun.Id))
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
            ViewData["BmUserId"] = new SelectList(_context.Users, "Id", "UserName", urun.BmUserId);
            ViewData["KategoriId"] = new SelectList(_context.Kategoris, "Id", "Adi", urun.KategoriId);
            return View(urun);
        }

        // GET: Uruns/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var urun = await _context.Uruns
                .Include(u => u.BmUser)
                .Include(u => u.Kategori)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (urun == null)
            {
                return NotFound();
            }

            return View(urun);
        }

        // POST: Uruns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var urun = await _context.Uruns.FindAsync(id);
            _context.Uruns.Remove(urun);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UrunExists(int id)
        {
            return _context.Uruns.Any(e => e.Id == id);
        }
    }
}
