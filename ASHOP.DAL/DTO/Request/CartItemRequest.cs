using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASHOP.DAL.DTO.Request
{
    public class CartItemRequest
    {
        public int ProductId { get; set; }

        public int Count { get; set; }

    }
}
