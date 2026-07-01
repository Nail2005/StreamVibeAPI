namespace StreamVibeAPI.DTOs.Auth
{
    public class UserSubscriptionPlanDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public decimal Price { get; set; }
    }
}
