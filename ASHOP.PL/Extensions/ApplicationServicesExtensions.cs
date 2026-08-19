using ASHOP.BLL.Common;
using ASHOP.BLL.Services;
using ASHOP.DAL.Data;
using ASHOP.DAL.Repository;
using ASHOP.PL.Utils;
using Microsoft.EntityFrameworkCore;

namespace ASHOP.PL.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection Services)
        {
             Services.AddScoped<ICategoryRepository, CategoryRepository>();
             Services.AddScoped<ICategoryService, CategoryService>();
             Services.AddScoped<IAuthenticationService, AuthenticationService>();
             Services.AddScoped<ISeedData, RoleSeedData>();
             Services.AddTransient<IEmailSender, EmailSender>();
            return Services;
        }
    }
}
