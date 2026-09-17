using ASHOP.DAL.Data;
using ASHOP.DAL.Models;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ASHOP.DAL.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<T> CreateAsync(T request)
        {
            await _context.AddAsync(request);
            return request;
        }
         
    

        public async Task<List<T>> GetAllAsync(string[]? includes = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
            {
                foreach (var include in includes)
                {

                    query = query.Include(include);
                }
            }


            return await query.ToListAsync();

        }
        public async Task<T> Getone(Expression<Func<T, bool>> filter, string[]? includes = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
            {
                foreach (var include in includes)
                {

                    query = query.Include(include);
                }
            }


            return await query.FirstOrDefaultAsync(filter);

        }
        public void Delete(T request)
        {
            _context.Remove(request);   
      
        }

        public async Task<T> UpdateAsync(T request)
        {
            _context.Update(request);
             return request;
        }
    }
}
