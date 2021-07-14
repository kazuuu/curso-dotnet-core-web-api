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
    public class CreditoService : ICreditoService
    {
        private readonly ICreditoRepository _creditoRepository;
        private readonly IAreaRepository _areaRepository;
        private readonly ITransacaoRepository _transacaoRepository;

        public CreditoService(ICreditoRepository creditoRepository, IAreaRepository areaRepository, ITransacaoRepository transacaoRepository)
        {
            _creditoRepository = creditoRepository;
            _areaRepository = areaRepository;
            _transacaoRepository = transacaoRepository;
        }

        public async Task<List<Credito>> ListCreditos()
        {
            List<Credito> listCreditos = await _creditoRepository.FindAll();
            return listCreditos;
        }

        public async Task<List<Credito>> ListCreditosByValor(double valorMin, double valorMax)
        {
            List<Credito> listCreditos = await _creditoRepository.Query()
                    .Where(c => c.Valor >= valorMin && c.Valor <= valorMax).ToListAsync();
            return listCreditos;
        }

        public async Task<Credito> GetCreditoById(long creditoId)
        {
            Credito newCredito = await _creditoRepository.GetAsync(creditoId);

            if (newCredito == null) throw new ArgumentException("O crédito não existe!");

            return newCredito;
        }

        public async Task<Credito> CreateCredito(Credito credito)
        {
            Area area = await _areaRepository.GetAsync(credito.AreaId);

            if (area == null) throw new ArgumentException("Área inválida");

            credito.Data = DateTime.Now;

            Credito newCredito = await _creditoRepository.CreateAsync(credito);
            await NewTransacao(newCredito);
            await AtualizaSaldo(newCredito, "soma");

            return newCredito;
        }

        public async Task<bool> DeleteCredito(long creditoId)
        {
            Credito credito = await _creditoRepository.GetAsync(creditoId);
            bool newCredito = await _creditoRepository.DeleteAsync(creditoId);

            if (newCredito == false) throw new ArgumentException("O crédito não existe!");

            await AtualizaSaldo(credito, "sub");

            return true;
        }

        private async Task NewTransacao(Credito newCredito)
        {
            Transacao transacao = new Transacao
            {
                Data = DateTime.Now,
                AreaId = newCredito.AreaId,
                Area = newCredito.Area,
                Contraparte = newCredito.ExternalName,
                Valor = newCredito.Valor,
                Descricao = newCredito.Descricao,
                OrigemTipo = 1
            };

            await _transacaoRepository.CreateAsync(transacao);
        }

        private async Task AtualizaSaldo(Credito newCredito, string opArea)
        {
            Area area = _areaRepository.Get(newCredito.AreaId);

            area.Saldo = CalculaSaldo(newCredito.Valor, area.Saldo, opArea);

            await _areaRepository.UpdateAsync(area);
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
