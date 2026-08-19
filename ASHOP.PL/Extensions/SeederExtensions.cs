using ASHOP.PL.Utils;

namespace ASHOP.PL.Extensions
{
    public static class SeederExtensions
    {
        public static async Task SeedDataAsync( this WebApplication app )
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var seeders = services.GetServices<ISeedData>();
                foreach (var seeder in seeders)
                {
                    await seeder.DataSeed();
                }
            }
        }
    }
}
