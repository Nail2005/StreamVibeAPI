namespace StreamVibeAPI.DTOs
{
    public class TopTenDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string PosterUrl { get; set; }

        public int TopTenRank { get; set; }

        public List<ContentGenreDto> Genres { get; set; }
    }
}

