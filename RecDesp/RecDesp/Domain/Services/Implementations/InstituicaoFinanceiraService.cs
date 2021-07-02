using RecDesp.Data.Repositories;
using RecDesp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecDesp.Domain.Services.Implementations
{
    public class InstituicaoFinanceiraService : IInstituicaoFinanceiraService
    {
        private readonly IInstituicaoFinanceiraRepository _instituicaoRepository;

        public InstituicaoFinanceiraService(IInstituicaoFinanceiraRepository instituicaoRepository)
        {
            _instituicaoRepository = instituicaoRepository;
        }

        public async Task<List<InstituicaoFinanceira>> ListInstituicoes()
        {
            List<InstituicaoFinanceira> listInstituicoes = await _instituicaoRepository.FindAll();

            return listInstituicoes;
        }

        public async Task<InstituicaoFinanceira> GetInstituicaoById(int instituicaoId)
        {
            InstituicaoFinanceira instituicao = await _instituicaoRepository.GetAsync(instituicaoId);
            return instituicao;
        }
    }
}
