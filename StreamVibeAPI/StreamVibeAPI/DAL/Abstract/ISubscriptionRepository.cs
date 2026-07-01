using StreamVibeAPI.Entities;

namespace StreamVibeAPI.DAL.Abstract
{
    public interface ISubscriptionRepository
    {
        Task<UserSubscription?> GetUserSubscriptionAsync(int userId);   
        Task AddAsync(UserSubscription subscription);
        Task DeleteAsync(UserSubscription subscription);    
        Task AddHistoryAsync(SubscriptionHistory history);  
        Task SaveChangesAsync();
    }
}
