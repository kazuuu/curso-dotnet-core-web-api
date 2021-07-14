using Microsoft.EntityFrameworkCore;
using RecDesp.Infra;
using RecDesp.Models;
using System;
using System.Threading.Tasks;

namespace RecDesp.Data.Repositories.Implementations
{
    public class AreaRepository : GenericRepository<Area, long>, IAreaRepository
    {
        private readonly MySQLContext _context;

        public AreaRepository(MySQLContext context) : base(context)
        {
            _context = context;
        }

        public virtual async Task<bool> AddUserToArea(Area area)
        {
            var entry = _context.Entry(area);

            if (entry == null) throw new ArgumentException("Entidade não encontrada");
            
            entry.State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
