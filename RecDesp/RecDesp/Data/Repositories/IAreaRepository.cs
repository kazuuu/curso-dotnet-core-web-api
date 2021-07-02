using RecDesp.Domain.Models;
using RecDesp.Models;
using System.Threading.Tasks;

namespace RecDesp.Data.Repositories
{
    public interface IAreaRepository : IGenericRepository<Area, long>
    {
        Task<bool> AddUser(Area area);
    }
}
