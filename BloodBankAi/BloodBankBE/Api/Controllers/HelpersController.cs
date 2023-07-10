using Api.Dto;
using Api.Dto.Address;
using Api.Dto.Bank;
using Api.Dto.User;
using Api.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelpersController : ControllerBase
    {
        private readonly string AppDirectory = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName);
        
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
        [HttpGet("getgovernorate/{id}")]
        public async Task<IActionResult> getgovernorate(int id)
        {
            var governorates = await _uow.Governorates.GetByIdAsync(id);
            if (governorates == null)
                return NotFound("Not Found ");
            var dto = _mapper.Map<CityDto>(governorates);
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
        [HttpGet("getallcitiesByGovernId/{id}")]
        public async Task<IActionResult> getallcitiesByGovernId(int id)
        {
            var cities = await _uow.Cities.FindAllAsync(x=>x.GovernorateId==id);
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
        [HttpGet("getTypeModerators")]
        public IActionResult getTypeModerators()
        {
            var allItems = Enum.GetValues(typeof(AdminstrationBank));
            var items = new List<EnumItem>();
            foreach (var item in allItems)
            {
                items.Add(new EnumItem { Id = (int)item, Value = item.ToString() });
            }
            return Ok(items);
        }
        [HttpGet("getTypeAdmins")]
        public IActionResult getTypeAdmins()
        {
            var allItems = Enum.GetValues(typeof(Adminstration));
            var items = new List<EnumItem>();
            foreach (var item in allItems)
            {
                items.Add(new EnumItem { Id = (int)item, Value = item.ToString() });
            }
            return Ok(items);
        }
        [HttpGet("getNews")]
        public IActionResult getNews()
        {
            //D:\BloodBank\BloodBankAi\BloodBankBE\Api\Data\JsonData
            //D:\BloodBank\BloodBankAi\BackEnd\Api\Data\JsonData\DefualteNews.json
            var path = $"{AppDirectory}\\BloodBankBE\\Api\\Data\\JsonData\\DefualteNews.json";
            var jsonData = System.IO.File.ReadAllText(path);
            var items = JsonSerializer.Deserialize<GetNews>(jsonData);
            return Ok(items);
        }
    }
}
