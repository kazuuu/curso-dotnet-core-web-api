using RecDesp.Data.Repositories;
using RecDesp.Domain.Models;
using RecDesp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecDesp.Domain.Services.Implementations
{
    public class DebitoService : IDebitoService
    {
        private readonly IDebitoRepository _debitoRepository;
        private readonly IAreaRepository _areaRepository;
        private readonly ITransacaoRepository _transacaoRepository;

        public DebitoService(IDebitoRepository debitoRepository, IAreaRepository areaRepository, ITransacaoRepository transacaoRepository)
        {
            _debitoRepository = debitoRepository;
            _areaRepository = areaRepository;
            _transacaoRepository = transacaoRepository;
        }

        public async Task<List<Debito>> ListDebitos()
        {
            List<Debito> listDebitos = await _debitoRepository.FindAll();
            return listDebitos;
        }

        public async Task<Debito> GetDebitoById(long debitoId)
        {
            Debito newDebito = await _debitoRepository.GetAsync(debitoId);
            return newDebito;
        }

        public async Task<Debito> CreateDebito(Debito debito)
        {
            // verifica se o saldo é valido e atualiza o saldo de cada área
            bool valida = await AtualizaSaldo(debito, "sub");

            if (valida)
            {
                // gera um externalId aleatório
                Random randNum = new Random();
                debito.ExternalId = randNum.Next(1000);

                debito.Data = DateTime.Now;

                Debito newDebito = await _debitoRepository.CreateAsync(debito);
                await NewTransacao(newDebito);

                return newDebito;
            }
            else
                return null;
        }

        public async Task<bool> DeleteDebito(long debitoId)
        {
            Debito debito = await _debitoRepository.GetAsync(debitoId);
            bool newDebito = await _debitoRepository.DeleteAsync(debitoId);

            if (newDebito)
                return await AtualizaSaldo(debito, "soma");
            else
                return false;
        }

        private async Task NewTransacao(Debito newDebito)
        {
            Transacao transacao = new Transacao
            {
                Data = DateTime.Now,
                AreaId = newDebito.AreaId,
                Area = newDebito.Area,
                Contraparte = newDebito.ExternalName,
                Valor = newDebito.Valor,
                Descricao = newDebito.Descricao,
                OrigemTipo = 2
            };

            await _transacaoRepository.CreateAsync(transacao);
        }

        private async Task<bool> AtualizaSaldo(Debito newDebito, string opArea)
        {
            Area area = _areaRepository.Get(newDebito.AreaId);

            if (area.Saldo <= 0 || area.Saldo < newDebito.Valor)
                return false;
            else
            {
                area.Saldo = CalculaSaldo(newDebito.Valor, area.Saldo, opArea);

                await _areaRepository.UpdateAsync(area);

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
