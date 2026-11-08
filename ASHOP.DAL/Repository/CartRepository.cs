using ASHOP.DAL.Data;
using ASHOP.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASHOP.DAL.Repository
{
    public class CartRepository : GenericRepository<CartItem>, ICartRepository
    {
        public CartRepository(ApplicationDbContext context) : base(context) { }
    }
}
