using RecDesp.Data.Repositories;
using RecDesp.Domain.Models;
using RecDesp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecDesp.Domain.Services.Implementations
{
    public class CobrancaService : ICobrancaService
    {
        private readonly ICobrancaRepository _cobrancaRepository;
        private readonly IAreaRepository _areaRepository;

        public CobrancaService(ICobrancaRepository cobrancaRepository, IAreaRepository areaRepository)
        {
            _cobrancaRepository = cobrancaRepository;
            _areaRepository = areaRepository;
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
            bool valida = false;
            
            if (status == 2)
            {
                // verifica se o saldo é valido e atualiza o saldo de cada área
                valida = await atualizaSaldo(newCobranca);
            }
                
            if (status < 1 || status > 3 || newCobranca == null || !valida)
                return null;
            else
            {
                newCobranca.Status = status;
                newCobranca = await _cobrancaRepository.UpdateAsync(newCobranca);

                return newCobranca;
            }
        }

        
        private async Task<bool> atualizaSaldo(Cobranca newCobranca)
        {
            Area fromArea = _areaRepository.Get(newCobranca.FromAreaId);
            Area toArea = _areaRepository.Get(newCobranca.ToAreaId);

            if (toArea.Saldo <= 0 || toArea.Saldo < newCobranca.Valor)
            {
                newCobranca.Status = 3;
                return false;
            }
            else
            {
                fromArea.Saldo += newCobranca.Valor;
                toArea.Saldo -= newCobranca.Valor;

                await _areaRepository.UpdateAsync(fromArea);
                await _areaRepository.UpdateAsync(toArea);

                return true;
            }           
        }
    }
}
