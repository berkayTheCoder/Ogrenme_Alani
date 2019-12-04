using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eticaret.Data;
using Eticaret.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Eticaret.Controllers
{
    public class KullaniciIslemleriController : Controller
    {
        private readonly VeritabaniContext context;

        public KullaniciIslemleriController(VeritabaniContext context) //ctor
        {
            this.context = context;
        }
        [HttpGet]
        public IActionResult Giris()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Giris(Kullanici kullanici)
        {
            var girisYapanKullanici = context.Kullanicilar.Where(x => x.KAdi == kullanici.KAdi && x.Sifre == kullanici.Sifre).SingleOrDefault();

            if (girisYapanKullanici != null) //böyle bir kayıt varsa
            {
                HttpContext.Session.SetString("KAdi", girisYapanKullanici.KAdi);
                HttpContext.Session.SetString("Turu", girisYapanKullanici.Turu);
                TempData["Mesaj"] = $"\"{girisYapanKullanici.KAdi}\" kullanıcısı başarıyla giriş yaptı. İşlemlerinize \"{girisYapanKullanici.Turu}\" yetkisi ile devam edebilirsiniz.";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["Mesaj"] = "Geçersiz kullanıcı adı veya şifre!";
                return View();
            }          
        }

        public IActionResult Cikis()
        {
            HttpContext.Session.Clear();
            TempData["Mesaj"] = "Geçerli oturum başarıyla sonlandırıldı.";
            return RedirectToAction("Index","Home");
        }

        public IActionResult Kayit()
        {
            HttpContext.Session.Clear();
            TempData["Mesaj"] = "Yapım aşamasında...";
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Detay()
        {
            TempData["Mesaj"] = "Yapım aşamasında...";
            return RedirectToAction("Index", "Home");
        }
    }
}