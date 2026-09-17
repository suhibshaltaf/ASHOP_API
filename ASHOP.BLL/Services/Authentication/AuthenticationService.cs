using ASHOP.BLL.Common;
using ASHOP.DAL.DTO;
using ASHOP.DAL.DTO.Request;
using ASHOP.DAL.DTO.Response;
using ASHOP.DAL.Models;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ASHOP.BLL.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _config;

        public AuthenticationService(UserManager<ApplicationUser> userManager,IEmailSender emailSender,IConfiguration configuration) {
           _userManager = userManager;
            _emailSender = emailSender;
           _config = configuration;
        }

      

        public async Task<Result<bool>> RegisterAsync(RegisterRequest request)
        {
           
  var user =  request.Adapt<ApplicationUser>();
            var result = await _userManager.CreateAsync(user,request.Password);
                if (!result.Succeeded)
             
                return new Result<bool>
                {
                    Success = false,
                    Message = "Failed to register user",
                    Errors=result.Errors.Select(e=>e.Description).ToList()
                };

                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                token = Uri.EscapeDataString(token);

                var emailUrl = $"https://localhost:7282/api/Account/ConfirmEmail?token={token}&UserId={user.Id}";

                await _emailSender.SendEmailAsync(request.Email, "Confirm Email ", "<div><h1>welcome</h1>" +
                    $"<a href={emailUrl}> Confirm </a></div>");
                return new Result<bool>
                {
                    Success = true,
                    Message = "User registration successful",
                    Data=true,
                    
                };
           
          

          
         
        }
        public async Task<Result<bool>> ConfirmEmail(ConfirmEmailRequest request)
        {
          
  var user = await _userManager.FindByIdAsync(request.UserId);
                if (user is null) {
                    return new Result<bool>
                    {
                        Success = false,
                        Message = "user not found",
                        Data = false,
                    };

                }

                request.Token = Uri.UnescapeDataString(request.Token);
            var result=await _userManager.ConfirmEmailAsync(user, request.Token);
                return new Result<bool>
                {
                    Success = result.Succeeded,
                    Message = result.Succeeded ? "Email confirmed successfully" : "Failed to confirm email",
                    Data = result.Succeeded,
                };

           

        }
        public async Task<Result<LoginResponse>> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null)
            {
                return new Result<LoginResponse>
                {
                    Success = false,
                    Message = "Invalid email or password",
                    Data = null
                };
            }

            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                return new Result<LoginResponse>
                {
                    Success = false,
                    Message = "Email is not confirmed",
                    Data = null
                };
            }

            var result = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!result)
            {
                return new Result<LoginResponse>
                {
                    Success = false,
                    Message = "Invalid email or password",
                    Data = null
                };
            }

            return new Result<LoginResponse>
            {
                Success = true,
                Message = "Login successful",
                Data = new LoginResponse
                {
                    AccessToken = await Generatejwt(user)
                }
            };
        }
        private async Task<string>Generatejwt(ApplicationUser user)
        {
            var roles=await _userManager.GetRolesAsync(user);
            var UserClaims = new List<Claim>()
           {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role,string.Join(",", roles)),

           };
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["ApiSetting:secretKey"]));

            var creds = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["ApiSetting:issuer"],
                audience: _config["ApiSetting:audience"],
                claims: UserClaims,
                expires: DateTime.Now.AddMonths(20),
                signingCredentials: creds   
            );
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
