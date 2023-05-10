using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using AutoMapper;
using Api.Interfaces;
using Api.Dto.Idintity;
using Api.Data.Entities.Identity;
using Api.Data.Entities;
using Api.Dto;
using Api.Helpers;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        private new List<string> _allowedExtenstions = new List<string> { ".svg", ".webp", ".avif", ".apng", ".png", ".gif", ".jpg", ".jpeg", ".jfif", ".pjpeg", ".pjp" };
        public AccountController(IAuthService authService, IUnitOfWork uow, IMapper mapper )
        {
            _authService = authService;
            _uow = uow;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<ApiResponse<ResponseAuthDto>> RegisterAsync([FromBody] RegisterDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return new ApiResponse<ResponseAuthDto> { Success = false, Message = "Not Found" };

                var result = await _authService.RegisterAsync(model);

                if (!result.IsAuthenticated)
                    return new ApiResponse<ResponseAuthDto> { Success = false, Message = result.Message };


                SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);

                return new ApiResponse<ResponseAuthDto> { Success = true, Message = result.Message,Data=result };
            }
            catch (Exception ex) 
            {
                return new ApiResponse<ResponseAuthDto> { Success = false, Message = ex.Message };
            }
            
        }

        [HttpPost("login")]
        public async Task<ApiResponse<ResponseAuthDto>> GetTokenAsync([FromBody] LoginDto model)
        {
            try
            {
                var result = await _authService.GetTokenAsync(model);

                if (!result.IsAuthenticated)
                    return new ApiResponse<ResponseAuthDto> { Success = false, Message = result.Message };

                if (!string.IsNullOrEmpty(result.RefreshToken))
                    SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);

                return new ApiResponse<ResponseAuthDto> { Success = true, Message = result.Message, Data = result };
            }
            catch (Exception ex) 
            {
                return new ApiResponse<ResponseAuthDto> { Success = false, Message = ex.Message };
            }
            
        }

        [HttpPost("addRole")]
        public async Task<IActionResult> AddRoleAsync([FromBody] AddRoleDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.AddRoleAsync(model);

            if (!string.IsNullOrEmpty(result))
                return BadRequest(result);

            return Ok(model);
        }

        [HttpGet("refreshToken")]
        public async Task<ApiResponse<ResponseAuthDto>> RefreshToken()
        {
            try
            {
                var refreshToken = Request.Cookies["refreshToken"];

                var result = await _authService.RefreshTokenAsync(refreshToken);

                if (!result.IsAuthenticated)
                    return new ApiResponse<ResponseAuthDto> { Success = false, Message = result.Message,Data=result };

                SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);

                return new ApiResponse<ResponseAuthDto> { Success = true, Message = result.Message, Data = result };
            }
            catch (Exception ex)
            {
                return new ApiResponse<ResponseAuthDto> { Success = false, Message = ex.Message };
            }
            
        }

        [HttpPost("revokeToken")]
        public async Task<ApiResponse<string>> RevokeToken([FromBody] RevokeTokenDto model)
        {
            
            try
            {
                var token = model.Token ?? Request.Cookies["refreshToken"];

                if (string.IsNullOrEmpty(token))
                    return new ApiResponse<string> { Success = false, Message = "Token is required!" };

                var result = await _authService.RevokeTokenAsync(token);

                if (!result)
                    return new ApiResponse<string> { Success = false, Message = "Token is invalid!" };


                return new ApiResponse<string> { Success = true, Message = "User is LogOut!" };
            }
            catch (Exception ex)
            {
                return new ApiResponse<string> { Success = false, Message = ex.Message };
            }
        }

        private void SetRefreshTokenInCookie(string refreshToken, DateTime expires)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = expires.ToLocalTime()
            };

            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }

        [HttpGet("getProfilebyid/{UserId}")]
        public async Task<ActionResult> GetUserProfile(string UserId)
        {
            var userByAddress=await _uow.Users.FindAsync(a => a.Id== UserId,new string[] { "Address" });
            if(userByAddress == null)
                return BadRequest("Not Found");
            var userDto = _mapper.Map<UserDetailDto>(userByAddress);
            if (userDto == null)
                return BadRequest("Not Found");
            return Ok(userDto);
        }
        [HttpPut("updateProfile/{UserId}")]
        public async Task<ActionResult> UpdateUserProfile(string UserId, [FromForm] UserUpdateDto dto)
        {
            var user = await _uow.Users.FindAsync(u => u.Id == UserId,new string[] { "Address" });
            user = _mapper.Map<ApplicationUser>(dto);
            user.Address = _mapper.Map<Address>(dto);
            if (dto.Picture != null)
            {
                if (!_allowedExtenstions.Contains(Path.GetExtension(dto.Picture.FileName).ToLower()))
                    return BadRequest("Extension of image not found!");

                using var dataStream = new MemoryStream();

                await dto.Picture.CopyToAsync(dataStream);

                user.Picture = dataStream.ToArray();
            }
            var result =_uow.Users.Update(user);
            var change = _uow.Complete();
            return Ok(result);
        }
        [HttpPut("updateProfileimage/{UserId}")]
        public async Task<ActionResult> UpdateUserProfileImage(string UserId, [FromForm] PictureUpdateDto dto)
        {
            var user = await _uow.Users.GetByIdAsync(UserId);
            if (user == null)
                return BadRequest("Not Found");
            if(CheckImage(dto))
            {
                user.Picture = null;
            }
            else
            {
                using var dataStream = new MemoryStream();
                await dto.Picture.CopyToAsync(dataStream);
                user.Picture = dataStream.ToArray();
            }
            
            var result = _uow.Users.Update(user);
            _uow.Complete();
            return Ok("Success Change Image");

        }
        private bool CheckImage(PictureUpdateDto dto)
        {
            if(dto.RemovePicture|| dto.Picture == null|| !_allowedExtenstions.Contains(Path.GetExtension(dto.Picture.FileName).ToLower()))
                return true;
            return false;
        }

    }
}
