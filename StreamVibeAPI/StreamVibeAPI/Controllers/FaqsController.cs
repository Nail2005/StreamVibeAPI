using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StreamVibeAPI.Business.Abstract;
using StreamVibeAPI.DTOs;

namespace StreamVibeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaqsController : ControllerBase
    {
        private readonly IFaqService _service;

        public FaqsController(IFaqService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetFaqs()
        {
            var faqs = await _service.TGetAllAsync();

            var result = new ApiResponse<object>
            {
                Success = true,
                Message = "Ok",
                Data = faqs
            };

            return Ok(result);

        }
    }


}
