using Eticaret.Authorization;
using Eticaret.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
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

        public static async Task Initialize(IServiceProvider services, string testUserPw)
        {
            using (var context = new UygVTContext(
                services.GetRequiredService<DbContextOptions<UygVTContext>>()))
            {

                var userManager = services.GetService<UserManager<EticaretUser>>();


                var adminID = await UserUretIDsiniGetir(services, testUserPw, "admin@qwerty.com");
                await RolAta(services, adminID, Constants.AdminRole);

                var managerID = await UserUretIDsiniGetir(services, testUserPw, "manager@qwerty.com");
                await RolAta(services, managerID, Constants.ManagerRole);

                var uyeID = await UserUretIDsiniGetir(services, testUserPw, "uye123@qwerty.com");
                await RolAta(services, uyeID, Constants.Uye);

                await SeedAsync(context, services);

            }
        }

        private static async Task<IdentityResult> RolAta(IServiceProvider serviceProvider,
                                                                              string uid, string role)
        {
            IdentityResult IR = null;
            var roleManager = serviceProvider.GetService<RoleManager<EticaretRole>>();

            if (roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            if (await roleManager.FindByNameAsync(role) == null)
            {
                IR = await roleManager.CreateAsync(new EticaretRole(role));
            }

            var userManager = serviceProvider.GetService<UserManager<EticaretUser>>();

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
            var userManager = serviceProvider.GetService<UserManager<EticaretUser>>();

            var user = await userManager.FindByNameAsync(userName);
            if (user == null)
            {
                user = new EticaretUser { UserName = userName };
                await userManager.CreateAsync(user, testUserPw);
            }
#region MyRegion0

            return user.Id;
        }

        public static async Task SeedAsync(UygVTContext context, IServiceProvider services)
        {
            
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
                context.SaveChanges();
	#endregion
#if true //sonra

                var userManager = services.GetRequiredService<UserManager<EticaretUser>>();

                var iyelikler = new Iyelik[]
                {

                    new Iyelik{
                        UrunId = context.Urunler.Single(u=>u.Adi=="Parfüm").Id,
                        EticaretUserId = userManager.Users.Single(u=>u.UserName=="admin@qwerty.com").Id
                    },
                    new Iyelik{
                        UrunId = context.Urunler.Single(u=>u.Adi=="Televizyon").Id,
                        EticaretUserId = userManager.Users.Single(u=>u.UserName=="admin@qwerty.com").Id
                    },
                    new Iyelik{
                        UrunId = context.Urunler.Single(u=>u.Adi=="yatak").Id,
                        EticaretUserId = userManager.Users.Single(u=>u.UserName=="admin@qwerty.com").Id
                    },
                    
                };

                var users = userManager.Users.ToList();
                foreach (var item in users)
                {
                    await userManager.UpdateAsync(item);
                }
                context.AddRange(iyelikler);
#endif

                context.SaveChanges();
            }


        }
    }
}
