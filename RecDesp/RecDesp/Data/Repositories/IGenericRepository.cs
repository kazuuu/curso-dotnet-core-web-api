using RecDesp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecDesp.Data.Repositories
{
    public interface IGenericRepository<T, T_ID> where T : BaseModel<T_ID> where T_ID : struct
    {
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T_ID id);
        T Get(T_ID id);
        Task<T> GetAsync(T_ID id);
        IQueryable<T> Query();
        Task<List<T>> FindAll();
    }
}
