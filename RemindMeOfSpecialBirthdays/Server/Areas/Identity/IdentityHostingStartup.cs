using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RemindMeOfSpecialBirthdays.Server.Data;
using RemindMeOfSpecialBirthdays.Server.Models;

[assembly: HostingStartup(typeof(RemindMeOfSpecialBirthdays.Server.Areas.Identity.IdentityHostingStartup))]
namespace RemindMeOfSpecialBirthdays.Server.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}