using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ASHOP.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        protected string CurrentUserId => User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }
}
 