using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using StreamVibeAPI.Business.Abstract;
using StreamVibeAPI.DTOs;
using StreamVibeAPI.DTOs.Auth;
using System.Reflection.Metadata.Ecma335;

namespace StreamVibeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _service;

        public AuthController(IUserService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto request)
        {
            var result = await _service.RegisterAsync(request);

            return StatusCode(201, new ApiResponse<AuthResponseDto>
            {
                Success = true,
                Message = "Registration successful",
                Data = result
            });
        }

        [HttpPost("login")] 
        public async Task<IActionResult> Login(LoginRequestDto request)
        {
            var result = await _service.LoginAsync(request);

            return Ok(new ApiResponse<AuthResponseDto>
            {
                Success = true,
                Message = "Login successful",
                Data = result
            });
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequestDto request)
        {
            var result = _service.RefreshTokenAsync(request);

            return Ok(new
            {
                Success = true,
                Data = result
            });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout(LogoutRequestDto request)
        {
            await _service.LogoutAsync(request);

            return Ok(new
            {
                Success = true,
                Message = "Logged out successfully"
            });
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> Me()
        {
            var userId = int.Parse(User.FindFirst("userId")!.Value);

            var result = await _service.MeAsync(userId);

            return Ok(new
            {
                Success = true,
                Data = new
                {
                    user = result 
                }
            });
        }

    }
}
