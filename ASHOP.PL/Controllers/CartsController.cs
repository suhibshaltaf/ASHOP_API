using ASHOP.BLL.Services.Cart;
using ASHOP.DAL.Data;
using ASHOP.DAL.DTO.Request;
using ASHOP.PL.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Security.Claims;

namespace ASHOP.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartsController : BaseApiController
    {
        private readonly ApplicationDbContext _context;
        private readonly ICartService _cartService;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public CartsController(ApplicationDbContext context, IStringLocalizer<SharedResources> localizer, ICartService cartService)
        {
     _context = context;
           _localizer = localizer;
          _cartService = cartService;
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart( CartItemRequest request)
        {
            
            
            var result = await _cartService.AddToCart(CurrentUserId, request);
           
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("")]
        public async Task<IActionResult> GetCart()
        {
            var result = await _cartService.GetCart(CurrentUserId);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpDelete("{id}")]

        public async Task<IActionResult> RemoveFromCart( [FromRoute] int id)
        {
            var result = await _cartService.RemoveFromCart(CurrentUserId, id);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
