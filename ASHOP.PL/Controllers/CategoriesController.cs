using ASHOP.DAL.Data;
using ASHOP.DAL.DTO;
using ASHOP.DAL.DTO.Response;
using ASHOP.DAL.Models;
using ASHOP.PL.Resources;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using ASHOP.DAL.Repository;
using ASHOP.BLL.Services;

namespace ASHOP.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly ICategoryService _categoryService;

        public CategoriesController(ApplicationDbContext context, IStringLocalizer<SharedResources> localizer,ICategoryService categoryService)
        {
            _context = context;
            _localizer = localizer;
            _categoryService = categoryService;
        }
        [HttpGet("")]
        public async Task<IActionResult> Index()

        {
            var categories = await _categoryService.GetAllCategories();
            return Ok(new { _localizer["Success"].Value, categories });
        }
        [HttpPost("")]
        public async Task<IActionResult> Create(CategoryRequest request)
        {
            var resonse = await _categoryService.CreateCategory(request);


            return Ok();    
        }
    }
}

