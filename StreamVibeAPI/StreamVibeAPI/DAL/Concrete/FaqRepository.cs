using Microsoft.EntityFrameworkCore;
using StreamVibeAPI.DAL.Abstract;
using StreamVibeAPI.DAL.Context;
using StreamVibeAPI.Entities;

namespace StreamVibeAPI.DAL.Concrete
{
    public class FaqRepository : IFaqRepository
    {
        private readonly AppDbContext _context;

        public FaqRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Faq>> GetAllAsync()
        {
            return await _context.Faqs
                .OrderBy(x => x.OrderNumber)
                .ToListAsync();
        }
    }
}
