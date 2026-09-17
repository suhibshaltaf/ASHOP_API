using ASHOP.BLL.Common;
using ASHOP.BLL.Services.Authentication;
using ASHOP.BLL.Services.Categories;
using ASHOP.BLL.Services.File;
using ASHOP.BLL.Services.Products;
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

            Services.AddScoped<IUnitOfWork, UnitOfWork>();
            Services.AddExceptionHandler<GlobalExceptionHandler>();
            /*Repository*/

                                  /*Service*/

             Services.AddScoped<ICategoryService, CategoryService>();
            Services.AddScoped<IProductService, ProductService>();
             Services.AddScoped<IFileService, FileService>();

                Services.AddScoped<IAuthenticationService, AuthenticationService>();
             Services.AddScoped<ISeedData, RoleSeedData>();
             Services.AddTransient<IEmailSender, EmailSender>();

            Services.AddProblemDetails();
            return Services;
        }
    }
}
