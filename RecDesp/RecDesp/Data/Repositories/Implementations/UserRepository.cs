using Microsoft.EntityFrameworkCore;
using RecDesp.Domain.Models;
using RecDesp.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecDesp.Data.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly MySQLContext _context;
        private DbSet<ApplicationUser> _data;

        public UserRepository(MySQLContext context)
        {
            _context = context;
            _data = _context.Set<ApplicationUser>();
        }

        public ApplicationUser GetUserById(Guid userId)
        {
            ApplicationUser applicationUser = _data.SingleOrDefault(s => s.Id.Equals(userId));

            return applicationUser;
        }

        public async Task<List<ApplicationUser>> List()
        {
            return await _data.AsNoTracking().ToListAsync();
        }
    }
}
