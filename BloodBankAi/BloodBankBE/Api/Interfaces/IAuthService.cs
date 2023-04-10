using Api.Dto;
using Api.Dto.Idintity;

namespace Api.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseAuthDto> RegisterAsync(RegisterDto model);
        Task<ResponseAuthDto> GetTokenAsync(LoginDto model);
        Task<string> AddRoleAsync(AddRoleDto model);
        Task<string> RemoveRoleAsync(AddRoleDto model);
        Task<ResponseAuthDto> RefreshTokenAsync(string token);
        Task<bool> RevokeTokenAsync(string token);
        Task<List<AdminstrationDto>> GetRolesForUser();
    }
}
