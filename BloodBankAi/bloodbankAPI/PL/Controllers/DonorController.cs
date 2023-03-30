using AutoMapper;
using BL.Dtos;
using BL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonorController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        public DonorController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        [HttpGet("getalldonors")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _uow.Users.GetAllAsync();
            if (users == null)
                return NotFound();
            var dto = _mapper.Map<IEnumerable<GetDonorDto>>(users);
            return Ok(dto);
        }
        [HttpGet("getdonorbyid/{id}")]
        public async Task<IActionResult> GetUserByID(string id)
        {
            var donorByAddresss = await _uow.Adresses.FindAsync(u => u.UserId == id, new string[] { "User" });
            if (donorByAddresss == null)
                return NotFound();
            var dto = _mapper.Map<GetDonorById>(donorByAddresss);
            return Ok(dto);
        }

    }
}
