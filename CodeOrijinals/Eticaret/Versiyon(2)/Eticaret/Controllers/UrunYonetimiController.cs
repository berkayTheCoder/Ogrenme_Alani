using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Eticaret.Data;
using Eticaret.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Eticaret.Controllers
{
    public class UrunYonetimiController : Controller
    {
        private readonly UygVTContext _context;
        private readonly IHostingEnvironment hosting;

        public UrunYonetimiController(UygVTContext context, IHostingEnvironment hosting)
        {
            _context = context;
            this.hosting = hosting;
        }

        // GET: UrunYonetimi
        public async Task<IActionResult> TumUrunler()
        {
            return View(await _context.Urunler.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> TopluTasi(IFormCollection collection)
        {
            var SeciliUrunler = collection["SeciliUrunler"].ToList();
            var HedefKategoriId = Convert.ToInt32(collection["HedefKategoriId"].ToString());

            foreach (var item in SeciliUrunler)
            {
                var urun = await _context.Urunler.FindAsync(Convert.ToInt32(item));
                urun.KategoriId = HedefKategoriId;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(KategoriUrunleri),new {id = HedefKategoriId });
        }

        [HttpPost]
        public async Task<IActionResult> TopluSil(IFormCollection collection)
        {
            var SeciliUrunler = collection["SeciliUrunler"].ToList();
            int kategoriId =0;
            foreach (var item in SeciliUrunler)
            {
                var urun = await _context.Urunler.FindAsync(Convert.ToInt32(item));
                kategoriId = urun.KategoriId;
                _context.Urunler.Remove(urun);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(KategoriUrunleri), new {id= kategoriId });
        }

        public async Task<IActionResult> KategoriUrunleri(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kategori = await _context.Kategoriler
                .Include(m => m.Urunleri)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kategori == null)
            {
                return NotFound();
            }

            return View(kategori);
        }

        // GET: UrunYonetimi/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var urun = await _context.Urunler
                .FirstOrDefaultAsync(m => m.Id == id);
            if (urun == null)
            {
                return NotFound();
            }

            return View(urun);
        }

        // GET: UrunYonetimi/Create
        public IActionResult Create(int? id)
        {
            return View();
        }

        // POST: UrunYonetimi/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? id, [Bind("Adi,Stok,Dosya,Aciklama")] Urun urun)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var kategori = await _context.Kategoriler.FindAsync(id);
                if (kategori == null)
                {
                    return NotFound();
                }

                var dosyaYolu = Path.Combine(hosting.WebRootPath, "resimler");
                if (!Directory.Exists(dosyaYolu))
                {
                    Directory.CreateDirectory(dosyaYolu);
                }

                using (var dosyaAkisi = new FileStream(Path.Combine(dosyaYolu, urun.Dosya.FileName), FileMode.Create))
                {
                    await urun.Dosya.CopyToAsync(dosyaAkisi);
                }

                urun.Resim = urun.Dosya.FileName;

                urun.Kategori = kategori;


                _context.Add(urun);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(KategoriUrunleri), new {id=id });
            }
            return View(urun);
        }

        // GET: UrunYonetimi/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var urun = await _context.Urunler.FindAsync(id);
            if (urun == null)
            {
                return NotFound();
            }

            //var kategoriler = await _context.Kategoriler.ToListAsync();
            //ViewBag.Kategoriler = kategoriler;

            return View(urun);
        }

        // POST: UrunYonetimi/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,KategoriId,Adi,Stok,Aciklama,Dosya,Resim")] Urun urun)
        {
            if (id != urun.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (urun.Dosya!=null)
                    {
                        var dosyaYolu = Path.Combine(hosting.WebRootPath, "resimler");
                        if (!Directory.Exists(dosyaYolu))
                        {
                            Directory.CreateDirectory(dosyaYolu);
                        }

                        using (var dosyaAkisi = new FileStream(Path.Combine(dosyaYolu, urun.Dosya.FileName), FileMode.Create))
                        {
                            await urun.Dosya.CopyToAsync(dosyaAkisi);
                        }

                        urun.Resim = urun.Dosya.FileName; 
                    }
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
                return RedirectToAction(nameof(KategoriUrunleri),new {id=urun.KategoriId });
            }
            return View(urun);
        }

        // GET: UrunYonetimi/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var urun = await _context.Urunler
                .FirstOrDefaultAsync(m => m.Id == id);
            if (urun == null)
            {
                return NotFound();
            }

            return View(urun);
        }

        // POST: UrunYonetimi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
                var urun = await _context.Urunler.FindAsync(id);
                _context.Urunler.Remove(urun);
                await _context.SaveChangesAsync();
                TempData["Mesaj"] = "Ürün başarıyla silindi";
                return RedirectToAction(nameof(KategoriUrunleri), new { id = urun.KategoriId }); 
        }

        private bool UrunExists(int id)
        {
            return _context.Urunler.Any(e => e.Id == id);
        }
    }
}
