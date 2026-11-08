using ASHOP.BLL.Services.File;
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
using System.Text;
using System.Threading.Tasks;

namespace ASHOP.BLL.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork  _unitOfWork;
        private readonly IFileService _fileService;

        public ProductService(IUnitOfWork  unitOfWork,IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _fileService = fileService;
        }
    

        public async Task<Result<ProductResponse>> CreateProduct(ProductRequest request)
        {
         
                if (request.MainImage is null)
                {
                    return Result<ProductResponse>.Fail("Main image is required.");
                   
                }
                var uploadResult = await _fileService.UploadFile(request.MainImage);
                if (!uploadResult.Success)
                {
                    return Result<ProductResponse>.Fail(uploadResult.Message);
                }


                var product = request.Adapt<Product>();
                product.MainImage = uploadResult.Data.Url;
            product.MainImagePublicId = uploadResult.Data.PublicId;
            await _unitOfWork.ProductsRepository.CreateAsync(product);
                await _unitOfWork.CompleteAsync();

                return Result<ProductResponse>.Ok(product.Adapt<ProductResponse>());
                
           
        }

     

        public async Task<Result<List<ProductResponse>>>GetAllProducts()
        { var products =await  _unitOfWork.ProductsRepository.GetAllAsync(
           includes: new string[] { nameof(Product.Translations), nameof(Product.category) });

                return new Result<List<ProductResponse>>
                {
                    Success = true,
                    Message = "success",
                    Data = products.Adapt<List<ProductResponse>>()
                };

           
        }

        public async Task<Result<ProductResponse>> GetProduct(Expression<Func<Product, bool>> filter)
        {
          
                var product = await _unitOfWork.ProductsRepository.Getone(filter, new string[] { nameof(Product.Translations), nameof(Product.category) });
                if (product is null ) {
                    return new Result<ProductResponse>
                    {
                        Success = false,
                        Message = "Product not found."
                    };
                }
                return new Result<ProductResponse>
                {
                    Success = true,
                    Message = "success",
                    Data = product.Adapt<ProductResponse>()
                };

        }

        public async Task<Result<bool>> DeleteProduct(int id)
        {
            var product =await  _unitOfWork.ProductsRepository.Getone(p => p.Id == id);
            if(product is null)
            
                return  Result<bool>.Fail("Product not found.");
            
            var deleteImageResult = await _fileService.Delete(product.MainImagePublicId);
            if(!deleteImageResult.Success)
            
                return Result<bool>.Fail(deleteImageResult.Message);
            _unitOfWork.ProductsRepository.Delete(product);
            var affectedRows = await _unitOfWork.CompleteAsync();
            return affectedRows > 0 ? Result<bool>.Ok(true, "Product deleted successfully.") : Result<bool>.Fail("Failed to delete product.");
        }
    }
}
