using RecDesp.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecDesp.Domain.Services
{
    public interface ITransferenciaService
    {
        Task<List<Transferencia>> ListTransferencias();
        Task<List<Transferencia>> ListTransferenciasByValor(double valorMin, double valorMax);
        Task<Transferencia> GetTransferenciaById(long transferenciaId);
        Task<Transferencia> CreateTransferencia(Transferencia transferencia);
        Task<bool> DeleteTransferencia(long transferenciaId);
    }
}
