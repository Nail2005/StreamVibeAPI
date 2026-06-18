using StreamVibeAPI.DTOs;
using StreamVibeAPI.Entities;

namespace StreamVibeAPI.Business.Abstract
{
    public interface IFaqService
    {
        Task<List<FaqDto>> TGetAllAsync();  
    }
}
