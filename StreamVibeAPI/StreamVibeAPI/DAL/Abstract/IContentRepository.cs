using StreamVibeAPI.Entities;

namespace StreamVibeAPI.DAL.Abstract
{
    public interface IContentRepository
    {
        Task<List<Content>> GetFeaturedAsync();
        Task<List<Content>> GetTopTenAsync(string type);
        Task<List<Content>> GetFilteredAsync(string type, string filter, int limit);
        Task<Content?> GetByIdAsync(int id);
        Task<List<Season>> GetSeasonsAsync(int contentId);
        Task<List<Review>> GetReviewsAsync(int contentId);
    }
}
