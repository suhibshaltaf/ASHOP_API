using ASHOP.DAL.DTO;
using ASHOP.DAL.DTO.Request;
using ASHOP.DAL.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASHOP.BLL.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<Result<bool>> RegisterAsync(RegisterRequest request);
        Task<Result<LoginResponse>>  LoginAsync(LoginRequest request);
        Task<Result<bool>> ConfirmEmail(ConfirmEmailRequest request);
    }
}
