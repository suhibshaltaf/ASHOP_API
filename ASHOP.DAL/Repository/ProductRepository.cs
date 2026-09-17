using ASHOP.DAL.Data;
using ASHOP.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASHOP.DAL.Repository
{
    public class ProductRepository:GenericRepository<Product>,IProductRepository
    {
          public ProductRepository(ApplicationDbContext context) : base(context){}
    }
}
