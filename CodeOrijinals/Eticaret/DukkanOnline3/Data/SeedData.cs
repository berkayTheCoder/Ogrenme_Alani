using DukkanOnline3.Authorization;
using DukkanOnline3.Data;
using DukkanOnline3.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DukkanOnline3.Controllers
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, string testUserPw)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // For sample purposes seed both with the same password.
                // Password is set with the following:
                // dotnet user-secrets set SeedUserPW <pw>
                // The admin user can do anything

                var adminID = await EnsureUser(serviceProvider, testUserPw, "admin@contoso.com");

                // allowed user can create and edit contacts that they create
                var managerID = await EnsureUser(serviceProvider, testUserPw, "manager@contoso.com");
                await EnsureRole(serviceProvider, managerID, Constants.Uye);
                await EnsureRole(serviceProvider, managerID, Constants.Uretici);
                await EnsureRole(serviceProvider, managerID, Constants.Toptanci);
                await EnsureRole(serviceProvider, managerID, Constants.Perakendeci);
                await EnsureRole(serviceProvider, managerID, Constants.Bakici);
                await EnsureRole(serviceProvider, managerID, Constants.Musteri);
                await EnsureRole(serviceProvider, managerID, Constants.Alici);
                await EnsureRole(serviceProvider, managerID, Constants.Yorumcu);

                SeedDB(context, adminID);
            }
        }

        public static void SeedDB(ApplicationDbContext context, string adminID)
        {
            if (context.Contacts.Any())
            {
                return;
            }
            var kategoriler = new Kategori[]
            {
                new Kategori { Name = "Anne/Bebek" },
                new Kategori { Name = "Elektronik" },
                new Kategori { Name = "Ev/Yaşam" },
                new Kategori { Name = "Giyim",  }

            };
            context.Kategoris.AddRange(kategoriler);
            context.SaveChanges();
            //context.AltKategoris.AddRange(
            //    new AltKategori { Name = "Er", Id = context.Kategoris.Single(k => k.Name == "Giyim").Id },
            //    new AltKategori { Name = "Kadın" },
            //    new AltKategori { Name = "Çocuk" }
            //    );
            //context.AltKategoris.AddRange(
            //    new AltKategori { Name = "Erkek", Id = context.AltKategoris.Single(k => k.Name == "Çocuk").Id },
            //    new AltKategori { Name = "Kız", Id = context.AltKategoris.Single(k => k.Name == "Çocuk").Id }
            //    );
            context.Uruns.AddRange(
                new Urun {
                    Name = "Biberon", KategoriId = context.Kategoris.Single(k => k.Name == "Anne/Bebek").Id,
                    Maliyeti = 10 , Stok = 100
                },
                new Urun
                {
                    Name = "Bez",
                    KategoriId = context.Kategoris.Single(k => k.Name == "Anne/Bebek").Id,
                    Maliyeti = 10,
                    Stok = 100
                },
                new Urun
                {
                    Name = "Pantolon",
                    KategoriId = context.Kategoris.Single(k => k.Name == "Giyim").Id,
                    Maliyeti = 10,
                    Stok = 100
                }
                );
            //context.Mamuls.AddRange(
            //    new Mamul
            //    {
            //        Name = "Biberon",
            //        KategoriId = kategoriler.Single(k => k.Name == "Anne/Bebek").Id,
            //        Maliyeti = 10,
            //        Stok = 100,
                    
            //    },
            //    new Mamul
            //    {
            //        Name = "Bez",
            //        KategoriId = kategoriler.Single(k => k.Name == "Anne/Bebek").Id,
            //        Maliyeti = 10,
            //        Stok = 100
            //    },
            //    new Mamul
            //    {
            //        Name = "Pantolon",
            //        KategoriId = kategoriler.Single(k => k.Name == "Giyim").Id,
            //        Maliyeti = 10,
            //        Stok = 100,
            //    }
                //);
            //context.MyIyeliks.AddRange(
            //    new MyIyelik { MyUrunIyelikTalepId },
            //    new MyIyelik { Name = "Perakende" },
            //    new MyIyelik { Name = "Mamul" }
            //    );
            context.SaveChanges();
            //context.MyUrunIyelikTaleps.AddRange(
            //    new MyUrunIyelikTalep
            //    {
            //        IyelikId = context.MyIyeliks.Single(k => k.Name == "Toptan").Id,
            //        UrunId = context.MyUruns.Single(k => k.Name == "Biberon").Id
            //    },
            //    new MyUrunIyelikTalep
            //    {
            //        IyelikId = context.MyIyeliks.Single(k => k.Name == "Toptan").Id,
            //        UrunId = context.MyUruns.Single(k => k.Name == "Bez").Id
            //    },
            //    new MyUrunIyelikTalep
            //    {
            //        IyelikId = context.MyIyeliks.Single(k => k.Name == "Toptan").Id,
            //        UrunId = context.MyUruns.Single(k => k.Name == "Pantolon").Id
            //    }
            //    );
            //context.SaveChanges();
            //context.MyUserIyelikTaleps.AddRange(
            //    new MyUrunTalep { UrunId = context.MyUruns.Single(k => k.Name == "Biberon").Id },
            //    new MyUrunTalep { UrunId = context.MyUruns.Single(k => k.Name == "Bez").Id },
            //    new MyUrunTalep { UrunId = context.MyUruns.Single(k => k.Name == "Pantolon").Id }
            //    );
            //context.SaveChanges();

            //context.MyIyeliksTaleps.AddRange(
            //    new MyIyelikTalep { IyelikId = context.MyIyeliks.Single(k => k.Name == "Toptan").Id },
            //    new MyIyelikTalep { IyelikId = context.MyIyeliks.Single(k => k.Name == "Perakende").Id },
            //    new MyIyelikTalep { IyelikId = context.MyIyeliks.Single(k => k.Name == "Mamul").Id }
            //    );
            //context.Contacts.AddRange();
            //context.SaveChanges();
        }

        private static async Task<string> EnsureUser(IServiceProvider serviceProvider,
                                                    string testUserPw, string userName)
        {
            var userManager = serviceProvider.GetService<UserManager<MyUser>>();

            var user = await userManager.FindByNameAsync(userName);
            if (user == null)
            {
                user = new MyUser { UserName = userName };
                await userManager.CreateAsync(user, testUserPw);
            }

            return user.Id;
        }

        private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider,
                                                                      string uid, string role)
        {
            IdentityResult IR = null;
            var roleManager = serviceProvider.GetService<RoleManager<MyRole>>();

            if (roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            if (await roleManager.FindByNameAsync(role) == null)
            {
                IR = await roleManager.CreateAsync(new MyRole(role));
            }

            var userManager = serviceProvider.GetService<UserManager<MyUser>>();

            var user = await userManager.FindByIdAsync(uid);

            if (user == null)
            {
                throw new Exception("The testUserPw password was probably not strong enough!");
            }

            IR = await userManager.AddToRoleAsync(user, role);

            return IR;
        }
        
    }
}