using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StreamVibeAPI.Business.Abstract;
using StreamVibeAPI.DTOs;
using StreamVibeAPI.Entities;
using System;

namespace StreamVibeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SubscriptionsController : ControllerBase
    {
        private readonly ISubscribeService _service;

        public SubscriptionsController(ISubscribeService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        [HttpGet("plans")]
        public async Task<IActionResult> GetPlans([FromQuery] string blling = "monthly")
        {
            var result = await _service.GetPlansAsync(blling);

            return Ok(new
            {
                Success = true,
                Data = result
            });
        }

        [Authorize]
        [HttpPost("subscribe")]
        public async Task<IActionResult> Subscribe(SubscripeRequestDto request)
        {
            var userId = int.Parse(User.FindFirst("userId")!.Value);

            var result = await _service.SubscribeAsync(userId, request);

            return StatusCode(201, new
            {
                Success = true,
                Message = "Subscription activated successfully",
                Data = result   
            });

        }

        [Authorize]
        [HttpGet("my")]
        public async Task<IActionResult> MySubscribe()
        {
            var userId = int.Parse(User.FindFirst("userId")!.Value);

            var result = await _service.GetMySubscriptionAsync(userId);

            return Ok(new
            { 
                Success = true,
                Data = result
            });

        }

        [Authorize]
        [HttpDelete("cancel")]
        public async Task<IActionResult> Cancel()
        {
            var userId = int.Parse(User.FindFirst("userId")!.Value);

            await _service.CancelSubscriptionAsync(userId);   

            return Ok(new
            {
                Success = true,
                Message = "Subscription cancelled successfully"
            }); 
        }
    }
}
