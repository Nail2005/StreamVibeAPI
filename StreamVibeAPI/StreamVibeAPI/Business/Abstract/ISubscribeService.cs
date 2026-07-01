using StreamVibeAPI.DTOs;

namespace StreamVibeAPI.Business.Abstract
{
    public interface ISubscribeService
    {
        Task<List<SubscriptionPlanDto>> GetPlansAsync(string billing);
        Task<MySubscriptionDto> SubscribeAsync(int userId, SubscripeRequestDto request); 
        Task<MySubscriptionDto> GetMySubscriptionAsync(int userId);       
        Task CancelSubscriptionAsync(int userId);
    }
}
