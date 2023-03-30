using BL.Dtos;
using DAL.Models;

namespace BL.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseAuthDto> RegisterAsync(RegisterDto model);
        Task<ResponseAuthDto> GetTokenAsync(LoginDto model);
        Task<string> AddRoleAsync(AddRoleDto model);
        Task<string> RemoveRoleAsync(AddRoleDto model);
        Task<List<AdminstrationDto>> GetRolesForUser();
        Task<ResponseAuthDto> RefreshTokenAsync(string token);
        Task<bool> RevokeTokenAsync(string token);
    }
}