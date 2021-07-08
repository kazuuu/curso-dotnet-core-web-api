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

            if (newArea)
                return true;
            else
                return false;
        }

        public async Task<Area> UpdateArea(Area area)
        {
            Area newArea = await _areaRepository.UpdateAsync(area);
            return newArea;
        }

        public async Task<string> AddUser(long id, string userName)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(userName);
            Area area = await _areaRepository.GetAsync(id);

            if (user != null && area != null)
            {
                area.Users.Add(user);
                await _areaRepository.AddUser(area);

                return "Usuário inserido na área com sucesso!";
            }
            else
                return "Usuário ou área não existe!";
        }
    }
}
