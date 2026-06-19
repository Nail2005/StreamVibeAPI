using AutoMapper;
using StreamVibeAPI.Business.Abstract;
using StreamVibeAPI.DAL.Abstract;
using StreamVibeAPI.DTOs;

namespace StreamVibeAPI.Business.Concrete
{
    public class ContentManager : IContentService
    {
        private readonly IContentRepository _repository;
        private readonly IMapper _mapper;

        public ContentManager(IContentRepository repository, IMapper mapper)
        {
            _repository = repository;   
            _mapper = mapper;
        }

        public async Task<ContentDetailDto?> TGetDetailAsync(int id)
        {
            var value = await _repository.GetByIdAsync(id);

            if(value == null)
                return null;

            var result = new ContentDetailDto
            {
                Id = value.Id,
                Title = value.Title,
                Description = value.Description,
                PosterUrl = value.PosterUrl,
                BackgroundUrl = value.BackgroundUrl,
                TrailerUrl = value.TrailerUrl,
                Type = value.Type,
                ReleaseYear = value.ReleaseYear,
                ImdbRating = value.ImdbRating,
                StreamvibeRating = value.StreamvibeRating,

                Genres = value.ContentGenres.Select(x=> new GenreDetailDto
                {
                    Id = x.Genre.Id,
                    Name = x.Genre.Name,
                    Slug = x.Genre.Slug
                }).ToList(),

                Languages = value.ContentLanguages.Select(x => x.Language).ToList(),

                Cast = value.ContentPeople
                .Where(x => x.RoleType == "actor")
                .Select(x => new CastDto
                {
                    Id = x.Person.Id,
                    Name = x.Person.Name,
                    AvatarUrl = x.Person.AvatarUrl,
                    Nationality = x.Person.Nationality,
                    CharacterName = x.CharacterName
                })
                .ToList(),

                Directors = value.ContentPeople
                .Where(x => x.RoleType == "director")
                .Select(x => new PersonDto
                {
                    Id = x.Person.Id,
                    Name = x.Person.Name,
                    AvatarUrl = x.Person.AvatarUrl,
                    Nationality = x.Person.Nationality
                })
                .ToList(),

                Music = value.ContentPeople
                .Where(x => x.RoleType == "music")
                .Select(x => new PersonDto
                {
                    Id = x.Person.Id,
                    Name = x.Person.Name,
                    AvatarUrl = x.Person.AvatarUrl,
                    Nationality = x.Person.Nationality
                })
                .ToList()
            };

            return result;



        }

        public async Task<List<HeroDto>> TGetFeaturedAsync()
        {
            var data = await _repository.GetFeaturedAsync();
            return _mapper.Map<List<HeroDto>>(data);
        }

        public async Task<List<ContentDto>> TGetFilteredAsync(string type, string filter, int limit)
        {
            var data = await _repository.GetFilteredAsync(type, filter, limit);

            return _mapper.Map<List<ContentDto>>(data);
        }

        public async Task<List<ReviewDto>> TGetReviewsAsync(int id)
        {
            var data = await _repository.GetReviewsAsync(id); 

            return _mapper.Map<List<ReviewDto>>(data);
        }

        public async Task<List<SeasonDto>> TGetSeasonsAsync(int id)
        {
            var values = await _repository.GetSeasonsAsync(id);

            var result = values.Select(x => new SeasonDto
            {
                Id = x.Id,
                SeasonNumber = x.SeasonNumber,
                EpisodeCount = x.EpisodeCount,
                Title = x.Title,

                Episodes = x.Episodes
                        .OrderBy(e => e.EpisodeNumber)
                        .Select(e => new EpisodeDto
                        {
                            Id = e.Id,
                            EpisodeNumber = e.EpisodeNumber,
                            Title = e.Title,
                            Description = e.Description,
                            ThumbnailUrl = e.ThumbnailUrl,
                            DurationMinutes = e.DurationMinutes
                        }).ToList()
            }).ToList();

            return result;  
        }

        public async Task<List<TopTenDto>> TGetTopTenAsync(string type)
        {
            var data = await _repository.GetTopTenAsync(type);

            var result = data.Select(x=>new TopTenDto
            {
                Id = x.Id,
                Title = x.Title,
                PosterUrl = x.PosterUrl,
                TopTenRank = x.TopTenRank!.Value,
                Genres = x.ContentGenres.Select(cg => new ContentGenreDto
                {
                    Id = cg.Genre.Id,
                    Name = cg.Genre.Name
                }).ToList()
            }).ToList();

            return result;  
        }
    }
}
