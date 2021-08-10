using Microsoft.EntityFrameworkCore;
using MyWallWebAPI.Domain.Models;
using MyWallWebAPI.Infrastructure.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWallWebAPI.Infrastructure.Data.Repositories
{
    public class UserRepository
    {
        private readonly MySQLContext _context;

        public UserRepository(MySQLContext context)
        {
            _context = context;
        }

        public async Task<List<ApplicationUser>> ListUsers()
        {
            List<ApplicationUser> list = await _context.User.ToListAsync();

            return list;
        }

        public async Task<ApplicationUser> GetUser(string userId)
        {
            ApplicationUser user = await _context.User.FindAsync(userId);

            return user;
        }

        public async Task<ApplicationUser> CreateUser(ApplicationUser user)
        {
            var ret = await _context.User.AddAsync(user);

            await _context.SaveChangesAsync();

            ret.State = EntityState.Detached;

            return ret.Entity;
        }

        public async Task<int> UpdateUser(ApplicationUser user)
        {
            _context.Entry(user).State = EntityState.Modified;

            return await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteUser(string userId)
        {
            var item = await _context.User.FindAsync(userId);
            _context.User.Remove(item);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
