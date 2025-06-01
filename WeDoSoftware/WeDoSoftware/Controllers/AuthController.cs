using Microsoft.AspNetCore.Mvc;
using WeDoSoftware.Application.DTOs.UserDTOs;
using WeDoSoftware.Application.ServiceInterfaces;

namespace WeDoSoftware.WebApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public AuthController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _userService.ValidateUserAsync(dto.Email, dto.Password);
            if (user == null)
                return Unauthorized("Invalid email or password.");

            var token = _tokenService.GenerateToken(user);
            return Ok(new { Token = token });
        }

    }
}
