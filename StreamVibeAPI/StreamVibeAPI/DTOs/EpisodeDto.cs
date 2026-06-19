namespace StreamVibeAPI.DTOs
{
    public class EpisodeDto
    {
        public int Id { get; set; }

        public int EpisodeNumber { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ThumbnailUrl { get; set; }

        public int DurationMinutes { get; set; }
    }
}
