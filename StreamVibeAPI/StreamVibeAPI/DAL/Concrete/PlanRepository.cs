using Microsoft.EntityFrameworkCore;
using StreamVibeAPI.DAL.Abstract;
using StreamVibeAPI.DAL.Context;
using StreamVibeAPI.Entities;
using System.Runtime.CompilerServices;

namespace StreamVibeAPI.DAL.Concrete
{
    public class PlanRepository : IPlanRepository
    {
        private readonly AppDbContext _context;

        public PlanRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<PricingPlan>> GetAllAsync()
        {
            return await _context.PricingPlans.Include(x=> x.PlanFeatures).ToListAsync();
        }

        public async Task<PricingPlan?> GetByIdAsync(int id)
        {
            return await _context.PricingPlans.FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
