using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASHOP.DAL.DTO.Response
{
   public class CartItemResponse
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
        public string MainImage { get; set; }
        public decimal TotalPrice => Price * Count;

    }
}
