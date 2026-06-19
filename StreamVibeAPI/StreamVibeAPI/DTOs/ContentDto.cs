namespace StreamVibeAPI.DTOs
{
    public class ContentDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string PosterUrl { get; set; }

        public decimal? ImdbRating { get; set; }

        public decimal? StreamvibeRating { get; set; }

        public int ReleaseYear { get; set; }

        public string Type { get; set; }
    }
}
