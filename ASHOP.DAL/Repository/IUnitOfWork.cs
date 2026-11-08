using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASHOP.DAL.Repository
{
    public interface IUnitOfWork :IAsyncDisposable
    {
        ICategoryRepository CategoriesRepository { get; }
        IProductRepository ProductsRepository { get; }
        ICartRepository CartItemsRepository { get; }

        IOrderRepository orderRepository { get; }

        Task<int> CompleteAsync();
    }
}
