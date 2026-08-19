using ASHOP.DAL.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ASHOP.PL.Extensions
{
    public  static class AuthenticationExtensions
    {
        public static IServiceCollection AddJWTAuthenticationService(this IServiceCollection Services, IConfiguration Configuration)
        {

         Services
   .AddAuthentication(Options =>
   {
       Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
       Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
   })
   .AddJwtBearer(options =>
   {

       options.TokenValidationParameters = new TokenValidationParameters
       {
           ValidateIssuer = true,
           ValidateAudience = true,
           ValidateLifetime = true,
           ValidateIssuerSigningKey = true,
           ValidIssuer = Configuration["ApiSetting:issuer"],
           ValidAudience = Configuration["ApiSetting:audience"],
           IssuerSigningKey =

           new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["ApiSetting:secretKey"])),
           //  ClockSkew = TimeSpan.Zero  Disable the default 5-minute grace period
       };
   });

           Services.AddAuthorization();
            return Services;
        }
    }
}
