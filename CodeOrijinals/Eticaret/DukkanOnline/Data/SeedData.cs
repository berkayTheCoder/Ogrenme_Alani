using DukkanOnline.Authorization;
using DukkanOnline.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DukkanOnline.Data
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, string testUserPw )
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // For sample purposes seed both with the same password.
                // Password is set with the following:
                // dotnet user-secrets set SeedUserPW <pw>
                // The admin user can do anything

                var adminID = await EnsureUser(serviceProvider, testUserPw, "admin@contoso.com");
                await EnsureRole(serviceProvider, adminID, Constants.ContactAdministratorsRole);

                // allowed user can create and edit contacts that they create
                var managerID = await EnsureUser(serviceProvider, testUserPw, "manager@contoso.com");
                await EnsureRole(serviceProvider, managerID, Constants.ContactManagersRole);

                var toptanciID = await EnsureToptanci(serviceProvider, testUserPw, "toptanci@contoso.com");
                await EnsureRole(serviceProvider, toptanciID, Constants.UrunMangerRole);

                var saticiID = await EnsureSatici(serviceProvider, testUserPw, "satici@contoso.com");
                await EnsureRole(serviceProvider, saticiID, Constants.UrunMangerRole);

                var BakiciID = await Bakici(serviceProvider, testUserPw, "bakici@contoso.com");
                await EnsureRole(serviceProvider, BakiciID, Constants.SepetManagersRole);

                var AliciID = await Alici(serviceProvider, testUserPw, "alici@contoso.com");
                await EnsureRole(serviceProvider, AliciID, Constants.SatinAlmaManagersRole);

                var YorumcuID = await Yorumcu(serviceProvider, testUserPw, "Yorumcu@contoso.com");
                await EnsureRole(serviceProvider, YorumcuID, Constants.YorumManagersRole);


                SeedDB(context, adminID);
            }
        }
        /*
        private static Task<string> UserIDBul(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
            var user = await userManager.GetUserId(User);
            if (user == null)
            {
                user = new Toptanci { UserName = UserName };
                await userManager.update(user, testUserPw);
            }
            return user.Id;
        }
        */

        private static async Task<string> EnsureUser(IServiceProvider serviceProvider,
                                                    string testUserPw, string UserName)
        {
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                user = new ApplicationUser { UserName = UserName };
                await userManager.CreateAsync(user, testUserPw);
            }

            return user.Id;
        }
        private static async Task<string> EnsureToptanci(IServiceProvider serviceProvider,
                                                    string testUserPw, string UserName)
        {
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                user = new Toptanci { UserName = UserName };
                await userManager.CreateAsync(user, testUserPw);
            }

            return user.Id;
        }
        
        private static async Task<string> EnsureSatici(IServiceProvider serviceProvider,
                                                    string testUserPw, string UserName)
        {

            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                user = new Satici { UserName = UserName };
                await userManager.CreateAsync(user, testUserPw);
            }

            return user.Id;
        }
        private static async Task<string> Bakici(IServiceProvider serviceProvider,
                                                    string testUserPw, string UserName)
        {
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                user = new Bakici { UserName = UserName };
                await userManager.CreateAsync(user, testUserPw);
            }

            return user.Id;
        }
        private static async Task<string> Alici(IServiceProvider serviceProvider,
                                                    string testUserPw, string UserName)
        {
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                user = new Alici { UserName = UserName };
                await userManager.CreateAsync(user, testUserPw);
            }

            return user.Id;
        }
        private static async Task<string> Yorumcu(IServiceProvider serviceProvider,
                                                    string testUserPw, string UserName)
        {
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                user = new Yorumcu { UserName = UserName };
                await userManager.CreateAsync(user, testUserPw);
            }

            return user.Id;
        }


        private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider,
                                                                      string uid, string role)
        {
            IdentityResult IR = null;
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
            //serviceProvider la RoleManager dan IdentityRole için ServisAlıyoruz.

            if (roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            if (!await roleManager.RoleExistsAsync(role))
            {
                IR = await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByIdAsync(uid);

            if (user == null)
            {
                throw new Exception("The testUserPw password was probably not strong enough!");
            }

            IR = await userManager.AddToRoleAsync(user, role);

            return IR;
        }
        private static async Task<IdentityResult> RolAta(IServiceProvider serviceProvider,
                                                                      string uid, string role)
        {
            IdentityResult IR = null;
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
            //serviceProvider la RoleManager dan IdentityRole için ServisAlıyoruz.

            if (roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            if (!await roleManager.RoleExistsAsync(role))
            {
                IR = await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

            var user = await userManager.FindByIdAsync(uid);

            if (user == null)
            {
                throw new Exception("The testUserPw password was probably not strong enough!");
            }

            IR = await userManager.AddToRoleAsync(user, role);

            return IR;
        }
        public static void SeedDB(ApplicationDbContext context, string adminID)
        {
            if (context.Uruns.Any())
            {
                return;   // DB has been seeded
            }

            context.Contacts.AddRange(
                new Contact
                {
                    Name = "Şükrü Berkay",
                    Address = "538 Gökevler 4A/110",
                    Town = "Esenyurt",
                    City = "İstanbul",
                    State = "Avrupa",
                    Zip = "10999",
                    Email = "berkay@example.com",
                    Status = ContactStatus.Onaylı,
                    OwnerID = adminID
                }
            );
            context.SaveChanges();

            context.Kategoris.AddRange(
                new Kategori
                {
                    Name = "Outdoor",
                    
                }
            );
            context.SaveChanges();

            //context.Toptancis.AddRange(
            //    new Toptanci
            //    {
            //        UserName = "admin@contoso.com",
                    
            //    }
            //);
            //context.SaveChanges();

            context.Uruns.AddRange(
                new Urun
                {
                    Name = "Çadır",
                    KategoriId = context.Kategoris.Single(k => k.Name == "Outdoor").Id,
                    ToptanciId = context.Toptancis.Single(t=>t.UserName== "toptanci@contoso.com").Id,
                    Stok = 100,
                    Price = 10,
                }
            );
            context.SaveChanges();
        }
    }


}
