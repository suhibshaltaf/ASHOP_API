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
    public class CategoryRepository: ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Category> CreateAsync(Category category)
        {
     await  _context.AddAsync(category);
          await    _context.SaveChangesAsync(); 
            return category;
        }

        public Task<List<Category>> GetAllAsync()
        {
            var categories = _context.Categories.Include(c => c.Translations).ToListAsync();
             return categories;
        }

   
    }
}
