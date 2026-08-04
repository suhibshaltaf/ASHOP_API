using ASHOP.BLL.Common;
using ASHOP.DAL.DTO.Request;
using ASHOP.DAL.DTO.Response;
using ASHOP.DAL.Models;
using Mapster;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASHOP.BLL.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        public AuthenticationService(UserManager<ApplicationUser> userManager,IEmailSender emailSender) {
           _userManager = userManager;
            _emailSender = emailSender;
        }

      

        public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
        {
            var user =  request.Adapt<ApplicationUser>();
            var result = await _userManager.CreateAsync(user,request.Password);

            if (!result.Succeeded)
             
                return new RegisterResponse()
                {
                    Message = "Error",
                    Errors=result.Errors.Select(e=>e.Description).ToList()
                };
            var token= await _userManager.GenerateEmailConfirmationTokenAsync(user);
            token=Uri.EscapeDataString(token);

            var emailUrl = $"https://localhost:7282/api/Account/ConfirmEmail?token={token}&UserId={user.Id}";

          await _emailSender.SendEmailAsync(request.Email, "Confirm Email ", "<div><h1>welcome</h1>" +
              $"<a href={emailUrl}> Confirm </a></div>");
            return new RegisterResponse()
            {
                Message = "User registration successful"
            }; 
        }
        public async Task<bool>ConfirmEmail(ConfirmEmailRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if(user is null) return false;
            var result=await _userManager.ConfirmEmailAsync(user, request.Token);
            if (!result.Succeeded)return false;
            return true;
        }
        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync (request.Email);

            if (user is null)
            {
                return new LoginResponse()
                {
                    Message = "Invalid email or password"
                };
            }
            if(!await _userManager.IsEmailConfirmedAsync(user) )
            {
                return new LoginResponse()
                {
                    Message = "Email is not confirmed"
                };
            }
            var result = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!result)
            {
                return new LoginResponse()
                {
                    Message = "Invalid Password"
                };
            }
            return new LoginResponse()
            {
                Message = "Success"
            };
        }
    }
}
