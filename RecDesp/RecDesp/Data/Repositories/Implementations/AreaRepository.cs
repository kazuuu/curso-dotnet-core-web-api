using Microsoft.EntityFrameworkCore;
using RecDesp.Domain.Models;
using RecDesp.Infra;
using RecDesp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RecDesp.Data.Repositories.Implementations
{
    public class AreaRepository : GenericRepository<Area, long>, IAreaRepository
    {
        private readonly MySQLContext _context;

        public AreaRepository(MySQLContext context) : base(context)
        {
            _context = context;
        }

        public virtual async Task<bool> AddUser(Area area)
        {
            var entry = _context.Entry(area);

            if (entry != null)
            {        
                entry.State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return true;
            }

            throw new KeyNotFoundException("Entidade não encontrada");
        }
    }
}
