using Api.Dto;
using Api.Helpers;
using Api.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Api.Data.Entities.Identity;
using Api.Data.Entities;
using Api.Dto.Idintity;
using Api.Dto.Bank;
using Api.Dto.User;
using Api.Dto.BloodGroupe;
using Api.Const;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers
{
    [Authorize(Roles = "BankAdmin,BankModerator")]

    [Route("api/BankAdmin")]
    [ApiController]
    public class BankAdminController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        private new List<string> _bloodGroups = new List<string> { "O-", "O+", "A-", "A+", "B-", "B+", "AB-", "AB+" };
        private new List<string> _allowedExtenstions = new List<string> { ".svg", ".webp", ".avif", ".apng", ".png", ".gif", ".jpg", ".jpeg", ".jfif", ".pjpeg", ".pjp" };
        public BankAdminController(IUnitOfWork uow, IMapper mapper, IAuthService authService)
        {
            _uow = uow;
            _mapper = mapper;
            _authService = authService;
        }
        [HttpGet("getallbanks/{userId}")]
        public ApiResponse<List<GetAllBanksInAdminDto>> GetAllBank(string userId)
        {
            try
            {
                var items=new List<GetAllBanksInAdminDto>();
                var user = _uow.Users.values().Where(m => m.Id == userId).Include(u => u.Moderates).ThenInclude(m => m.Bank).ThenInclude(b => b.Address).FirstOrDefault();
                if (user.Moderates == null)
                    return new ApiResponse<List<GetAllBanksInAdminDto>> { Message = "Not Found", Success = false };

                foreach (var moderator in user.Moderates)
                {
                    var json = JsonSerializer.Serialize(moderator.Bank, new JsonSerializerOptions()
                    {
                        ReferenceHandler = ReferenceHandler.IgnoreCycles
                    });
                    var dto = JsonSerializer.Deserialize<GetAllBanksInAdminDto>(json);
                    items.Add(dto);
                }
                return new ApiResponse<List<GetAllBanksInAdminDto>> { Data = items, Success = true ,Message="success fully"};
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<GetAllBanksInAdminDto>> { Message= ex.Message ,Success=false};
            }

        }
        [HttpGet("GetBankByID/{Id}")]
        public async Task<ApiResponse<GetBankByIdInAdmin>> GetBankByID(int Id)
        {
            try
            {
                var bank = await _uow.Banks.values().Where(b => b.Id == Id).Include(b => b.Address).Include(b => b.Moderators).ThenInclude(m => m.User).FirstOrDefaultAsync();
                if (bank == null)
                    return new ApiResponse<GetBankByIdInAdmin> { Success = false, Message = "Not Found Bank" };
                var json = JsonSerializer.Serialize(bank, new JsonSerializerOptions()
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles
                });
                var dto = JsonSerializer.Deserialize<GetBankByIdInAdmin>(json);
                return new ApiResponse<GetBankByIdInAdmin> { Success = true, Message = "Bank Data", Data = dto };
            }
            catch (Exception ex)
            {
                return new ApiResponse<GetBankByIdInAdmin> { Success = false, Message = ex.Message };
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
        [HttpGet("GetBloodData/{Id}")]
        public async Task<ApiResponse<BloodGroupUpdateDto>> GetBloodData(int Id)
        {
            try
            {
                var bankById = await _uow.Banks.FindAsync(u => u.Id == Id, new string[] { "BloodGroups" });
                if(bankById == null)
                    return new ApiResponse<BloodGroupUpdateDto> { Success = false, Message = "Not Found." };
                var json = JsonSerializer.Serialize(bankById.BloodGroups, new JsonSerializerOptions()
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles
                });
                var dto = JsonSerializer.Deserialize<ICollection<BloodGroupDto>>(json);
                var item = new BloodGroupUpdateDto() { BankId = bankById.Id, Groups = dto };
                return new ApiResponse<BloodGroupUpdateDto> {Data=item, Success = true, Message = "Success  ." };
            }
            catch (Exception ex)
            {
                return new ApiResponse<BloodGroupUpdateDto> { Success = false, Message = ex.Message };
            }

        }
        [HttpPut("UpdateBloodData")]
        public async Task<ApiResponse<BloodGroupUpdateDto>> UpdateBloodData(BloodGroupUpdateDto model)
        {
            try
            {
                if(model ==null)
                    return new ApiResponse<BloodGroupUpdateDto> { Success = false, Message = "Bad REquest." };
                var bankById = await _uow.Banks.FindAsync(u => u.Id == model.BankId, new string[] { "BloodGroups" });
                if (bankById == null)
                    return new ApiResponse<BloodGroupUpdateDto> { Success = false, Message = "Not Found." };
                if(model.Groups != null)
                {
                    foreach(var groupe in model.Groups)
                    {
                        var bloodGroupe = await _uow.BloodGroups.GetByIdAsync(groupe.Id);
                        if(bloodGroupe != null)
                        {
                            bloodGroupe.Value = groupe.Value;
                            _uow.BloodGroups.Update(bloodGroupe);
                            _uow.Complete();
                        }
                    }
                }
                return new ApiResponse<BloodGroupUpdateDto> { Data = model, Success = true, Message = "Success  ." };
            }
            catch (Exception ex)
            {
                return new ApiResponse<BloodGroupUpdateDto> { Success = false, Message = ex.Message };
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
