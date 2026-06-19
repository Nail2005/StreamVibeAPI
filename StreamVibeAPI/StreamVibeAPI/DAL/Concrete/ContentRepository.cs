using Microsoft.EntityFrameworkCore;
using StreamVibeAPI.DAL.Abstract;
using StreamVibeAPI.DAL.Context;
using StreamVibeAPI.Entities;

namespace StreamVibeAPI.DAL.Concrete
{
    public class ContentRepository : IContentRepository
    {
        private readonly AppDbContext _context;

        public ContentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Content?> GetByIdAsync(int id)
        {
            return await _context.Contents
                       .Include(c=>c.ContentGenres)
                           .ThenInclude(c=>c.Genre)
                       .Include(c=>c.ContentLanguages)
                       .Include(c=>c.ContentPeople)
                           .ThenInclude(c=>c.Person)
                       .FirstOrDefaultAsync(c=>c.Id == id);
        }

        public async Task<List<Content>> GetFeaturedAsync()
        {
            return await _context.Contents.Where(c => c.IsFeatured == true).ToListAsync();
        }

        public async Task<List<Content>> GetFilteredAsync(string type, string filter, int limit)
        {
            var query = _context.Contents.Where(c=>c.Type == type);

            switch (filter.ToLower())
            {
                case "trending": 
                    query = query.Where(c=> c.IsTrending == true);
                    break;

                case "new-release":
                    query = query.Where(c=>c.IsNewRelease == true);
                    break;      

                case "must-watch":
                    query = query.Where(c => c.IsMustWatch == true);
                    break;  
            }

            return await query.Take(limit).ToListAsync();  
        }

        public async Task<List<Review>> GetReviewsAsync(int contentId)
        {
            return await _context.Reviews
                        .Where(c => c.ContentId == contentId)
                        .OrderByDescending(c => c.CreatedAt)
                        .ToListAsync();
        }

        public async Task<List<Season>> GetSeasonsAsync(int contentId)
        {
            return await _context.Seasons
              .Include(x => x.Episodes)
              .Where(x => x.ContentId == contentId)
              .OrderBy(x => x.SeasonNumber)
              .ToListAsync(); ;
        }

        public async Task<List<Content>> GetTopTenAsync(string type)
        {
            return await _context.Contents
                                .Include(c => c.ContentGenres)
                                .ThenInclude(cg => cg.Genre)
                                .Where(c => c.Type == type && c.TopTenRank != null)
                                .OrderBy(c => c.TopTenRank)
                                .ToListAsync();
        }


    }
}
