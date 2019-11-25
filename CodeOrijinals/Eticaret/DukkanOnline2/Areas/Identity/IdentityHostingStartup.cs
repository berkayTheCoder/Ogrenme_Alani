using System;
using DukkanOnline2.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(DukkanOnline2.Areas.Identity.IdentityHostingStartup))]
namespace DukkanOnline2.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<BakiciContext>(options =>
                    options.UseSqlite(
                        context.Configuration.GetConnectionString("BakiciContextConnection")));

                services.AddDefaultIdentity<Bakici>()
                    .AddEntityFrameworkStores<BakiciContext>();
            });
        }
    }
}