using ASHOP.DAL.DTO;
using ASHOP.DAL.DTO.Request;
using ASHOP.DAL.DTO.Response;
using ASHOP.DAL.Models;
using ASHOP.DAL.Repository;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASHOP.BLL.Services.Cart
{
  
    public class CartService : ICartService 
    {
        private readonly IUnitOfWork _unitOfWork;
        public CartService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IUnitOfWork unitOfWork { get; }

        public async Task<Result<bool>> AddToCart(string userId, CartItemRequest request)
        {
            var product = await _unitOfWork.ProductsRepository.Getone(p => p.Id == request.ProductId);
            if (product is   null)
             return Result<bool>.Fail("Product not found");

           if(request.Count<0)
                return Result<bool>.Fail("Count must be greater than 0");

           if(product.Quantity < request.Count)
                return Result<bool>.Fail("not enough stock available");

            var existigItem = await _unitOfWork.CartItemsRepository.Getone(c => c.UserId == userId && c.ProductId == request.ProductId);
            if(existigItem is not null)
            {
                existigItem.Count += request.Count;
            }
            else
            {
                var NewItem = new CartItem
                {
                    UserId = userId,

                    ProductId = request.ProductId,
                    Count = request.Count

                };         
                await _unitOfWork.CartItemsRepository.CreateAsync(NewItem);

            }
            await _unitOfWork.CompleteAsync();
            return Result<bool>.Ok(true,"Item added to cart successfully");

        }

        public async Task<Result<List<CartItemResponse>>> GetCart(string userId)
        {
            var userCartItems = await _unitOfWork.CartItemsRepository.GetAllAsync(
               filter: c => c.UserId == userId,
               includes:new string[] { nameof(CartItem.Product), $"{nameof(CartItem.Product)}.{nameof(Product.Translations)}" }
               );
            var response = userCartItems.Adapt<List<CartItemResponse>>();

            return Result<List<CartItemResponse>>.Ok(response);
        
       
        }

        public async Task<Result<bool>> RemoveFromCart(string userId, int productId)
        {
            var cartItem = await _unitOfWork.CartItemsRepository.Getone(
                c => c.UserId == userId && c.ProductId == productId
                );
            if (cartItem is null)
                return Result<bool>.Fail("Cart item not found");
            _unitOfWork.CartItemsRepository.Delete(cartItem);
          var affectedRows = await _unitOfWork.CompleteAsync();
            return affectedRows>0 ? Result<bool>.Ok(true, "Item removed from cart successfully") : Result<bool>.Fail("Failed to remove item from cart");
        }


        public async Task<Result<bool>> UpdateQuantity(string userId, int productId, int count)
        {
            if (count < 0)
                return Result<bool>.Fail("Count must be greater than 0");

            var cartItem = await _unitOfWork.CartItemsRepository.Getone(

             filter:    c => c.UserId == userId && c.ProductId == productId,
             includes: new string[] { nameof(CartItem.Product) }
                );
            if (cartItem.Product.Quantity < count)
                return Result<bool>.Fail("not enough stock available");

          cartItem.Count = count;
            await _unitOfWork.CompleteAsync();
            var affectedRows = await _unitOfWork.CompleteAsync();
            return affectedRows > 0 ? Result<bool>.Ok(true, "Item quantity updated successfully") : Result<bool>.Fail("Failed to update item quantity");
        }
    }
}
