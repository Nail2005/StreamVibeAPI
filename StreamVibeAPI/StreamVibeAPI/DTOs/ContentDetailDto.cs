namespace StreamVibeAPI.DTOs
{
    public class ContentDetailDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string PosterUrl { get; set; }

        public string BackgroundUrl { get; set; }

        public string TrailerUrl { get; set; }

        public string Type { get; set; }

        public int ReleaseYear { get; set; }

        public decimal? ImdbRating { get; set; }

        public decimal? StreamvibeRating { get; set; }

        public List<GenreDetailDto> Genres { get; set; }

        public List<string> Languages { get; set; }

        public List<CastDto> Cast { get; set; }

        public List<PersonDto> Directors { get; set; }

        public List<PersonDto> Music { get; set; }
    }
}
