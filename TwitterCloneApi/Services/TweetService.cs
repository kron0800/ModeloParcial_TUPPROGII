using TwitterCloneApi.DTOs;
using TwitterCloneApi.Models;
using TwitterCloneApi.Repositories;

namespace TwitterCloneApi.Services
{
    public class TweetService : ITweetService
    {
        private readonly ITweetRepository _repository;

        public TweetService(ITweetRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TweetDto>> GetTweetsByFiltersAsync(int? userId, string? country)
        {
            // Get the default linq query
            IQueryable<Tweet> query = _repository.GetQueryable().Where(t => t.IsPublic == true);
            
            // Filters using IQueryable
            // If userId is not null and is greater than 0, add WHERE idUser == @userId
            if (userId != null && userId > 0)
            {
                query = query.Where(t => t.IdUser == userId);
            }
            // If country is not null or whitespace, add WHERE country like @country
            if (!string.IsNullOrWhiteSpace(country))
            {
                query = query.Where(t => t.IdUserNavigation.IdCountryNavigation.Country1.Contains(country));
            }

            // Get a Ienumerable of tweets (model)
            IEnumerable<Tweet> tweets = await _repository.GetByFiltersAsync(query); 
            // Map entities (tweets) to dtos (tweetdto). 
            return tweets.Select(TweetDto.EntityToDto);
        }

        public async Task<bool> UpdateTweetAsync(int tweetId, string content)
        {
            Tweet? target = await _repository.GetByIdAsync(tweetId);
            // check if a tweet with given id exists
            if (target == null) { throw new Exception($"Unable to find Tweet with ID '{tweetId}'."); }
            // check if content is null or whitespace
            if (string.IsNullOrWhiteSpace(content)) { throw new Exception("Content can't be empty."); }
            // check if tweet is not public
            if (!target.IsPublic) { throw new Exception($"Tweet with ID '{tweetId}' is not public. You can only update public tweets."); }
            
            DateTime now = DateTime.Now;
            // check if the diff between now and the publish datetime in days is more than 7
            if ((now - target.PublishDatetime).Days > 7 ) { throw new Exception("You can only update tweets that were tweeted less than a week ago."); }
            
            target.Content = content;
            target.PublishDatetime = now;

            return await _repository.UpdateAsync(target);
        }
        public async Task<bool> DeleteTweetAsync(int tweetId)
        {
            Tweet? target = await _repository.GetByIdAsync(tweetId);
            // check if a tweet with given id exists
            if (target == null) { throw new Exception($"Unable to find Tweet with ID '{tweetId}'."); }
            // check if tweet is not public
            if (!target.IsPublic) { throw new Exception($"Tweet with ID '{tweetId}' is not public. You can only delete public tweets."); }

            target.IsPublic = false;
            return await _repository.UpdateAsync(target);
        }
    }
}
