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

        public CreditoService(ICreditoRepository creditoRepository, IAreaRepository areaRepository)
        {
            _creditoRepository = creditoRepository;
            _areaRepository = areaRepository;
        }

        public async Task<List<Credito>> ListCreditos()
        {
            List<Credito> listCreditos = await _creditoRepository.FindAll();
            return listCreditos;
        }

        public async Task<Credito> GetCreditoById(long creditoId)
        {
            Credito newCredito = await _creditoRepository.GetAsync(creditoId);
            return newCredito;
        }

        public async Task<Credito> CreateCredito(Credito credito)
        {
            // gera um externalId aleatório
            Random randNum = new Random();
            credito.ExternalId = randNum.Next(1000);

            Credito newCredito = await _creditoRepository.CreateAsync(credito);
            atualizaSaldo(newCredito);

            return newCredito;
        }

        public async Task<bool> DeleteCredito(long creditoId)
        {
            bool newCredito = await _creditoRepository.DeleteAsync(creditoId);

            if (newCredito)
                return true;
            else
                return false;
        }

        private async void atualizaSaldo(Credito newCredito)
        {
            Area area = _areaRepository.Get(newCredito.AreaId);

            area.Saldo += newCredito.Valor;

            await _areaRepository.UpdateAsync(area);
        }
    }
}
