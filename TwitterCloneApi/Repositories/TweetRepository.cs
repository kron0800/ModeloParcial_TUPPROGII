using Microsoft.EntityFrameworkCore;
using TwitterCloneApi.Models;

namespace TwitterCloneApi.Repositories
{
    public class TweetRepository : ITweetRepository
    {
        private readonly TwitterCloneContext _context;

        public TweetRepository(TwitterCloneContext context)
        {
            _context = context;
        }

        public IQueryable<Tweet> GetQueryable() // return query to make filters on service
        {
            return _context.Tweets.AsQueryable();
        }

        public async Task<IEnumerable<Tweet>> GetByFiltersAsync(IQueryable<Tweet> query)
        {
            return await query.Include(t => t.IdUserNavigation).ThenInclude(u => u.IdCountryNavigation).AsNoTracking().ToListAsync();
        }

        public async Task<Tweet?> GetByIdAsync(int id)
        {
            return await _context.Tweets.Include(t => t.IdUserNavigation).ThenInclude(u => u.IdCountryNavigation).FirstOrDefaultAsync(t  => t.Id == id);
        }

        public async Task<bool> UpdateAsync(Tweet entity)
        {
            if (entity == null) { throw new Exception("Unable to update given Tweet."); }
            int rowsAffected = await _context.SaveChangesAsync();
            return rowsAffected > 0;
        }
    }
}
