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
            return await _context.PricingPlans.ToListAsync();
        }
    }
}
