using AutoMapper;
using BL.Dtos;
using BL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
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
        public async Task<IActionResult> GetModerators()
        {
            var users = await _authService.GetRolesForUser();
            if(users == null)
                return NotFound("NotFound");
            return Ok(users);
        }
        [HttpPost("addmoderator")]
        public async Task<IActionResult> AddModerator([FromForm] AddAdminDto Dto)
        {
            var user = await _uow.Users.FindAsync(u => u.UserName == Dto.UserName);
            if (user == null)
                return NotFound("User Not Found");
            var role = Dto.Roles.ToString();
            var model = new AddRoleDto { UserId = user.Id, Role = role };
            var addrole = await _authService.AddRoleAsync(model);
            if (!string.IsNullOrEmpty(addrole))
                return BadRequest(addrole);
            return Ok(addrole);
        }
        [HttpDelete("deletemoderator")]
        public async Task<IActionResult> RemoveModerator(AddRoleDto model)
        {
            var result = await _authService.RemoveRoleAsync(model);
            return Ok(result);
        }
    }
}
