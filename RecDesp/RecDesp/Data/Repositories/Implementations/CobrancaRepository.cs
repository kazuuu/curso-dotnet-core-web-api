using RecDesp.Domain.Models;
using RecDesp.Infra;

namespace RecDesp.Data.Repositories.Implementations
{
    public class CobrancaRepository : GenericRepository<Cobranca, long>, ICobrancaRepository
    {
        private readonly MySQLContext _context;

        public CobrancaRepository(MySQLContext context) : base(context)
        {
            _context = context;
        }
    }
}
