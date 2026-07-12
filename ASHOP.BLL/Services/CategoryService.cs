using ASHOP.DAL.DTO;
using ASHOP.DAL.DTO.Response;
using ASHOP.DAL.Models;
using ASHOP.DAL.Repository;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

      

        public async Task<CategoryResponse> GetCategory(Expression<Func<Category, bool>> filter)
        {
            var category = await _CategoryRepository.Getone(filter, new string[] { nameof(Category.Translations) });
            return category.Adapt<CategoryResponse>();
        }

        async Task<CategoryResponse> ICategoryService.CreateCategory(CategoryRequest request)
        {
            var category = request.Adapt<Category>();
          await  _CategoryRepository.CreateAsync(category);
            return category.Adapt<CategoryResponse>();
        }

        async Task<List<CategoryResponse>> ICategoryService.GetAllCategories()
        {
            var categories = await _CategoryRepository.GetAllAsync(
                new string[] { nameof(Category.Translations) });
            return categories.Adapt<List<CategoryResponse>>();
           
        }
        public async Task<bool> DeleteCategory(int id)
        {
            var category = await _CategoryRepository.Getone(c => c.Id == id);
            if (category == null) {
                return false;
            }
            return await _CategoryRepository.DeleteAsync(category);
        }

        public async Task<CategoryResponse> UpdateCategory(int id, CategoryRequest request)
        {
            var category = await _CategoryRepository.Getone(

        c => c.Id == id,new string[] { nameof(Category.Translations) }
    );

            if (category == null)
                return null;

            foreach (var item in request.Translations)
            {
                var translation = category.Translations
                    .FirstOrDefault(t => t.Language == item.Language);

                if (translation != null)
                {
                    translation.Name = item.Name;
                }
            }

            await _CategoryRepository.UpdateAsync(category);

            return category.Adapt<CategoryResponse>();
        }
    }
}
