using StreamVibeAPI.Entities;

namespace StreamVibeAPI.Business.Abstract
{
    public interface ITokenService
    {
        string GenerateAccessToken(User user);
        string GenerateRefreshToken();  
    }
}
