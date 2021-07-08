using RecDesp.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecDesp.Domain.Services
{
    public interface ITransacaoService
    {
        Task<List<Transacao>> ListTransacao();
        Task<List<Transacao>> ListTransacaoByOrigem(int origem);
        Task<Transacao> GetTransacaoById(long transacaoId);
        Task<Transacao> CreateTransacao(Transacao transacao);
        Task<Transacao> UpdateTransacao(Transacao transacao);
        Task<bool> DeleteTransacao(long transacaoId);
    }
}
