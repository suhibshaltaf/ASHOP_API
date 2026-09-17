using ASHOP.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASHOP.DAL.DTO.Response
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }

        public int Quantity { get; set; }

        public double Rate { get; set; }
        public string MainImage { get; set; }

        public int CategoryId { get; set; }
public string Name { get; set; }
        public string Description { get; set; }
    } 
}
