using RecDesp.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecDesp.Domain.Services
{
    public interface IDebitoService
    {
        Task<List<Debito>> ListDebitos();
        Task<Debito> GetDebitoById(long debitoId);
        Task<Debito> CreateDebito(Debito debito);
        Task<bool> DeleteDebito(long debitoId);
    }
}
