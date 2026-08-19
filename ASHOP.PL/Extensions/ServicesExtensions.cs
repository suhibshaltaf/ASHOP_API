using ASHOP.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace ASHOP.PL.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection addpplicationService(this IServiceCollection Services, IConfiguration Configuration)
        {

            Services.AddControllers();
            Services.AddOpenApi();
            Services.AddDatabaseService(Configuration);


            Services.AddIdentityService();

            Services.AddJWTAuthenticationService(Configuration);
            Services.AddLocalizationService();

            Services.AddApplicationService();
            return Services;
        }
        }
}
