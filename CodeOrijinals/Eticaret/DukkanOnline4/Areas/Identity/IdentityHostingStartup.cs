using System;
using DukkanOnline4.Areas.Identity.Data;
using DukkanOnline4.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(DukkanOnline4.Areas.Identity.IdentityHostingStartup))]
namespace DukkanOnline4.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<DukkanOnline4Context>(options =>
                    options.UseSqlite(
                        context.Configuration.GetConnectionString("DukkanOnline4ContextConnection")));

                services.AddDefaultIdentity<DukkanOnline4User>()
                    .AddEntityFrameworkStores<DukkanOnline4Context>();
            });
        }
    }
}