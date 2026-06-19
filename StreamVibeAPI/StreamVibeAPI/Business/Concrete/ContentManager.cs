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
