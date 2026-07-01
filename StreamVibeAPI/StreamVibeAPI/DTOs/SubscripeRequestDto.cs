namespace StreamVibeAPI.DTOs
{
    public class SubscripeRequestDto
    {
        public int PlanId { get; set; }
        public string BillingCycle { get; set; } = null!;
        public bool IsTrial { get; set; }
    }
}
