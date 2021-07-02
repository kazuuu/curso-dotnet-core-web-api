using Microsoft.EntityFrameworkCore;
using RecDesp.Domain.Models;
using RecDesp.Infra;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecDesp.Data.Repositories.Implementations
{
    public class GenericRepository<T, T_ID> : IGenericRepository<T, T_ID> where T : BaseModel<T_ID> where T_ID : struct
    {
        private readonly MySQLContext _context;

        public GenericRepository(MySQLContext context)
        {
            _context = context;
        }

        public virtual async Task<T> CreateAsync(T entity)
        {
            var ret = await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            ret.State = EntityState.Detached;

            return ret.Entity;
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            var entry = _context.Entry(entity);

            if (entry != null)
            {
                entry.State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return entry.Entity;
            }

            throw new KeyNotFoundException("Entidade não encontrada");
        }

        public virtual async Task<bool> DeleteAsync(T_ID id)
        {
            var entry = _context.Set<T>().SingleOrDefault(p => p.Id.Equals(id));
            
            if (entry != null)
            {
                _context.Set<T>().Remove(entry);
                await _context.SaveChangesAsync();

                return true;
            }

            throw new KeyNotFoundException("Entidade não encontrada");
        }

        public virtual async Task<List<T>> FindAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public T Get(T_ID id)
        {
            return _context.Set<T>().Find(id);
        }

        public virtual async Task<T> GetAsync(T_ID id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public IQueryable<T> Query()
        {
            return _context.Set<T>().AsNoTracking();
        }

    }
}
