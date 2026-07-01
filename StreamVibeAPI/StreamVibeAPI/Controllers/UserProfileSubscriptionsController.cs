using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StreamVibeAPI.Business.Abstract;

namespace StreamVibeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileSubscriptionsController : ControllerBase
    {
        private readonly IUserService _service;
        public UserProfileSubscriptionsController(IUserService service)
        {
            _service = service;
        }

        [HttpGet("profile")]
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            int userId = int.Parse(User.FindFirst("userId")!.Value);

            var result = await _service.GetProfileAsync(userId);

            return Ok(new
            {
                Success = true,
                Data = result
            });

        }
    }
}
