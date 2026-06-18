using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using StreamVibeAPI.DAL.Abstract;
using StreamVibeAPI.DAL.Context;
using StreamVibeAPI.Entities;

namespace StreamVibeAPI.DAL.Concrete
{
    public class GenreRepository : IGenreRepository
    {
        private readonly AppDbContext _context;

        public GenreRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Genre>> GetAllAsync()
        {
            return await _context.Genres.ToListAsync();
        }


        public async Task<List<Genre>> GetByTypeAsync(string type)
        {
            return await _context.Genres
                    .Where(x=>x.Type==type)
                    .ToListAsync();
        }
    }
}
