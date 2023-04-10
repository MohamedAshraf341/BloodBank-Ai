using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using AutoMapper;
using Api.Interfaces;
using Api.Dto.Idintity;
using Api.Data.Entities.Identity;
using Api.Data.Entities;
using Api.Dto;

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
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RegisterAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> GetTokenAsync([FromBody] LoginDto model)
        {
            var result = await _authService.GetTokenAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            if (!string.IsNullOrEmpty(result.RefreshToken))
                SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);

            return Ok(result);
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
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            var result = await _authService.RefreshTokenAsync(refreshToken);

            if (!result.IsAuthenticated)
                return BadRequest(result);

            SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);

            return Ok(result);
        }

        [HttpPost("revokeToken")]
        public async Task<IActionResult> RevokeToken([FromBody] RevokeTokenDto model)
        {
            var token = model.Token ?? Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
                return BadRequest("Token is required!");

            var result = await _authService.RevokeTokenAsync(token);

            if (!result)
                return BadRequest("Token is invalid!");

            return Ok();
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
