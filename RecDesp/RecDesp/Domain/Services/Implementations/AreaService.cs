using Microsoft.AspNetCore.Identity;
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
    public class AreaService : IAreaService
    {
        private readonly IAreaRepository _areaRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public AreaService(IAreaRepository areaRepository, UserManager<ApplicationUser> userManager)
        {
            _areaRepository = areaRepository;
            _userManager = userManager;
        }

        public async Task<List<Area>> ListAreas()
        {
            List<Area> listAreas = await _areaRepository.FindAll();
            return listAreas;
        }

        public async Task<List<Area>> ListAreasBySaldo(double min, double max)
        {
            List<Area> listAreas = await _areaRepository.Query()
                    .Where(a => a.Saldo >= min && a.Saldo <= max).ToListAsync();
            return listAreas;
        }

        public async Task<Area> GetAreaById(long areaId)
        {
            Area newArea = await _areaRepository.GetAsync(areaId);

            if (newArea == null) throw new ArgumentException("A área não existe!");

            return newArea;
        }

        public async Task<Area> CreateArea(Area area)
        {
            Area newArea = await _areaRepository.CreateAsync(area);
            return newArea;
        }

        public async Task<bool> DeleteArea(long areaId)
        {
            bool newArea = await _areaRepository.DeleteAsync(areaId);

            if (newArea == false) throw new ArgumentException("A área não existe!");

            return true;
        }

        public async Task<Area> UpdateArea(Area area)
        {
            Area newArea = await _areaRepository.UpdateAsync(area);
            if (newArea == null) throw new ArgumentException("A área não existe!");

            return newArea;
        }

        public async Task<bool> AddUserToArea(long areaId, string userName)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(userName);
            Area area = await _areaRepository.GetAsync(areaId);

            if (user == null)
                throw new ArgumentException("Usuário não existe!");
            if (area == null)
                throw new ArgumentException("Área não existe!");

            // verifica se a lista de usuários está vazia
            if (area.Users == null) area.Users = new List<ApplicationUser>();

            area.Users.Add(user);
            await _areaRepository.AddUserToArea(area);

            return true;
        }
    }
}
