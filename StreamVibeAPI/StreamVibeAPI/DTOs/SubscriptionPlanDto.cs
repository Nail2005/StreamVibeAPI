namespace StreamVibeAPI.DTOs
{
    public class SubscriptionPlanDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public decimal Price { get; set; }

        public bool IsPopular { get; set; }

        public string BillingCycle { get; set; } = null!;

        public List<PlanFeatureDto> Features { get; set; } = [];
    }
}
