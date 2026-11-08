using ASHOP.BLL.Common;
using ASHOP.BLL.Services.Authentication;
using ASHOP.BLL.Services.Cart;
using ASHOP.BLL.Services.Categories;
using ASHOP.BLL.Services.Checkout;
using ASHOP.BLL.Services.File;
using ASHOP.BLL.Services.Products;
using ASHOP.DAL.Data;
using ASHOP.DAL.Repository;
using ASHOP.PL.Utils;
using Microsoft.EntityFrameworkCore;
using Stripe;

namespace ASHOP.PL.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddBusinessServices    (this IServiceCollection Services,IConfiguration configuration)
        {

            Services.AddScoped<IUnitOfWork, UnitOfWork>();
            Services.AddExceptionHandler<GlobalExceptionHandler>();
            /*Repository*/

                                  /*Service*/

             Services.AddScoped<ICategoryService, CategoryService>();
            Services.AddScoped<IProductService, BLL.Services.Products.ProductService>();
             Services.AddScoped<IFileService, BLL.Services.File.FileService>();
            Services.AddScoped<ICartService, CartService>();
            Services.AddScoped<ICheckoutService, BLL.Services.Checkout.CheckoutService>();

            Services.AddScoped<IAuthenticationService, AuthenticationService>();
             Services.AddScoped<ISeedData, RoleSeedData>();
             Services.AddTransient<IEmailSender, EmailSender>();

            Services.Configure<StripeSetting>(configuration.GetSection("Stripe"));

            StripeConfiguration.ApiKey = configuration["Stripe:Secretkey"];
            Services.AddProblemDetails();
            return Services;
        }
    }
}
