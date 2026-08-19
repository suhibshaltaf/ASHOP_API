using ASHOP.DAL.Data;
using ASHOP.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ASHOP.PL.Extensions
{
    public  static class IdentityExtensions
    {
        public static IServiceCollection AddIdentityService(this IServiceCollection Services)
        {
            Services.AddIdentity<ApplicationUser, IdentityRole>(
                Options =>
                {
                    Options.User.RequireUniqueEmail = true;

                }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
            return Services;
        }
    }
}
