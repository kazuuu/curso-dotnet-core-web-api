using RecDesp.Data.Repositories;
using RecDesp.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecDesp.Domain.Services.Implementations
{
    public class CobrancaService : ICobrancaService
    {
        private readonly ICobrancaRepository _cobrancaRepository;

        public CobrancaService(ICobrancaRepository cobrancaRepository)
        {
            _cobrancaRepository = cobrancaRepository;
        }

        public async Task<List<Cobranca>> ListCobrancas()
        {
            List<Cobranca> listCobrancas = await _cobrancaRepository.FindAll();
            return listCobrancas;
        }

        public async Task<Cobranca> GetCobrancaById(long cobrancaId)
        {
            Cobranca newCobranca = await _cobrancaRepository.GetAsync(cobrancaId);
            return newCobranca;
        }

        public async Task<Cobranca> CreateCobranca(Cobranca cobranca)
        {
            cobranca.Status = 1; // Setando a cobrança como pendente
            Cobranca newCobranca = await _cobrancaRepository.CreateAsync(cobranca);
            return newCobranca;
        }

        public async Task<bool> DeleteCobranca(long cobrancaId)
        {
            bool newCobranca = await _cobrancaRepository.DeleteAsync(cobrancaId);

            if (newCobranca)
                return true;
            else
                return false;
        }

        public async Task<Cobranca> CobrancaResponse(long cobrancaId, int status)
        {
            Cobranca newCobranca = await _cobrancaRepository.GetAsync(cobrancaId);

            if ((status < 1 || status > 3) || newCobranca == null)
                return null;
            else
            {
                newCobranca.Status = status;
                newCobranca = await _cobrancaRepository.UpdateAsync(newCobranca);
                return newCobranca;
            }
        }
    }
}
