using RecDesp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecDesp.Domain.Services
{
    public interface ICobrancaService
    {
        Task<List<Cobranca>> ListCobrancas();
        Task<List<Cobranca>> ListCobrancasByStatus(int status);
        Task<Cobranca> GetCobrancaById(long cobrancaId);
        Task<Cobranca> CreateCobranca(Cobranca cobranca);
        Task<bool> DeleteCobranca(long cobrancaId);
        Task<Cobranca> CobrancaResponse(long cobrancaId, int status);
    }
}
