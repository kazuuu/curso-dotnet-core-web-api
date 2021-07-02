using RecDesp.Domain.Models;
using RecDesp.Infra;

namespace RecDesp.Data.Repositories.Implementations
{
    public class InstituicaoFinanceiraRepository : GenericRepository<InstituicaoFinanceira, int>, IInstituicaoFinanceiraRepository 
    {
        private readonly MySQLContext _context;

        public InstituicaoFinanceiraRepository(MySQLContext context) : base(context)
        {
            _context = context;
        }
    }
}
