using ASHOP.BLL.Services.Products;
using ASHOP.DAL.DTO.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASHOP.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateProduct([FromForm] ProductRequest request)
        {
            var result = await _productService.CreateProduct(request);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);

        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await _productService.GetAllProducts();
            return result.Success ? Ok(result) : BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct([FromRoute] int id)
        {
            var result = await _productService.GetProduct(p => p.Id == id);
            return result.Success ? Ok(result) : BadRequest();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteProduct( int id)
        {
            var result = await _productService.DeleteProduct(id);
            return result.Success ? Ok(result) : BadRequest();
        }
    }
}
