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

            if (newCobranca == null) throw new ArgumentException("A cobrança não existe!");

            return newCobranca;
        }

        public async Task<Cobranca> CreateCobranca(Cobranca cobranca)
        {
            // pegando as áreas para serem salvas na cobrança
            Area fromNewArea = _areaRepository.Get(cobranca.FromAreaId);
            Area toNewArea = _areaRepository.Get(cobranca.ToAreaId);

            if (fromNewArea == null)
                throw new ArgumentException("A área cobradora é inválida!");
            if (toNewArea == null)
                throw new ArgumentException("A área cobrada é inválida!");

            cobranca.Data = DateTime.Now; // salva a data de quando foi feita a cobrança
            cobranca.Status = 1; // Setando a cobrança como pendente
            Cobranca newCobranca = await _cobrancaRepository.CreateAsync(cobranca);

            return newCobranca;
        }

        public async Task<bool> DeleteCobranca(long cobrancaId)
        {
            Cobranca cobranca = await _cobrancaRepository.GetAsync(cobrancaId);
            bool newCobranca = await _cobrancaRepository.DeleteAsync(cobrancaId);

            if (newCobranca == false) throw new ArgumentException("A cobrança não existe!");

            return await AtualizaSaldo(cobranca, "sub", "soma");
        }

        public async Task<Cobranca> CobrancaResponse(long cobrancaId, int status)
        {
            Cobranca newCobranca = await _cobrancaRepository.GetAsync(cobrancaId);
            if (status < 1 || status > 3 || newCobranca == null) 
                throw new ArgumentException("Cobrança Inválida!");

            if (status == 2)
            {
                // Se a cobrança for aprovada irá gerar uma nova transação
                await NewTransacao(newCobranca);
                // verifica se o saldo é valido e atualiza o saldo de cada área
                bool valida = await AtualizaSaldo(newCobranca, "soma", "sub");

                if (valida == false) throw new ArgumentException("O saldo da área é inválido!");
            }
            newCobranca.Status = status;
            newCobranca = await _cobrancaRepository.UpdateAsync(newCobranca);
            
            return newCobranca;
        }

        private async Task NewTransacao(Cobranca newCobranca)
        {
            Area toArea = await _areaRepository.GetAsync(newCobranca.ToAreaId);

            Transacao transacao = new()
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
