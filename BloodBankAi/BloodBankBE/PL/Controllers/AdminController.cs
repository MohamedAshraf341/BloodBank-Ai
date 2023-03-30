using AutoMapper;
using BL.Dtos;
using BL.Interfaces;
using DAL.Data.SeedData;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace PL.Controllers
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
            var dto = JsonSerializer.Deserialize<List<GetBankByIdDto>>(json);
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
            var dto = JsonSerializer.Deserialize<GetBankByIdWthAddressandModerators>(json);
            return Ok(dto);
        }
        [HttpPut("updatebankb/{Id}")]
        public async Task<IActionResult> UpdateBank(int Id, [FromForm] BankDto Dto)
        {
            var bankById = await _uow.Banks.FindAsync(u => u.Id == Id, new string[] { "Address" });
            if (bankById == null)
                return NotFound("Not Found");
            bankById = _mapper.Map<Bank>(Dto);
            bankById.LastUpdated = DateTime.Now;
            if (Dto.Picture != null)
            {
                if (!_allowedExtenstions.Contains(Path.GetExtension(Dto.Picture.FileName).ToLower()))
                    return BadRequest("Extension of image not found!");

                using var dataStream = new MemoryStream();

                await Dto.Picture.CopyToAsync(dataStream);

                bankById.Picture = dataStream.ToArray();
            }
            var bank = _uow.Banks.Update(bankById);
            var addressByIdBank = await _uow.Adresses.FindAsync(a => a.BankId == Id);
            addressByIdBank = _mapper.Map<Address>(Dto);
            var address= _uow.Adresses.Update(addressByIdBank);
            _uow.Complete();
            return Ok("Success Update");
        }
        [HttpPut("updatebankimage/{Id}")]
        public async Task<ActionResult> UpdateBankImage(int id, [FromForm] UpdatePictureDto dto)
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
        private bool CheckImage(UpdatePictureDto dto)
        {
            if (dto.RemovePicture || (!dto.RemovePicture && dto.Picture == null) || (!_allowedExtenstions.Contains(Path.GetExtension(dto.Picture.FileName).ToLower())))
                return true;
            return false;
        }
        [HttpPost("addbankb")]
        public async Task<IActionResult> AddBank([FromForm] BankDto Dto)
        {
            var user = await _uow.Users.FindAsync(u => u.UserName == Dto.UserName);
            if (user == null)
                return BadRequest("user Not Found");
            var bankDto = _mapper.Map<Bank>(Dto);
            bankDto.LastUpdated= DateTime.Now;
            if (Dto.Picture != null)
            {
                if (!_allowedExtenstions.Contains(Path.GetExtension(Dto.Picture.FileName).ToLower()))
                    return BadRequest("Extension of image not found!");

                using var dataStream = new MemoryStream();

                await Dto.Picture.CopyToAsync(dataStream);

                bankDto.Picture = dataStream.ToArray();
            }
            var bank =await _uow.Banks.AddAsync(bankDto);
            foreach(var i in _bloodGroups)
            {
                var bloodGroup = new BloodGroup {Group=i,BankId=bank.Id,Value=0 };
                _uow.BloodGroups.Add(bloodGroup);
            }
            var AddressDto = _mapper.Map<Address>(Dto);
            var address=await _uow.Adresses.AddAsync(AddressDto);
            var moderatorDto = new Moderator { UserId = user.Id, BankId = bank.Id, Type = Roles.BankAdmin.ToString() };
            var moderator= await _uow.Moderators.AddAsync(moderatorDto);
            var resultChange= _uow.Complete();
            var role = new AddRoleDto { Role = Roles.BankAdmin.ToString(), UserId = user.Id };
            var result = await _authService.AddRoleAsync(role);
            if (!string.IsNullOrEmpty(result))
                return BadRequest(result);                        
            return Ok("Success Add Bank");
        }
        [HttpPost("addmoderator")]
        public async Task<IActionResult> AddModerator([FromForm] BankModeratorEnumDto Dto)
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
        [HttpDelete("deletemoderator")]
        public async Task<IActionResult> RemoveModerator([FromForm] RemoveModeratorBank model)
        {
            var moderator = await _uow.Moderators.FindAsync(m => m.BankId == model.IdBank && m.UserId == model.IdUser);
            if (moderator == null)
                return NotFound("Not Found");
            _uow.Moderators.Delete(moderator);
            _uow.Complete();
            var roleDto = new AddRoleDto { UserId = model.IdUser, Role = model.Roles.ToString() };
            var result = await _authService.RemoveRoleAsync(roleDto);
            return Ok(result);
        }
    }
}
