using AutoMapper;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using Api.Interfaces;
using Api.Dto;
using Api.Helpers;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        public BankController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        [HttpGet("getallbanks")]
        public async Task<ApiResponse<List<BankWithBloodGroupsDto>>> GetAllBank()
        {
            try
            {
                var banks = await _uow.Banks.FindAllAsync(new string[] { "BloodGroups" });
                if (banks == null)
                    return new ApiResponse<List<BankWithBloodGroupsDto>> { Success = false, Message = "Not Found" };
                var json = JsonSerializer.Serialize(banks, new JsonSerializerOptions()
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles
                });
                var dto = JsonSerializer.Deserialize<List<BankWithBloodGroupsDto>>(json);
                return new ApiResponse<List<BankWithBloodGroupsDto>> { Success = true, Message = "List Of Banks", Data = dto };
            }
            catch (Exception ex) 
            {
                return new ApiResponse<List<BankWithBloodGroupsDto>> { Success = false, Message = ex.Message };
            }
        }
        [HttpGet("getbankbyid/{Id}")]
        public async Task<ApiResponse<BankByIdWithAddressDto>> GetBankByID(int Id)
        {
            try
            {
                var bank = await _uow.Banks.FindAsync(u => u.Id == Id, new string[] { "Address" });
                if (bank == null)
                    return new ApiResponse<BankByIdWithAddressDto> { Success = false, Message = "Not Found" };
                var json = JsonSerializer.Serialize(bank, new JsonSerializerOptions()
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles
                });
                var dto = JsonSerializer.Deserialize<BankByIdWithAddressDto>(json);
                return new ApiResponse<BankByIdWithAddressDto> { Success = true, Message = "Bank By Id", Data = dto };
            }
            catch (Exception ex) 
            {
                return new ApiResponse<BankByIdWithAddressDto> { Success = false, Message = ex.Message };
            }
        }
    }
}
