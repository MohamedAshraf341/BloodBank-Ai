using Api.Const;
using Api.Dto.Idintity;
using Api.Dto.User;
using Api.Helpers;
using Api.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]

    public class AdminstrationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        private readonly IAuthService _authService;
        public AdminstrationController(IAuthService authService, IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
            _authService = authService;
        }
        [HttpGet("getmoderators")]
        public async Task<ApiResponse<List<AdminstrationDto>>> GetModerators()
        {
            try
            {
                var users = await _authService.GetRolesForUser();
                if (users == null)
                    return new ApiResponse<List<AdminstrationDto>> { Success = false, Message = "Not Found" };
                return new ApiResponse<List<AdminstrationDto>> { Success = true, Message = "List Of Admin",Data=users };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<AdminstrationDto>> { Success = false, Message = ex.Message };
            }
            
        }
        [HttpPost("addmoderator")]
        public async Task<ApiResponse<AddSiteAdminDto>> AddModerator([FromBody] AddSiteAdminDto Dto)
        {
            
            try
            {
                var user = await _uow.Users.FindAsync(u => u.UserName == Dto.UserName);
                if (user == null)
                    return new ApiResponse<AddSiteAdminDto> { Success = false, Message = "Not Found" };
                var role = Dto.Roles.ToString();
                var model = new AddRoleDto { UserId = user.Id, Role = role };
                var addrole = await _authService.AddRoleAsync(model);
                if (!string.IsNullOrEmpty(addrole))
                    return new ApiResponse<AddSiteAdminDto> { Success = false, Message = addrole };
                return new ApiResponse<AddSiteAdminDto> { Success = true, Message = addrole ,Data=Dto};
            }
            catch (Exception ex)
            {
                return new ApiResponse<AddSiteAdminDto> { Success = false, Message = ex.Message };
            }
        }
        [HttpDelete("deletemoderator")]
        public async Task<ApiResponse<string>> RemoveModerator(AddRoleDto model)
        {
            try
            {
                var result = await _authService.RemoveRoleAsync(model);
                return new ApiResponse<string> { Success = false, Message = result };
            }
            catch (Exception ex)
            {
                return new ApiResponse<string> { Success = false, Message = ex.Message };
            }
            
        }
    }
}
