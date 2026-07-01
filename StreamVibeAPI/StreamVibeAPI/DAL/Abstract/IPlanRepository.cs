using StreamVibeAPI.Entities;

namespace StreamVibeAPI.DAL.Abstract
{
    public interface IPlanRepository
    {
        Task<List<PricingPlan>> GetAllAsync();  
        Task<PricingPlan?> GetByIdAsync(int id); 
        Task SaveChangesAsync();    
    }

}
