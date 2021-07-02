using RecDesp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecDesp.Domain.Services
{
    public interface IAreaService
    {
        Task<List<Area>> ListAreas();
        Task<Area> GetAreaById(long areaId);
        Task<Area> CreateArea(Area area);
        Task<Area> UpdateArea(Area area);
        Task<bool> DeleteArea(long areaId);
        Task<string> AddUser(long id, string userName);
    }
}
