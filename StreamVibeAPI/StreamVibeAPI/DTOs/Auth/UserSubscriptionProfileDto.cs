namespace StreamVibeAPI.DTOs.Auth
{
    public class UserSubscriptionProfileDto
    {
        public int Id { get; set; }

        public string BillingCycle { get; set; } = null!;

        public bool IsTrial { get; set; }

        public string Status { get; set; } = null!;

        public DateTime StartedAt { get; set; }

        public DateTime ExpiresAt { get; set; }

        public UserSubscriptionPlanDto Plan { get; set; } = null!;
    }
}
