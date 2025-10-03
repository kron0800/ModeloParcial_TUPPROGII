using TwitterCloneApi.Models;

namespace TwitterCloneApi.DTOs
{
    public class TweetDto
    {
        public int TweetId { get; set; }

        public UserDto User { get; set; }

        public string Content { get; set; }

        public DateTime PublishDatetime { get; set; }

        public static TweetDto EntityToDto(Tweet entity) => new TweetDto
        {
            TweetId = entity.Id,
            User = UserDto.EntityToDto(entity.IdUserNavigation),
            Content = entity.Content,
            PublishDatetime = entity.PublishDatetime
        };
    }
}
