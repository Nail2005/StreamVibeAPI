using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StreamVibeAPI.Business.Abstract;
using StreamVibeAPI.DTOs;
using System.Security.Policy;

namespace StreamVibeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupportController : ControllerBase
    {
        private readonly ISupportService _service;
        public SupportController(ISupportService service)
        {
            _service = service;
        }

        [HttpPost("contact")]
        public async Task<IActionResult> Contact(ContactMessageRequestDto dto)
        {
            var result = await _service.SendMessageAsync(dto);

            return StatusCode(201, new
            {
                Success = true,
                Message = "Your message has been sent successfully",
                Data = result
            });
        }
    }
}
