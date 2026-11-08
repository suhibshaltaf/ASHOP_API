using ASHOP.BLL.Services.Checkout;
using ASHOP.DAL.DTO.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASHOP.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : BaseApiController
    {
        private readonly ICheckoutService _checkoutService;

        public OrdersController(ICheckoutService checkoutService)
        {
            _checkoutService = checkoutService;
        }
        [HttpPost("checkout")]
        public async Task<IActionResult> CheckOut( [FromBody] CheckoutRequest request)
        {
            var result= await _checkoutService.Checkout(request, CurrentUserId);
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }
        [HttpGet("payment/success")]
        [AllowAnonymous]
        public async Task<IActionResult> Payment(string sessionId)
        {
            var result = await _checkoutService.HandleSuccess(sessionId);
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }
        //[HttpGet("payment/cancel")]

        //public async Task<IActionResult> Cancel(string sessionId)
        //{

        //}
    } }
