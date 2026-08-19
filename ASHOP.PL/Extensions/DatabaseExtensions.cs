using ASHOP.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace ASHOP.PL.Extensions
{
    public static class DatabaseExtensions
    {
        public static  IServiceCollection AddDatabaseService(this IServiceCollection Services,IConfiguration Configuration)
        {
            Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            return Services;
        } 

    }

}
