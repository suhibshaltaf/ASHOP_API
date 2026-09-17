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

namespace ASHOP.BLL.Services.Products
{
    public interface IProductService
    {
        Task<Result<ProductResponse>> CreateProduct(ProductRequest request);

        Task<Result<List<ProductResponse>>> GetAllProducts();
        Task<Result<ProductResponse>> GetProduct(Expression<Func<Product, bool>> filter);

        Task<Result<bool>> DeleteProduct(int id);




    }
}
