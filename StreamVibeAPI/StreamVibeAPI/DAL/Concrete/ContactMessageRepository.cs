using StreamVibeAPI.DAL.Abstract;
using StreamVibeAPI.DAL.Context;
using StreamVibeAPI.Entities;

namespace StreamVibeAPI.DAL.Concrete
{
    public class ContactMessageRepository : IContactMessageRepository
    {
        private readonly AppDbContext _context;

        public ContactMessageRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ContactMessage message)
        {
            await _context.ContactMessages.AddAsync(message);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();  
        }
    }
}
