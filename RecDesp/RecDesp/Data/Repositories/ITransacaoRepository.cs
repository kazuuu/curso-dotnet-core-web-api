using RecDesp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecDesp.Data.Repositories
{
    public interface ITransacaoRepository : IGenericRepository<Transacao, long>
    {
    }
}
