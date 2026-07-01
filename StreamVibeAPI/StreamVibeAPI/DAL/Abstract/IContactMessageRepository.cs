using StreamVibeAPI.Entities;

namespace StreamVibeAPI.DAL.Abstract
{
    public interface IContactMessageRepository
    {
        Task AddAsync(ContactMessage message);
        Task SaveChangesAsync();
    }
}
