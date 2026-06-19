using StreamVibeAPI.DTOs;
using StreamVibeAPI.Entities;

namespace StreamVibeAPI.Business.Abstract
{
    public interface IContentService
    {
        Task<List<HeroDto>> TGetFeaturedAsync();
        Task<List<TopTenDto>> TGetTopTenAsync(string type);
        Task<List<ContentDto>> TGetFilteredAsync(string type, string filter, int limit);
    }
}
