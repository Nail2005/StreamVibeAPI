namespace StreamVibeAPI.DTOs
{
    public class ReviewDto
    {
        public int Id { get; set; }

        public string ReviewerName { get; set; }

        public string ReviewerLocation { get; set; }

        public decimal Rating { get; set; }

        public string ReviewText { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
