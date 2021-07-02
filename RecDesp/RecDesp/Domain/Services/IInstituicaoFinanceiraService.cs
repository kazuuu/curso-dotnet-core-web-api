using RecDesp.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecDesp.Domain.Services
{
    public interface IInstituicaoFinanceiraService
    {
        Task<List<InstituicaoFinanceira>> ListInstituicoes();
        Task<InstituicaoFinanceira> GetInstituicaoById(int instituicaoId);
    }
}
