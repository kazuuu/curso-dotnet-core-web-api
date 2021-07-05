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

        public TransferenciaService(ITransferenciaRepository transferenciaRepository, IAreaRepository areaRepository)
        {
            _transferenciaRepository = transferenciaRepository;
            _areaRepository = areaRepository;
        }

        public async Task<List<Transferencia>> ListTransferencias()
        {
            List<Transferencia> listTransferencias = await _transferenciaRepository.FindAll();
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
            bool valida = await atualizaSaldo(transferencia);

            if (valida) 
            {
                transferencia.Data = DateTime.Now;
                Transferencia newTransferencia = await _transferenciaRepository.CreateAsync(transferencia);

                return newTransferencia;
            }    
            else 
                return null;
        }

        public async Task<bool> DeleteTransferencia(long transferenciaId)
        {
            bool newTransferencia = await _transferenciaRepository.DeleteAsync(transferenciaId);

            if (newTransferencia)
                return true;
            else
                return false;
        }

        private async Task<bool> atualizaSaldo(Transferencia newTransferencia)
        {
            Area fromArea = _areaRepository.Get(newTransferencia.FromAreaId);
            Area toArea = _areaRepository.Get(newTransferencia.ToAreaId);

            if (fromArea.Saldo <= 0 || fromArea.Saldo < newTransferencia.Valor) return false;
            else
            {
                fromArea.Saldo -= newTransferencia.Valor;
                toArea.Saldo += newTransferencia.Valor;

                await _areaRepository.UpdateAsync(fromArea);
                await _areaRepository.UpdateAsync(toArea);

                return true;
            }
        }
    }
}
