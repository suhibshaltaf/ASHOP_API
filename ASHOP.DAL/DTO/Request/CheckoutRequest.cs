using ASHOP.DAL.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASHOP.DAL.DTO.Request
{
    public class CheckoutRequest
    {
        public PaymentMethodEnum PaymentMethod { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? PhoneNumber { get; set; }




    }
}
