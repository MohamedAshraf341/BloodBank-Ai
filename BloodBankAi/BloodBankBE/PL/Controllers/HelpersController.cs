using AutoMapper;
using BL.Dtos;
using BL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelpersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        public HelpersController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        [HttpGet("getallgovernorate")]
        public async Task<IActionResult> GetAllGovernorate()
        {
            var governorates = await _uow.Governorates.GetAllAsync();
            if (governorates == null)
                return NotFound("Not Found ");
            var dto = _mapper.Map<List<GovernorateDto>>(governorates);
            return Ok(dto);
        }
        [HttpGet("getallcities")]
        public async Task<IActionResult> GetAllCity()
        {
            var cities = await _uow.Cities.FindAllAsync(new string[] { "Governorate" });
            if (cities == null)
                return NotFound("Not Found");
            var dto = _mapper.Map<IEnumerable<CityDto>>(cities);
            return Ok(dto);
        }
    }
}
