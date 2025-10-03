using TwitterCloneApi.Models;

namespace TwitterCloneApi.DTOs
{
    public class UserDto
    {
        public int UserId { get; set; }

        public string Username { get; set; }

        public string Country { get; set; }

        public static UserDto EntityToDto(User entity) => new UserDto
        {
            UserId = entity.Id,
            Username = entity.Username,
            Country = entity.IdCountryNavigation.Country1 ?? string.Empty
        };
    }
}
