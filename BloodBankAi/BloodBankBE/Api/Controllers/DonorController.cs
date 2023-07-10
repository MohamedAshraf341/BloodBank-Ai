using Api.Dto.User;
using Api.Helpers;
using Api.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Controllers
{
    [Authorize]

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
        public async Task<ApiResponse<IEnumerable<GetDonorsDto>>> GetAllUsers()
        {
            
            try
            {
                var users = await _uow.Users.GetAllAsync();
                if (users == null)
                    return new ApiResponse<IEnumerable<GetDonorsDto>> { Success=false,Message="Not Found Donor"};
                var dto = _mapper.Map<IEnumerable<GetDonorsDto>>(users);
                return new ApiResponse<IEnumerable< GetDonorsDto >> { Success = true,Message = "Donor List",Data= dto};
            }
            catch (Exception ex) 
            {
                return new ApiResponse<IEnumerable<GetDonorsDto>> { Success = false, Message = ex.Message};
            }            
        }
        [HttpGet("getdonorbyid/{id}")] 
        public async Task<ApiResponse<GetDonorById>> GetUserByID(string id)
        {
            try
            {
                var donorByAddresss = await _uow.Users.FindAsync(u => u.Id == id, new string[] { "Address" });
                if (donorByAddresss == null)
                    return new ApiResponse<GetDonorById> { Success=false, Message= "Not Found Donor" };
                var dto = _mapper.Map<GetDonorById>(donorByAddresss);
                return new ApiResponse<GetDonorById> { Success=true, Message="Donor By ID",Data= dto };
            }
            catch (Exception ex) 
            {
                return new ApiResponse<GetDonorById> { Success = false, Message = ex.Message };
            }

        }

    }
}
