using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StreamVibeAPI.Business.Abstract;
using StreamVibeAPI.DTOs;

namespace StreamVibeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly IDeviceService _service;

        public DevicesController(IDeviceService service)
        {
            _service = service;
        }

        [HttpGet]   
        public async Task<IActionResult> GetDevices()
        {
            var devices = await _service.TGetAllAsync();
            var result = new ApiResponse<object>
            {
                Success = true,
                Message = "Ok",
                Data = devices
            };
            return Ok(result);
        }
    }
}
