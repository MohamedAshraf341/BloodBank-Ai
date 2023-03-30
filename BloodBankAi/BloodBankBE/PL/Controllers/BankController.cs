using AutoMapper;
using BL.Dtos;
using BL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using System;

namespace PL.Controllers
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
        public async Task<IActionResult> GetAllBank()
        {
            var banks = await _uow.Banks.FindAllAsync(new string[] { "BloodGroups" });
            if (banks == null)
                return NotFound("Not Found");
            var json = JsonSerializer.Serialize(banks, new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            });
            var dto = JsonSerializer.Deserialize<List<BankBlooGoupDto>>(json);
            return Ok(dto);
        }
        [HttpGet("getbankbyid/{Id}")]
        public async Task<IActionResult> GetBankByID(int Id)
        {
            var bank = await _uow.Banks.FindAsync(u => u.Id==Id, new string[] { "Address" });
            if (bank == null)
                return NotFound("Not Found");
            var json = JsonSerializer.Serialize(bank, new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            });
            var dto = JsonSerializer.Deserialize<GetBankByIdDto>(json);
            return Ok(dto);
        }
    }
}
