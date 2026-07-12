using ASHOP.DAL.DTO;
using ASHOP.DAL.DTO.Response;
using ASHOP.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ASHOP.BLL.Services
{
    public interface ICategoryService
    {
       Task<  List<CategoryResponse> > GetAllCategories();
     Task <CategoryResponse> CreateCategory(CategoryRequest request);

        Task<CategoryResponse> GetCategory(Expression<Func<Category, bool>> filter);

        Task<CategoryResponse> UpdateCategory(int id, CategoryRequest request);
        Task<bool> DeleteCategory(int id);

    }
}
