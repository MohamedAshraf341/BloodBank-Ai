using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Api.Interfaces;
using Api.Dto;
using Api.Data.Entities;
using Api.Const;
using Api.Dto.Idintity;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        private new List<string> _bloodGroups = new List<string> { "O-", "O+", "A-", "A+", "B-", "B+", "AB-", "AB+"};
        private new List<string> _allowedExtenstions = new List<string> { ".svg", ".webp", ".avif", ".apng", ".png", ".gif", ".jpg", ".jpeg", ".jfif", ".pjpeg", ".pjp" };
        public AdminController(IUnitOfWork uow, IMapper mapper, IAuthService authService)
        {
            _uow = uow;
            _mapper = mapper;
            _authService = authService;
        }
        [HttpGet("getallbanks")]
        public async Task<IActionResult> GetAllBank()
        {
            var banks = await _uow.Banks.FindAllAsync(new string[] { "Address"});
            if (banks == null)
                return NotFound("Not Found");
            var json = JsonSerializer.Serialize(banks, new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            });
            var dto = JsonSerializer.Deserialize<List<GetAllBanksInAdminDto>>(json);
            return Ok(dto);
        }
        [HttpGet("getbankbyid/{Id}")]
        public async Task<IActionResult> GetBankByID(int Id)
        {
            var bank =await _uow.Banks.values().Where(b => b.Id == Id).Include(b => b.Address).Include(b => b.Moderators).ThenInclude(m => m.User).FirstOrDefaultAsync();
            if (bank == null)
                return NotFound("Not Found");
            var json = JsonSerializer.Serialize(bank, new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            });
            var dto = JsonSerializer.Deserialize<GetBankByIdInAdmin>(json);
            return Ok(dto);
        }
        [HttpPut("updatebankb/{Id}")]
        public async Task<IActionResult> UpdateBank(int Id, [FromBody] UpdateBankInAdminDto Dto)
        {
            var bankById = await _uow.Banks.FindAsync(u => u.Id == Id, new string[] { "Address" });
            if (bankById == null)
                return NotFound("Not Found");
            bankById = _mapper.Map<Bank>(Dto);
            bankById.LastUpdated = DateTime.Now;
            bankById.Address=_mapper.Map<Address>(Dto);
            var bank = _uow.Banks.Update(bankById);            
            _uow.Complete();
            return Ok("Success Update");
        }
        [HttpPut("updatebankimage/{Id}")]
        public async Task<ActionResult> UpdateBankImage(int id, [FromForm] PictureUpdateDto dto)
        {
            var bank = await _uow.Banks.GetByIdAsync(id);
            if (bank == null)
                return BadRequest("Not Found");
            if (CheckImage(dto))
            {
                bank.Picture = null;
            }
            else
            {
                using var dataStream = new MemoryStream();
                await dto.Picture.CopyToAsync(dataStream);
                bank.Picture = dataStream.ToArray();
            }

            var result = _uow.Banks.Update(bank);
            _uow.Complete();
            return Ok("Success Change Image");

        }
        private bool CheckImage(PictureUpdateDto dto)
        {
            if (dto.RemovePicture || (!dto.RemovePicture && dto.Picture == null) || (!_allowedExtenstions.Contains(Path.GetExtension(dto.Picture.FileName).ToLower())))
                return true;
            return false;
        }
        [HttpPost("addbankb")]
        public async Task<IActionResult> AddBank([FromBody] AddBankInAdminDto Dto)
        {
            var user = await _uow.Users.FindAsync(u => u.UserName == Dto.UserName);
            if (user == null)
                return BadRequest("user Not Found");
            var bank = _mapper.Map<Bank>(Dto);
            bank.LastUpdated= DateTime.Now;
            var bloodGroups = new List<BloodGroup>();
            foreach(var i in _bloodGroups)
            {
                var bloodGroup = new BloodGroup {Group=i,Value=0 };
                bloodGroups.Add(bloodGroup);
            }
            var moderators = new List<Moderator>() { new Moderator { UserId = user.Id, Type = Roles.BankAdmin.ToString() } };
            bank.BloodGroups = bloodGroups;
            bank.Moderators= moderators;
            var bankdto=_uow.Banks.AddAsync(bank);
            var resultChange = _uow.Complete();
            if(resultChange>0)
            {
                var role = new AddRoleDto { Role = Roles.BankAdmin.ToString(), UserId = user.Id };
                var result = await _authService.AddRoleAsync(role);
                if (!string.IsNullOrEmpty(result))
                    return BadRequest(result);
            }                                    
            return Ok(bankdto);
        }
        [HttpPost("addmoderator")]
        public async Task<IActionResult> AddModerator([FromForm] AddBankModeratorsInAdminDto Dto)
        {
            var user =await _uow.Users.FindAsync(u => u.UserName == Dto.UserName);
            if (user == null)
                return NotFound("User Not Found");
            var role = Dto.Roles.ToString();
            var model = new AddRoleDto { UserId = user.Id, Role = role };
            var addrole =await _authService.AddRoleAsync(model);
            if (!string.IsNullOrEmpty(addrole))
                return BadRequest(addrole);
            var moderatorDto = new Moderator { UserId = user.Id, BankId = Dto.BankId, Type = role };
            var moderator = await _uow.Moderators.AddAsync(moderatorDto);
            var resultChange = _uow.Complete();
            return Ok("Succuess Add Moderator");
        }
        [HttpDelete("deletemoderator{IdModerator}")]
        public async Task<IActionResult> RemoveModerator(int IdModerator)
        {
            var moderator = await _uow.Moderators.GetByIdAsync(IdModerator);
            if (moderator == null)
                return NotFound("Not Found");            
            var roleDto = new AddRoleDto { UserId = moderator.UserId, Role = moderator.Type };
            var result = await _authService.RemoveRoleAsync(roleDto);
            _uow.Moderators.Delete(moderator);
            _uow.Complete();
            return Ok(result);
        }
    }
}
