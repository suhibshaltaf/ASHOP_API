using ASHOP.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASHOP.DAL.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private ICategoryRepository? _CategoriesRepository;

        private IProductRepository? _ProductsRepository;

        public ICategoryRepository CategoriesRepository { get{
            if(_CategoriesRepository is null)
                {
                    _CategoriesRepository = new CategoryRepository(_context);
                }
                return _CategoriesRepository;
            } }

        public IProductRepository ProductsRepository {
            get
            {
                if (_ProductsRepository is null)
                {
                    _ProductsRepository = new ProductRepository(_context);
                }
                return _ProductsRepository;
            }
        }
        public UnitOfWork(ApplicationDbContext context)
        {
           _context = context;
     
        }
    

        public async Task<int> CompleteAsync()
        {
          return await _context.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
await _context.DisposeAsync();
        }
    }
}
