using Api.Dto;
using Api.Interfaces;
using AutoMapper;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
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
            var dto = _mapper.Map<IEnumerable<GetDonorsDto>>(users);
            return Ok(dto);
        }
        [HttpGet("getdonorbyid/{id}")]
        public async Task<IActionResult> GetUserByID(string id)
        {
            var donorByAddresss = await _uow.Users.FindAsync(u => u.Id == id, new string[] { "Address" });
            if (donorByAddresss == null)
                return NotFound();
            var dto = _mapper.Map<GetDonorById>(donorByAddresss);
            return Ok(dto);
        }

    }
}
