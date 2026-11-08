using ASHOP.DAL.Enum;
using ASHOP.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASHOP.DAL.DTO.Response
{
    public class OrderResponse
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
         public PaymentMethodEnum PaymentMethod { get; set; }

        public PaymentStatusEnum PaymentStatus { get; set; }
        public decimal? TotalAmount { get; set; }
        public OrderStatusEnum Status { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PhoneNumber { get; set; }

        public List<OrderItemResponse> OrderItems { get; set; }


        public string? PaymentUrl { get; set; }
    }
}
