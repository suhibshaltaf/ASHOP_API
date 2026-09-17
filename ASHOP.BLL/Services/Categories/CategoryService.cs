using ASHOP.DAL.DTO;
using ASHOP.DAL.DTO.Request;
using ASHOP.DAL.DTO.Response;
using ASHOP.DAL.Models;
using ASHOP.DAL.Repository;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ASHOP.BLL.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService( IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<CategoryResponse>>> GetAllCategories()
        {
         
                var categories = await _unitOfWork.CategoriesRepository.GetAllAsync(
                    new string[]
                    {
                        nameof(Category.Translations),
                        "CreatedBy"
                    });

                return new Result<List<CategoryResponse>>
                {
                    Success = true,
                    Message = "Categories retrieved successfully.",
                    Data = categories.Adapt<List<CategoryResponse>>()
                };
           
        }

        public async Task<Result<CategoryResponse>> GetCategory(
            Expression<Func<Category, bool>> filter)
        {
                var category = await _unitOfWork.CategoriesRepository.Getone(
                    filter,
                    new string[]
                    {
                        nameof(Category.Translations)
                    });

                if (category is null)
                {
                    return new Result<CategoryResponse>
                    {
                        Success = false,
                        Message = "Category not found."
                    };
                }

                return new Result<CategoryResponse>
                {
                    Success = true,
                    Message = "Category retrieved successfully.",
                    Data = category.Adapt<CategoryResponse>()
                };
          
        }

        public async Task<Result<CategoryResponse>> CreateCategory(
            CategoryRequest request)
        {
          
                var category = request.Adapt<Category>();

            await _unitOfWork.CategoriesRepository.CreateAsync(category);
            await _unitOfWork.CompleteAsync();


            return new Result<CategoryResponse>
                {
                    Success = true,
                    Message = "Category created successfully.",
                };
            
        }

        public async Task<Result<bool>> DeleteCategory(int id)
        {
           var category = await _unitOfWork.CategoriesRepository.Getone(
                    c => c.Id == id);

                if (category is null)
                {
                    return new Result<bool>
                    {
                        Success = false,
                        Message = "Category not found.",
                        Data = false
                    };
                }

            _unitOfWork.CategoriesRepository.Delete(category);
             var affectedRows = await _unitOfWork.CompleteAsync();
            return new Result<bool>
                {
                    Success = true,
                    Message = affectedRows > 0 ? "Category deleted successfully." : "Failed to delete category.",
                    Data = affectedRows > 0
                };
           
        }

        public async Task<Result<CategoryResponse>> UpdateCategory(
            int id,
            CategoryRequest request)
        {
          
                var category = await _unitOfWork.CategoriesRepository.Getone(
                    c => c.Id == id,
                    new string[]
                    {
                        nameof(Category.Translations)
                    });

                if (category is null)
                {
                    return new Result<CategoryResponse>
                    {
                        Success = false,
                        Message = "Category not found."
                    };
                }

                foreach (var item in request.Translations)
                {
                    var translation = category.Translations
                        .FirstOrDefault(t => t.Language == item.Language);

                    if (translation is not null)
                    {
                        translation.Name = item.Name;
                    }
                }

                var updatedCategory = await _unitOfWork.CategoriesRepository.UpdateAsync(category);

            await _unitOfWork.CompleteAsync();
            return new Result<CategoryResponse>
                {
                    Success = true,
                    Message = "Category updated successfully.",
                    Data = updatedCategory.Adapt<CategoryResponse>()
                };
            
        }
    }
}