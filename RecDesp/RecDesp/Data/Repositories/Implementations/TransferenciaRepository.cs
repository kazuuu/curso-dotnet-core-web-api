using RecDesp.Domain.Models;
using RecDesp.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecDesp.Data.Repositories.Implementations
{
    public class TransferenciaRepository : GenericRepository<Transferencia, long>, ITransferenciaRepository
    {
        private readonly MySQLContext _context;

        public TransferenciaRepository(MySQLContext context) : base(context)
        {
            _context = context;
        }
    }
}
