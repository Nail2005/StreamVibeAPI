using StreamVibeAPI.Entities;

namespace StreamVibeAPI.DAL.Abstract
{
    public interface IRefreshTokenRepository
    {
        Task AddAsync(RefreshToken token);
        Task DeleteUserTokensAsync(int userId);
        Task<RefreshToken?> GetByTokenAsync(string token);
        Task DeleteAsync(RefreshToken token);
        Task SaveChangesAsync();    
    }
}
