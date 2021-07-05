using RecDesp.Domain.Models;
using RecDesp.Infra;

namespace RecDesp.Data.Repositories.Implementations
{
    public class CreditoRepository : GenericRepository<Credito, long>, ICreditoRepository
    {
        private readonly MySQLContext _context;

        public CreditoRepository(MySQLContext context) : base(context)
        {
            _context = context;
        }
    }
}
