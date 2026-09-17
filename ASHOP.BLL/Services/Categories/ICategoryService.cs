using ASHOP.DAL.DTO;
using ASHOP.DAL.DTO.Request;
using ASHOP.DAL.DTO.Response;
using ASHOP.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ASHOP.BLL.Services.Categories
{
    public interface ICategoryService
    {
       Task<Result<List<CategoryResponse>>> GetAllCategories();
     Task<Result<CategoryResponse>> CreateCategory(CategoryRequest request);

        Task<Result<CategoryResponse>> GetCategory(Expression<Func<Category, bool>> filter);

        Task<Result<CategoryResponse>> UpdateCategory(int id, CategoryRequest request);
        Task<Result<bool>> DeleteCategory(int id);

    }   
}
