using ASHOP.DAL.DTO;
using ASHOP.DAL.DTO.Response;
using ASHOP.DAL.Models;
using ASHOP.DAL.Repository;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASHOP.BLL.Services
{
    public class CategoryService : ICategoryService
    {        private readonly ICategoryRepository _CategoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _CategoryRepository = categoryRepository;
        }



        async Task<CategoryResponse> ICategoryService.CreateCategory(CategoryRequest request)
        {
            var category = request.Adapt<Category>();
          await  _CategoryRepository.CreateAsync(category);
            return category.Adapt<CategoryResponse>();
        }

        async Task<List<CategoryResponse>> ICategoryService.GetAllCategories()
        {
            var categories = await _CategoryRepository.GetAllAsync();
            return categories.Adapt<List<CategoryResponse>>();
           
        }
    }
}
