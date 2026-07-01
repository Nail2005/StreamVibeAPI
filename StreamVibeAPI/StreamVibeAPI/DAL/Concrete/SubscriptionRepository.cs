using Microsoft.EntityFrameworkCore;
using StreamVibeAPI.DAL.Abstract;
using StreamVibeAPI.DAL.Context;
using StreamVibeAPI.Entities;

namespace StreamVibeAPI.DAL.Concrete
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly AppDbContext _context;

        public SubscriptionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(UserSubscription subscription)
        {
            await _context.UserSubscriptions.AddAsync(subscription);    
        }

        public async Task AddHistoryAsync(SubscriptionHistory history)
        {
            await _context.SubscriptionHistories.AddAsync(history);
        }

        public Task DeleteAsync(UserSubscription subscription)
        {
            _context.UserSubscriptions.Remove(subscription);  
            return Task.CompletedTask;      
        }

        public async Task<UserSubscription?> GetUserSubscriptionAsync(int userId)
        {
            var data = await _context.UserSubscriptions
                                                .Include(x=>x.Plan)
                                                .FirstOrDefaultAsync(x=>x.UserId == userId &&
                                                   (x.Status=="active" || x.Status=="trial"));
            return data;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
