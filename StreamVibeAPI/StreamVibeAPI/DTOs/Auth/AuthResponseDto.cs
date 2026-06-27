namespace StreamVibeAPI.DTOs.Auth
{
    public class AuthResponseDto
    {
        public UserResponseDto User { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }    
        public int ExpiresIn { get; set; }   
    }
}
