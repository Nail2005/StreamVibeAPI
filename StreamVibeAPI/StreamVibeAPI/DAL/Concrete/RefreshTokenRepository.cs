using Microsoft.EntityFrameworkCore;
using StreamVibeAPI.DAL.Abstract;
using StreamVibeAPI.DAL.Context;
using StreamVibeAPI.Entities;

namespace StreamVibeAPI.DAL.Concrete
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AppDbContext _context;

        public RefreshTokenRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(RefreshToken token)
        {
             await _context.RefreshTokens.AddAsync(token);
        }

        public async Task DeleteAsync(RefreshToken token)
        {
             _context.RefreshTokens.Remove(token);
        }

        public async Task DeleteUserTokensAsync(int userId)
        {
            var tokens = await _context.RefreshTokens.Where(x=>x.UserId == userId).ToListAsync();

            _context.RefreshTokens.RemoveRange(tokens); 
        }

        public async Task<RefreshToken?> GetByTokenAsync(string token)
        {
            return await _context.RefreshTokens.FirstOrDefaultAsync(x=>x.Token == token);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
