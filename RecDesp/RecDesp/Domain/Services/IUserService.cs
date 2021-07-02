using RecDesp.Domain.Models;
using RecDesp.Domain.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecDesp.Domain.Services
{
    public interface IUserService
    {
        Task<SsoDto> Login(LoginDto loginDto);
        Task<bool> Register(RegisterDto registerDto);
        Task<ApplicationUser> GetCurrentUser();
        Task<List<ApplicationUser>> ListUsers();
    }
}
