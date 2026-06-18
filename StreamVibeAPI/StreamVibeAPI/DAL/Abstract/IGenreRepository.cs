using StreamVibeAPI.Entities;

namespace StreamVibeAPI.DAL.Abstract
{
    public interface IGenreRepository
    {
        Task<List<Genre>> GetAllAsync();
        Task<List<Genre>> GetByTypeAsync(string type);
    }
}
