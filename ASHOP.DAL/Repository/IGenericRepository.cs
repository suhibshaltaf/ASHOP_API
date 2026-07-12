using ASHOP.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ASHOP.DAL.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync(string[]? includes = null);
        Task<T> CreateAsync(T request);
        Task<T> Getone(Expression<Func<T, bool>> filter, string[]? includes = null);
        Task<T> UpdateAsync(T request);
      Task <bool>DeleteAsync(T request);


    }
}
