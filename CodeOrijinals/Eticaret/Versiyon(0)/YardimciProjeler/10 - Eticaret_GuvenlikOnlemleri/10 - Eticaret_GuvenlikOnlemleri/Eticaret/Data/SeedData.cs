using Eticaret.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eticaret.Data
{
    public static class SeedData
    {
        public static void Seed(IApplicationBuilder app)
        {
            VeritabaniContext context = app.ApplicationServices.GetRequiredService<VeritabaniContext>();

            context.Database.Migrate(); //Bekleyen migrationları yürürlüğe koyar,işletir

            if (!context.Urunler.Any())//ürünler tablosu boşsa
            {
                var kategoriler = new Kategori[]
                {
                    new Kategori{Adi="Elektronik"}, // kategoriler[0]--->elektronik kategorisinin referansı
                    new Kategori{Adi="Ev / Yaşam"}, // kategoriler[1]
                    new Kategori{Adi="Kozmetik"},   // kategoriler[2]

                };
                context.Kategoriler.AddRange(kategoriler);

                var urunler = new Urun[]
                {

                    new Urun{Adi="Telefon",Stok=30,Resim="Telefon.jpg",Kategori=kategoriler[0]},
                    new Urun{Adi="Tablet",Stok=40,Resim="Tablet.jpg",Kategori=kategoriler[0] },
                    new Urun{Adi="Televizyon",Stok=50,Resim="Televizyon.jpg",Kategori=kategoriler[0]},
                    new Urun{Adi="Bilgisayar",Stok=70 ,Resim="Bilgisayar.jpg",Kategori=kategoriler[0]},
                    new Urun{Adi="Koltuk Takımı",Stok=70,Resim="koltuktakimi.jpg",Kategori=kategoriler[1]},
                    new Urun{Adi="Masa Lambası",Stok=70,Resim="masalambasi.jpg",Kategori=kategoriler[1] },
                    new Urun{Adi="Yatak",Stok=70,Resim="yatak.jpg",Kategori=kategoriler[1]},
                    new Urun{Adi="Yemek Takımı",Stok=70,Resim="yemektakimi.jpg",Kategori=kategoriler[1] },
                    new Urun{Adi="Maskara",Stok=70,Resim="maskara.jpg",Kategori=kategoriler[2] },
                    new Urun{Adi="Diş Fırçası",Stok=70 ,Resim="disfircasi.jpg",Kategori=kategoriler[2]},
                    new Urun{Adi="Parfüm",Stok=70,Resim="parfum.jpg",Kategori=kategoriler[2] },
                };
                context.AddRange(urunler);

                var kullanicilar = new Kullanici[]
                {
                    new Kullanici{KAdi="Ahmet",Sifre="123",Turu="Admin"},
                    new Kullanici{KAdi="Mehmet",Sifre="321",Turu="User"}
                };
                context.AddRange(kullanicilar);

                context.SaveChanges();
            }


        }
    }
}
