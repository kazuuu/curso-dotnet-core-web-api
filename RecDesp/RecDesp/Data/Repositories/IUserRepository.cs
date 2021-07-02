using RecDesp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecDesp.Data.Repositories
{
    public interface IUserRepository
    {
        ApplicationUser GetUserById(Guid userId);
        Task<List<ApplicationUser>> List();
    }
}
