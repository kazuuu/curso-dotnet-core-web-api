using RecDesp.Domain.Models;
using RecDesp.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecDesp.Data.Repositories.Implementations
{
    public class DebitoRepository : GenericRepository<Debito, long>, IDebitoRepository
    {
        private readonly MySQLContext _context;

        public DebitoRepository(MySQLContext context) : base(context)
        {
            _context = context;
        }
    }
}
