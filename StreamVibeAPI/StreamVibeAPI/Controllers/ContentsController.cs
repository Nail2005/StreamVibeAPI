using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StreamVibeAPI.Business.Abstract;
using StreamVibeAPI.DTOs;

namespace StreamVibeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentsController : ControllerBase
    {
        private readonly IContentService _contentservice;
        private readonly IGenreService _genreService;

        public ContentsController(IContentService contentservice, IGenreService genreService)
        {
            _contentservice = contentservice;
            _genreService = genreService;
        }

        [HttpGet("hero")]
        public async Task<IActionResult> GetHero()
        {
            var data = await _contentservice.TGetFeaturedAsync();
            var result = new ApiResponse<object>
            {
                Success = true,
                Message = "Ok",
                Data = data
            };

            return Ok(result);
        }

        [HttpGet("genres")]
        public async Task<IActionResult> GetGenres([FromQuery] string? type)
        {
            if (string.IsNullOrWhiteSpace(type))
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Type parameter is required.",
                    Data = null
                });

            }

            var data = await _genreService.TGetAllAsync(type);
            var result = new ApiResponse<object>
            {
                Success = true,
                Message = "Ok",
                Data = data
            };

            return Ok(result);
        }

        [HttpGet("top-ten")]
        public async Task<IActionResult> GetTopTen([FromQuery] string? type)
        {
            if (string.IsNullOrWhiteSpace(type))
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Type parameter is required.",
                    Data = null
                });
            }
            var data = await _contentservice.TGetTopTenAsync(type);
            var result = new ApiResponse<object>
            {
                Success = true,
                Message = "Ok",
                Data = data
            };
            return Ok(result);

        }

        [HttpGet("filtered")]
        public async Task<IActionResult> GetContent([FromQuery] string? type, [FromQuery] string? filter, [FromQuery] int limit = 10)
        {
            if (string.IsNullOrWhiteSpace(type))
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Type parameter is required.",
                    Data = null
                });
            }

            if (string.IsNullOrWhiteSpace(filter))
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Filter parameter is required.",
                    Data = null
                });
            }

            var data = await _contentservice.TGetFilteredAsync(type, filter, limit);

            var result = new ApiResponse<object>
            {
                Success = true,
                Message = "Ok",
                Data = data
            };

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContentDetail(string id)
        {
            if (!int.TryParse(id, out int contentId))
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Id must be numeric",
                    Data = null
                });
            }

            var data = await _contentservice.TGetDetailAsync(contentId);

            if (data == null)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Content not found",
                    Data = null
                });
            }

            var result = new ApiResponse<object>
            {
                Success = true,
                Message = "Ok",
                Data = data
            };

            return Ok(result);
        }

        [HttpGet("{id}/seasons")]
        public async Task<IActionResult> GetSeasons(string id)
        {
            if (!int.TryParse(id, out int contentId))
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Id must be numeric",
                    Data = null
                });
            }

            var detail = await _contentservice.TGetDetailAsync(contentId);

            if (detail == null)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Content not found",
                    Data = null
                });
            }

            var values = await _contentservice.TGetSeasonsAsync(contentId);

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "OK",
                Data = values
            });
        }

        [HttpGet("{id}/reviews")]
        public async Task<IActionResult> GetReviews(string id)
        {
            if (!int.TryParse(id, out int contentId))
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Id must be numeric",
                    Data = null
                });
            }

            var detail = await _contentservice.TGetDetailAsync(contentId);

            if (detail == null)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Content not found",
                    Data = null
                });
            }

            var values = await _contentservice.TGetReviewsAsync(contentId);

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "OK",
                Data = values
            });
        }
    }
}
