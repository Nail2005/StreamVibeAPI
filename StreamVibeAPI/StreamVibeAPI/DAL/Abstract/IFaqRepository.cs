using StreamVibeAPI.Entities;

namespace StreamVibeAPI.DAL.Abstract
{
    public interface IFaqRepository
    {
        Task<List<Faq>> GetAllAsync();
        
    }
}
