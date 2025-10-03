using TwitterCloneApi.DTOs;
using TwitterCloneApi.Models;

namespace TwitterCloneApi
{
    public interface ITweetService
    {
        Task<IEnumerable<TweetDto>> GetTweetsByFiltersAsync(int? userId, string? country);
        Task<bool> UpdateTweetAsync(int tweetId, string content);
        Task<bool> DeleteTweetAsync(int tweetId);
    }
}
