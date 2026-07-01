namespace StreamVibeAPI.DTOs.Auth
{
    public class UserProfileSubscribeDto
    {
        public UserResponseDto User { get; set; } = null!;  
        public UserSubscriptionProfileDto? Subscription { get; set; }  
    }
}
