using KendiDukkanim.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KendiDukkanim.Data
{
    public class VtBaslatici
    {
        public static void Baslat(KendiDukkanimContext dukkanimContext)
        {
            if (dukkanimContext.Satici.Any())
            {
                return;
            }

            var saticilar = new Satici[]
            {
                new Satici{ FirstMidName="Şükrü", LastName="Berkay" }
            };
            foreach (Satici item in saticilar)
            {
                dukkanimContext.Satici.Add(item);
            }
            dukkanimContext.SaveChanges();


            var urunler = new Urun[]
            {
                new Urun{ Adi="Deneme_u1", Puani=5 },
                new Urun{ Adi="Deneme_u2", Puani=4 },
                new Urun{ Adi="Deneme_u3", Puani=3, Stok=33 }
            };
            foreach (Urun item in urunler)
            {
                dukkanimContext.Urun.Add(item);
            }
            dukkanimContext.SaveChanges();

            //var satislar = new Satis[]
            //{
            //    new Satis{ Adi="Deneme_u1", Puani=5 },
            //    new Satis{ Adi="Deneme_u2", Puani=4 },
            //    new Satis{ Adi="Deneme_u3", Puani=3, Stok=33 }
            //};
            //foreach (Urun item in urunler)
            //{
            //    dukkanimContext.Urun.Add(item);
            //}
            //dukkanimContext.SaveChanges();
        }
    }
}
