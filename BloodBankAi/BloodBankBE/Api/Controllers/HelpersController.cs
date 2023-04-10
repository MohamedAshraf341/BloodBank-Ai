using Api.Dto;
using Api.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
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
            var dto = _mapper.Map<IEnumerable<CityDto>>(governorates);
            return Ok(dto);
        }
        [HttpGet("getallcities")]
        public async Task<IActionResult> GetAllCity()
        {
            var cities = await _uow.Cities.GetAllAsync();
            if (cities == null)
                return NotFound("Not Found");
            var dto = _mapper.Map<IEnumerable<CityDto>>(cities);
            return Ok(dto);
        }
        [HttpGet("getallcitiesByGovernId{GovernId}")]
        public async Task<IActionResult> getallcitiesByGovernId(int GovernId)
        {
            var cities = await _uow.Cities.FindAllAsync(x=>x.GovernorateId== GovernId);
            if (cities == null)
                return NotFound("Not Found");
            var dto = _mapper.Map<IEnumerable<CityDto>>(cities);
            return Ok(dto);
        }
        [HttpGet("getBloodGroups")]
        public  IActionResult getBloodGroups()
        {
            var bloodGroups = new string[] { "A+", "A-", "B+", "B-", "O+", "O-", "AB+", "AB-" };
            return Ok(bloodGroups);
        }
    }
}
