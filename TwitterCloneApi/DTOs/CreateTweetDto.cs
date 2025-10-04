using TwitterCloneApi.Models;

namespace TwitterCloneApi.DTOs
{
    public class CreateTweetDto
    {
        public int UserId { get; set; }

        public string Content { get; set; } = string.Empty;

        public static Tweet DtoToEntity(CreateTweetDto dto) => new Tweet
        {
            IdUser = dto.UserId,
            Content = dto.Content,
            PublishDatetime = DateTime.Now,
            IsPublic = true
        };
    }
}
