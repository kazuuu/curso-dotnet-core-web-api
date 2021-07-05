using RecDesp.Domain.Models;
using RecDesp.Infra;

namespace RecDesp.Data.Repositories.Implementations
{
    public class TransacaoRepository : GenericRepository<Transacao, long>, ITransacaoRepository
    {
        private readonly MySQLContext _context;

        public TransacaoRepository(MySQLContext context) : base(context)
        {
            _context = context;
        }
    }
}
