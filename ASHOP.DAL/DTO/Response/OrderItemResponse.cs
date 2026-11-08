using ASHOP.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASHOP.DAL.DTO.Response
{
    public class OrderItemResponse
    {
      
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Count { get; set; }
        public decimal PriceAtPurchase { get; set; }

        public decimal TotalPrice => PriceAtPurchase * Count;
    }
}
