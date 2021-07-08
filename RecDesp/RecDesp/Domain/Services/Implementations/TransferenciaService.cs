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
    public class TransferenciaService : ITransferenciaService
    {
        private readonly ITransferenciaRepository _transferenciaRepository;
        private readonly IAreaRepository _areaRepository;
        private readonly ITransacaoRepository _transacaoRepository;

        public TransferenciaService(ITransferenciaRepository transferenciaRepository, IAreaRepository areaRepository, ITransacaoRepository transacaoRepository)
        {
            _transferenciaRepository = transferenciaRepository;
            _areaRepository = areaRepository;
            _transacaoRepository = transacaoRepository;
        }

        public async Task<List<Transferencia>> ListTransferencias()
        {
            List<Transferencia> listTransferencias = await _transferenciaRepository.FindAll();
            return listTransferencias;
        }

        public async Task<List<Transferencia>> ListTransferenciasByValor(double valorMin, double valorMax)
        {
            List<Transferencia> listTransferencias = await _transferenciaRepository.Query()
                    .Where(c => c.Valor >= valorMin && c.Valor <= valorMax).ToListAsync();
            return listTransferencias;
        }

        public async Task<Transferencia> GetTransferenciaById(long transferenciaId)
        {
            Transferencia newTransferencia = await _transferenciaRepository.GetAsync(transferenciaId);
            return newTransferencia;
        }

        public async Task<Transferencia> CreateTransferencia(Transferencia transferencia)
        {
            // verifica se o saldo é valido e atualiza o saldo de cada área
            bool valida = await AtualizaSaldo(transferencia, "sub", "soma");

            if (valida) 
            {
                transferencia.Data = DateTime.Now;
                Transferencia newTransferencia = await _transferenciaRepository.CreateAsync(transferencia);
                await NewTransacao(newTransferencia);

                return newTransferencia;
            }    
            else 
                return null;
        }

        public async Task<bool> DeleteTransferencia(long transferenciaId)
        {
            Transferencia transferencia = await _transferenciaRepository.GetAsync(transferenciaId);
            bool newTransferencia = await _transferenciaRepository.DeleteAsync(transferenciaId);

            if (newTransferencia)
                return await AtualizaSaldo(transferencia, "soma", "sub");
            else
                return false;
        }

        private async Task NewTransacao(Transferencia newTransferencia)
        {
            Area fromArea = await _areaRepository.GetAsync(newTransferencia.FromAreaId);

            Transacao transacao = new Transacao
            {
                Data = DateTime.Now,
                AreaId = newTransferencia.ToAreaId,
                Area = newTransferencia.ToArea,
                Contraparte = fromArea.NomeArea,
                Valor = newTransferencia.Valor,
                Descricao = newTransferencia.Descricao,
                OrigemTipo = 3
            };

            await _transacaoRepository.CreateAsync(transacao);
        }

        private async Task<bool> AtualizaSaldo(Transferencia newTransferencia, string opFromArea, string opToArea)
        {
            Area fromArea = _areaRepository.Get(newTransferencia.FromAreaId);
            Area toArea = _areaRepository.Get(newTransferencia.ToAreaId);

            if (fromArea.Saldo <= 0 || fromArea.Saldo < newTransferencia.Valor) return false;
            else
            {
                fromArea.Saldo = CalculaSaldo(newTransferencia.Valor, fromArea.Saldo, opFromArea);
                toArea.Saldo = CalculaSaldo(newTransferencia.Valor, toArea.Saldo, opToArea);

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
