using Microsoft.EntityFrameworkCore;
using RecDesp.Data.Repositories;
using RecDesp.Domain.Models;
using RecDesp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecDesp.Domain.Services.Implementations
{
    public class CobrancaService : ICobrancaService
    {
        private readonly ICobrancaRepository _cobrancaRepository;
        private readonly IAreaRepository _areaRepository;
        private readonly ITransacaoRepository _transacaoRepository;

        public CobrancaService(ICobrancaRepository cobrancaRepository, IAreaRepository areaRepository, ITransacaoRepository transacaoRepository)
        {
            _cobrancaRepository = cobrancaRepository;
            _areaRepository = areaRepository;
            _transacaoRepository = transacaoRepository;
        }

        public async Task<List<Cobranca>> ListCobrancas()
        {
            List<Cobranca> listCobrancas = await _cobrancaRepository.FindAll();

            /* Tentei fazer com que a área não retorne como null
            for (int i = 0; i < listCobrancas.Count; i++)
            {
                listCobrancas[i].FromArea = _areaRepository.Get(listCobrancas[i].FromAreaId);
                listCobrancas[i].ToArea = _areaRepository.Get(listCobrancas[i].ToAreaId);
            }
            */

            return listCobrancas;
        }

        public async Task<List<Cobranca>> ListCobrancasByStatus(int status)
        {
            List<Cobranca> listCobrancas = await _cobrancaRepository.Query()
                    .Where(c => c.Status == status).ToListAsync();
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
            Cobranca cobranca = await _cobrancaRepository.GetAsync(cobrancaId);
            bool newCobranca = await _cobrancaRepository.DeleteAsync(cobrancaId);

            if (newCobranca)
                return await AtualizaSaldo(cobranca, "sub", "soma"); 
            else
                return false;
        }

        public async Task<Cobranca> CobrancaResponse(long cobrancaId, int status)
        {
            Cobranca newCobranca = await _cobrancaRepository.GetAsync(cobrancaId);
            bool valida = false;
            
            if (status == 2)
            {
                // Se a cobrança for aprovada irá gerar uma nova transação
                await NewTransacao(newCobranca);

                // verifica se o saldo é valido e atualiza o saldo de cada área
                valida = await AtualizaSaldo(newCobranca, "soma", "sub");                
            }
                
            if (status < 1 || status > 3 || newCobranca == null || valida == false)
                return null;
            else
            {
                newCobranca.Status = status;
                newCobranca = await _cobrancaRepository.UpdateAsync(newCobranca);

                return newCobranca;
            }
        }

        private async Task NewTransacao(Cobranca newCobranca)
        {
            Area toArea = await _areaRepository.GetAsync(newCobranca.ToAreaId);

            Transacao transacao = new Transacao
            {
                Data = DateTime.Now,
                AreaId = newCobranca.FromAreaId,
                Area = newCobranca.FromArea,
                Contraparte = toArea.NomeArea,
                Valor = newCobranca.Valor,
                Descricao = newCobranca.Descricao,
                OrigemTipo = 4
            };

            await _transacaoRepository.CreateAsync(transacao);
        }

        private async Task<bool> AtualizaSaldo(Cobranca newCobranca, string opFromArea, string opToArea)
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
                fromArea.Saldo = CalculaSaldo(newCobranca.Valor, fromArea.Saldo, opFromArea);
                toArea.Saldo = CalculaSaldo(newCobranca.Valor, toArea.Saldo, opToArea);

                await _areaRepository.UpdateAsync(fromArea);
                await _areaRepository.UpdateAsync(toArea);

                return true;
            }           
        }

        private static double CalculaSaldo(double valor, double saldo, string operacao)
        {
            if (operacao == "soma")
                return saldo += valor;
            else
                return saldo -= valor;
        }
    }
}
