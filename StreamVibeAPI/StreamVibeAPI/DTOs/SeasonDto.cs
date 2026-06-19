namespace StreamVibeAPI.DTOs
{
    public class SeasonDto
    {
        public int Id { get; set; }

        public int SeasonNumber { get; set; }

        public int EpisodeCount { get; set; }

        public string Title { get; set; }

        public List<EpisodeDto> Episodes { get; set; }
    }
}
