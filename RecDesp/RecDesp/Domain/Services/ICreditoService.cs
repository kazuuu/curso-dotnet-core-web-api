using RecDesp.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecDesp.Domain.Services
{
    public interface ICreditoService
    {
        Task<List<Credito>> ListCreditos();
        Task<List<Credito>> ListCreditosByValor(double valorMin, double valorMax);
        Task<Credito> GetCreditoById(long creditoId);
        Task<Credito> CreateCredito(Credito credito);
        Task<bool> DeleteCredito(long creditoId);
    }
}
