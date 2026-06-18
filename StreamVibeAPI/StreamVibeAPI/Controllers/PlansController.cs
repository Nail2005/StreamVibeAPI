using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StreamVibeAPI.Business.Abstract;
using StreamVibeAPI.DTOs;

namespace StreamVibeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlansController : ControllerBase
    {
        private readonly IPlanService _service;
        public PlansController(IPlanService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetPlans([FromQuery] string billing = "monthly")
        {
            var result = await _service.TGetAllAsync(billing);
            return Ok(result);
        }
    }
}
