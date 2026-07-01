namespace StreamVibeAPI.DTOs
{
    public class MySubscriptionDto
    {
        public int Id { get; set; }

        public string BillingCycle { get; set; } = null!;

        public bool IsTrial { get; set; }

        public string Status { get; set; } = null!;

        public DateTime StartedAt { get; set; }

        public DateTime ExpiresAt { get; set; }

        public SubscriptionPlanInfoDto Plan { get; set; } = null!;
    }
}
