using ASHOP.DAL.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASHOP.DAL.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public OrderStatusEnum Status { get; set; } = OrderStatusEnum.pending;
        public PaymentMethodEnum PaymentMethod { get; set; } 

        public PaymentStatusEnum PaymentStatus { get; set; } = PaymentStatusEnum.Pending;
        public DateTime? ShippedDate { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal? TotalAmount { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
         public string PhoneNumber { get; set; }

        public List<OrderItem> OrderItems { get; set; } 

        public string? StripeSeesioId { get; set; }

        public string? PaymentIntentId { get; set; }

        public DateTime? paidAT { get; set; }




    }
}
