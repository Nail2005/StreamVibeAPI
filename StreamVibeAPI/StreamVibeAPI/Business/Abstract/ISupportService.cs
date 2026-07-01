using StreamVibeAPI.DTOs;

namespace StreamVibeAPI.Business.Abstract
{
    public interface ISupportService
    {
        Task<ContactMessageResponseDto> SendMessageAsync(ContactMessageRequestDto dto);

    }
}
