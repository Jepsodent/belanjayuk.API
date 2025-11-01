using belanjayuk.API.Models.DTO;
using belanjayuk.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace belanjayuk.API.Controllers
{
    [Route("api/seller")]
    [ApiController]
    [Authorize]
    public class SellerController : ControllerBase
    {
        private readonly IJwtService _jwtService;
        private readonly IAuthService _authservice;

        public SellerController(IJwtService jwtService, IAuthService authService)
        {
            _jwtService = jwtService;
            _authservice = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterSeller([FromBody] RegisterSellerRequestDto request)
        {
            var userId = User.FindFirst("ID")?.Value;
            if(string.IsNullOrWhiteSpace(userId))
            {
                return Unauthorized(new APIResponseDto<UserResponseDto>
                {
                    IsSuccess = false,
                    Message = "User ID not found in token.",
                    Data = null
                });
            }

            var result = await _authservice.RegisterSeller(userId, request);

            if (result.IsSuccess) 
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginSeller([FromBody] LoginRequestDto request)
        {
            var result = await _authservice.LoginSeller(request);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
