using BerkayMarket.Authorization;
using BerkayMarket.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BerkayMarket.Data
{
    public static class SeedData
    {

        public static async Task Initialize(IServiceProvider services, string testUserPw)
        {
            using (var context = new ApplicationDbContext(
                services.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                var adminID = await UserUretIDsiniGetir(services, testUserPw, "admin@qwerty.com");
                await RolAta(services, adminID, Constants.AdminRole);

                var managerID = await UserUretIDsiniGetir(services, testUserPw, "manager@qwerty.com");
                await RolAta(services, managerID, Constants.ManagerRole);

                var uyeID = await UserUretIDsiniGetir(services, testUserPw, "uye123@qwerty.com");
                await RolAta(services, uyeID, Constants.Uye);

                var aliciID = await UserUretIDsiniGetir(services, testUserPw, "alici123@qwerty.com");
                await RolAta(services, aliciID, Constants.Uye);

                Seed(context, services, adminID, managerID, uyeID, aliciID);
            }
        }

        private static async Task<IdentityResult> RolAta(IServiceProvider serviceProvider,
                                                                              string uid, string role)
        {
            IdentityResult IR = null;
            var roleManager = serviceProvider.GetService<RoleManager<BmRole>>();

            if (roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            if (await roleManager.FindByNameAsync(role) == null)
            {
                IR = await roleManager.CreateAsync(new BmRole(role));
            }

            var userManager = serviceProvider.GetService<UserManager<BmUser>>();

            var user = await userManager.FindByIdAsync(uid);

            if (user == null)
            {
                throw new Exception("The testUserPw password was probably not strong enough!");
            }

            IR = await userManager.AddToRoleAsync(user, role);

            return IR;
        }


        private static async Task<string> UserUretIDsiniGetir(IServiceProvider serviceProvider,
                                                    string testUserPw, string userName)
        {
            var userManager = serviceProvider.GetService<UserManager<BmUser>>();

            var user = await userManager.FindByNameAsync(userName);
            if (user == null)
            {
                user = new BmUser { UserName = userName };
                await userManager.CreateAsync(user, testUserPw);
            }

            return user.Id;
        }

        public static void Seed(ApplicationDbContext context, IServiceProvider services, string adminID, string managerID, string uyeID, string aliciID)
        {
            
            context.Database.Migrate(); //Bekleyen migrationları yürürlüğe koyar,işletir


            if (!context.Uruns.Any())//ürünler tablosu boşsa
            {
                var kategoriler = new Kategori[]
                {
                    new Kategori{Adi="Elektronik"}, // kategoriler[0]--->elektronik kategorisinin referansı
                    new Kategori{Adi="Ev / Yaşam"}, // kategoriler[1]
                    new Kategori{Adi="Kozmetik"},   // kategoriler[2]

                };
                context.Kategoris.AddRange(kategoriler);

                var urunler = new Urun[]
                {

                    new Urun{Adi="Telefon",Stok=30,Resim="Telefon.jpg",Kategori=kategoriler[0], BmUserId=adminID},
                    new Urun{Adi="Tablet",Stok=40,Resim="Tablet.jpg",Kategori=kategoriler[0], BmUserId=adminID },
                    new Urun{Adi="Televizyon",Stok=50,Resim="Televizyon.jpg",Kategori=kategoriler[0], BmUserId=adminID},
                    new Urun{Adi="Bilgisayar",Stok=70 ,Resim="Bilgisayar.jpg",Kategori=kategoriler[0], BmUserId=adminID},
                    new Urun{Adi="Koltuk Takımı",Stok=70,Resim="koltuktakimi.jpg",Kategori=kategoriler[1], BmUserId=adminID},
                    new Urun{Adi="Masa Lambası",Stok=70,Resim="masalambasi.jpg",Kategori=kategoriler[1], BmUserId=adminID },
                    new Urun{Adi="Yatak",Stok=70,Resim="yatak.jpg",Kategori=kategoriler[1], BmUserId=adminID},
                    new Urun{Adi="Yemek Takımı",Stok=70,Resim="yemektakimi.jpg",Kategori=kategoriler[1], BmUserId=adminID },
                    new Urun{Adi="Maskara",Stok=70,Resim="maskara.jpg",Kategori=kategoriler[2], BmUserId=adminID },
                    new Urun{Adi="Diş Fırçası",Stok=70 ,Resim="disfircasi.jpg",Kategori=kategoriler[2], BmUserId=adminID},
                    new Urun{Adi="Parfüm",Stok=70,Resim="parfum.jpg",Kategori=kategoriler[2], BmUserId=adminID },
                };
                context.AddRange(urunler);
#if false // version03 iyelik denemeleri // Bu sadece iyelik // bu deneme geçmiş versiyona ait olmalıydı

                var userManager = services.GetRequiredService<UserManager<BmUser>>();

                var iyelikler = new Iyelik[]
                {
                    new Iyelik{
                        UrunId = context.Uruns.Single(u=>u.Adi=="Parfüm").Id,
                        BmUserId = adminID
                    },
                    new Iyelik{
                        UrunId = context.Uruns.Single(u=>u.Adi=="Televizyon").Id,
                        BmUserId = managerID
                    },
                    new Iyelik{
                        UrunId = context.Uruns.Single(u=>u.Adi=="yatak").Id,
                        BmUserId = uyeID
                    },
                    new Iyelik{
                        UrunId = context.Uruns.Single(u=>u.Adi=="maskara").Id,
                        BmUserId = aliciID
                    }
                };
                context.AddRange(iyelikler);
#endif
#if false // iyelik hiyerarşisi ekleme
                var mamuller = new Mamul[]
                {

                }
#endif
                context.SaveChanges();
            }


        }
    }
}
