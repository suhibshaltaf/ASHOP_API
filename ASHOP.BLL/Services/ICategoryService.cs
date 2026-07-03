using ASHOP.DAL.DTO;
using ASHOP.DAL.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASHOP.BLL.Services
{
    public interface ICategoryService
    {
       Task<  List<CategoryResponse> > GetAllCategories();



     Task <CategoryResponse> CreateCategory(CategoryRequest request);
    }
}
