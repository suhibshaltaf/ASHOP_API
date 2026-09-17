using ASHOP.BLL.Services.Authentication;
using ASHOP.DAL.DTO.Request;
using Microsoft.AspNetCore.Mvc;

namespace ASHOP.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AccountController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest  request)
        {
            var result = await _authenticationService.RegisterAsync(request);
            return Ok(result);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _authenticationService.LoginAsync(request);
            return Ok(result);
        }
        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailRequest request)
        {
            var result = await _authenticationService.ConfirmEmail(request);

            if (!result.Data)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
