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
using Api.Helpers;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Api.Dto.Bank;
using Api.Dto.User;

namespace Api.Controllers
{
    [Authorize(Roles = "Admin,Moderator")]

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
        public async Task<ApiResponse<List<GetAllBanksInAdminDto>>> GetAllBank()
        {
            try
            {
                var banks = await _uow.Banks.FindAllAsync(new string[] { "Address" });
                if (banks == null)
                    return new ApiResponse<List<GetAllBanksInAdminDto>> { Success = false, Message = "Not Found Banks" };
                var json = JsonSerializer.Serialize(banks, new JsonSerializerOptions()
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles
                });
                var dto = JsonSerializer.Deserialize<List<GetAllBanksInAdminDto>>(json);
                return new ApiResponse<List<GetAllBanksInAdminDto>> { Success = true, Message = "List Of Banks ", Data = dto };
            }
            catch (Exception ex) 
            {
                return new ApiResponse<List<GetAllBanksInAdminDto>> { Success = false, Message = ex.Message };
            }
            
        }
        [HttpGet("getbankbyid/{Id}")]
        public async Task<ApiResponse<GetBankByIdInAdmin>> GetBankByID(int Id)
        {
            try
            {
                var bank = await _uow.Banks.values().Where(b => b.Id == Id).Include(b => b.Address).Include(b => b.Moderators).ThenInclude(m => m.User).FirstOrDefaultAsync();
                if (bank == null)
                    return new ApiResponse<GetBankByIdInAdmin> { Success = false, Message = "Not Found Bank"};
                var json = JsonSerializer.Serialize(bank, new JsonSerializerOptions()
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles
                });
                var dto = JsonSerializer.Deserialize<GetBankByIdInAdmin>(json);
                return new ApiResponse<GetBankByIdInAdmin> { Success = true, Message = "Bank Data",Data= dto };
            }
            catch (Exception ex) 
            {
                return new ApiResponse<GetBankByIdInAdmin> { Success = false, Message = ex.Message };
            }

        }
        [HttpPut("updatebankb/{Id}")]
        public async Task<ApiResponse<string>> UpdateBank(int Id, [FromBody] UpdateBankInAdminDto Dto)
        {
            try
            {
                var bankById = await _uow.Banks.FindAsync(u => u.Id == Id, new string[] { "Address" });
                if (bankById == null)
                    return new ApiResponse<string> { Success = false, Message = "Not Found Bank" };
                bankById.Name = Dto.Name;
                bankById.PhoneNumber = Dto.PhoneNumber;
                bankById.Email = Dto.Email;
                bankById.Website=Dto.Website;
                bankById.Address.Government = Dto.Government;
                bankById.Address.City = Dto.City;
                bankById.Address.Area= Dto.Area;
                bankById.LastUpdated = DateTime.Now;
                var bank = _uow.Banks.Update(bankById);
                var change=_uow.Complete();
                return new ApiResponse<string> { Success = true, Message = "Success Update Data Bank ." };
            }
            catch (Exception ex) 
            {
                return new ApiResponse<string> { Success = false, Message = ex.Message };
            }
            
        }
        [HttpPut("updatebankimage/{Id}")]
        public async Task<ApiResponse<string>> UpdateBankImage(int id, [FromForm] PictureUpdateDto dto)
        {
            try
            {
                var bank = await _uow.Banks.GetByIdAsync(id);
                if (bank == null)
                    return new ApiResponse<string> { Success = false, Message = "Not Found Bank" };
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
                return new ApiResponse<string> { Success = true, Message = "Success Update Image Of Bank ." };
            }
            catch (Exception ex) 
            {
                return new ApiResponse<string> { Success = false, Message = ex.Message };
            }


        }
        private bool CheckImage(PictureUpdateDto dto)
        {
            if (dto.RemovePicture || (!dto.RemovePicture && dto.Picture == null) || (!_allowedExtenstions.Contains(Path.GetExtension(dto.Picture.FileName).ToLower())))
                return true;
            return false;
        }
        [HttpPost("addbankb")]
        public async Task<ApiResponse<AddBankInAdminDto>> AddBank([FromBody] AddBankInAdminDto Dto)
        {
            try
            {
                var user = await _uow.Users.FindAsync(u => u.UserName == Dto.UserName);
                if (user == null)
                    return new ApiResponse<AddBankInAdminDto> { Success = false, Message = "Not Found User" };
                var bank = _mapper.Map<Bank>(Dto);
                bank.LastUpdated = DateTime.Now;
                var bloodGroups = new List<BloodGroup>();
                foreach (var i in _bloodGroups)
                {
                    var bloodGroup = new BloodGroup { Group = i, Value = 0 };
                    bloodGroups.Add(bloodGroup);
                }
                var moderators = new List<Moderator>() { new Moderator { UserId = user.Id, Type = Roles.BankAdmin.ToString() } };
                bank.BloodGroups = bloodGroups;
                bank.Moderators = moderators;
                var bankdto = _uow.Banks.AddAsync(bank);
                var resultChange = _uow.Complete();
                if (resultChange > 0)
                {
                    var role = new AddRoleDto { Role = Roles.BankAdmin.ToString(), UserId = user.Id };
                    var result = await _authService.AddRoleAsync(role);
                    if (!string.IsNullOrEmpty(result))
                        return new ApiResponse<AddBankInAdminDto> { Success = false, Message = result, };
                }
                return new ApiResponse<AddBankInAdminDto> { Success = true, Message = "Success Add Bank .",Data= Dto };
            }
            catch (Exception ex) 
            {
                return new ApiResponse<AddBankInAdminDto> { Success = false, Message = ex.Message };
            }

        }
        [HttpPost("addmoderator")]
        public async Task<ApiResponse<string>> AddModerator([FromBody] AddBankModeratorsInAdminDto Dto)
        {
            try
            {
                var user = await _uow.Users.FindAsync(u => u.UserName == Dto.UserName);
                if (user == null)
                    return new ApiResponse<string> { Success = false, Message = "Not Found User" };
                var role = Dto.Roles.ToString();
                var model = new AddRoleDto { UserId = user.Id, Role = role };
                var addrole = await _authService.AddRoleAsync(model);
                if (!string.IsNullOrEmpty(addrole))
                    return new ApiResponse<string> { Success = false, Message = addrole };
                var moderatorDto = new Moderator { UserId = user.Id, BankId = Dto.BankId, Type = role };
                var moderator = await _uow.Moderators.AddAsync(moderatorDto);
                var resultChange = _uow.Complete();
                return new ApiResponse<string> { Success = true, Message = "Succuess Add Moderator ." };
            }
            catch (Exception ex) 
            {
                return new ApiResponse<string> { Success = false, Message = ex.Message };
            }
            
        }
        [HttpDelete("deletemoderator/{IdModerator}")]
        public async Task<ApiResponse<string>> RemoveModerator(int IdModerator)
        {
            try
            {
                var moderator = await _uow.Moderators.GetByIdAsync(IdModerator);
                if (moderator == null)
                    return new ApiResponse<string> { Success = false, Message = "Not Found Moderator" };
                var roleDto = new AddRoleDto { UserId = moderator.UserId, Role = moderator.Type };
                var result = await _authService.RemoveRoleAsync(roleDto);
                _uow.Moderators.Delete(moderator);
                _uow.Complete();
                return new ApiResponse<string> { Success = true, Message = "Succuess Remove Moderator ." };
            }
            catch (Exception ex)
            {
                return new ApiResponse<string> { Success = false, Message = ex.Message };
            }
            
        }
    }
}
