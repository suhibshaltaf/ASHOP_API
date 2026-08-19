    

using ASHOP.BLL.Common;
using ASHOP.BLL.Mapping;
using ASHOP.BLL.Services;
using ASHOP.DAL.Data;
using ASHOP.DAL.Models;
using ASHOP.DAL.Repository;
using ASHOP.PL.Extensions;
using ASHOP.PL.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Globalization;
using System.Text;

namespace ASHOP.PL
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.addpplicationService(builder.Configuration);
       

            var app = builder.Build();
            app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

             MapsterConfig.MapsterConfigRegister();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            await app.SeedDataAsync();


            app.MapControllers();

            app.Run();
        }
    }
}
