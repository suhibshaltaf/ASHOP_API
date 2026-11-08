using ASHOP.DAL.DTO;
using ASHOP.DAL.DTO.Request;
using ASHOP.DAL.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASHOP.BLL.Services.Checkout
{
    public interface ICheckoutService
    {
        Task <Result<OrderResponse>> Checkout(CheckoutRequest request, string userId);
        Task<Result<OrderResponse>> HandleSuccess(string sessionId);

    }
}
