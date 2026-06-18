namespace StreamVibeAPI.DTOs
{
    public class PlanDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public bool IsPopular { get; set; }
    }
}
