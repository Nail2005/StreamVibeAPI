using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StreamVibeAPI.Business.Abstract;
using StreamVibeAPI.DTOs;

namespace StreamVibeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _service;

        public GenresController(IGenreService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetGenres([FromQuery] string type = "all")
        {
            var genres = await _service.TGetAllAsync(type);

            var result = new ApiResponse<object>
                                          { 
                                            Success = true,
                                            Message = "Ok",
                                            Data = genres
                                          };

            return Ok(result); 

        }
    }
}
