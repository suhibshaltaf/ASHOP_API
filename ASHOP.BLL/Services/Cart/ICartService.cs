using ASHOP.DAL.DTO;
using ASHOP.DAL.DTO.Request;
using ASHOP.DAL.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASHOP.BLL.Services.Cart
{
    public interface ICartService
    {
        Task <Result<bool>> AddToCart(string userId,CartItemRequest request);
        Task<Result<List<CartItemResponse>>> GetCart (string userId);
        Task<Result<bool>> UpdateQuantity(string userId, int productId, int count);

        Task<Result<bool>> RemoveFromCart(string userId, int productId);


    }
}
