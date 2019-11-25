using System;
using Eticaret.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Eticaret.Areas.Identity.IdentityHostingStartup))]
namespace Eticaret.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<KimlikVtContext>(options =>
                    options.UseSqlite(
                        context.Configuration.GetConnectionString("KimlikVtContextConnection")));

                services.AddDefaultIdentity<EticaretUser>().AddRoles<EticaretRole>()
                    .AddEntityFrameworkStores<KimlikVtContext>();
            });
        }
    }
}