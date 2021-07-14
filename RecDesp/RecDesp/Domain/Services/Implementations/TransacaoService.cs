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
    public class TransacaoService : ITransacaoService
    {
        private readonly ITransacaoRepository _transacaoRepository;
        private readonly IAreaRepository _areaRepository;

        public TransacaoService(ITransacaoRepository transacaoRepository, IAreaRepository areaRepository)
        {
            _transacaoRepository = transacaoRepository;
            _areaRepository = areaRepository;
        }

        public async Task<List<Transacao>> ListTransacao()
        {
            List<Transacao> listTransacoes = await _transacaoRepository.FindAll();
            return listTransacoes;
        }

        public async Task<List<Transacao>> ListTransacaoByOrigem(int origem)
        {
            List<Transacao> listTransacoes = await _transacaoRepository.Query()
                    .Where(t => t.OrigemTipo == origem).ToListAsync();
            return listTransacoes;
        }

        public async Task<Transacao> GetTransacaoById(long transacaoId)
        {
            Transacao newTransacao = await _transacaoRepository.GetAsync(transacaoId);

            if (newTransacao == null) throw new ArgumentException("A transação não existe!");

            return newTransacao;
        }

        public async Task<Transacao> CreateTransacao(Transacao transacao)
        {
            // verificando se a área existe
            Area area = await _areaRepository.GetAsync(transacao.AreaId);

            if (area == null) throw new ArgumentException("Área inválida!");

            transacao.Data = DateTime.Now; // salva a data de quando foi feita a transação
            Transacao newTransacao = await _transacaoRepository.CreateAsync(transacao);

            return newTransacao;
        }

        public async Task<bool> DeleteTransacao(long transacaoId)
        {
            bool newTransacao = await _transacaoRepository.DeleteAsync(transacaoId);
            if (newTransacao == false) throw new ArgumentException("A transação não existe!");

            return true;
        }

        public async Task<Transacao> UpdateTransacao(Transacao transacao)
        {
            Transacao newTransacao = await _transacaoRepository.UpdateAsync(transacao);

            if (newTransacao == null) throw new ArgumentException("A transação não existe!");

            return newTransacao;
        }
    }
}
