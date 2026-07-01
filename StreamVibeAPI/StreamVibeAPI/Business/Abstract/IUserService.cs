using StreamVibeAPI.DTOs.Auth;

namespace StreamVibeAPI.Business.Abstract
{
    public interface IUserService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request);

        Task<AuthResponseDto> LoginAsync(LoginRequestDto request);

        Task<RefreshTokenResponseDto> RefreshTokenAsync(RefreshTokenRequestDto request);

        Task LogoutAsync(LogoutRequestDto request);

        Task<UserResponseDto> MeAsync(int userId);  

        Task<UserProfileSubscribeDto> GetProfileAsync(int userId);  

    }
}
