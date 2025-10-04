using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TwitterCloneApi.DTOs;
using TwitterCloneApi.Models;

namespace TwitterCloneApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class TweetsController: ControllerBase
    {
        private readonly ITweetService _service;

        public TweetsController(ITweetService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] int? userId,
            [FromQuery] string? country,
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate )
        {
            try
            {
                IEnumerable<TweetDto> tweetsDto = await _service.GetTweetsByFiltersAsync(userId, country, startDate, endDate);
                return Ok(new { Status = "success", Data = tweetsDto });   
            }
            catch (Exception ex)
            {
                return ServerError(ex.Message);
                throw;
            }
        }

        [HttpPut("{tweetId}")]
        public async Task<IActionResult> Update(int tweetId, [FromQuery] string content)
        {
            try
            {
                bool result = await _service.UpdateTweetAsync(tweetId, content);
                if (result)
                {
                    return Ok(new { Status = "success", Message = $"Tweet with ID '{tweetId}' was updated successfully." });
                } else
                {
                    return BadRequest(new { Status = "error", Message = $"Unable to update Tweet with ID '{tweetId}'." });
                }
            }
            catch (Exception ex)
            {
                return ServerError(ex.Message);
                throw;
            }
        }

        [HttpDelete("{tweetId}")]
        public async Task<IActionResult> Delete(int tweetId)
        {
            try
            {
                bool result = await _service.DeleteTweetAsync(tweetId);
                if (result)
                {
                    return Ok(new { Status = "success", Message = $"Tweet with ID '{tweetId}' was deleted successfully." });
                }
                else
                {
                    return BadRequest(new { Status = "error", Message = $"Unable to delete Tweet with ID '{tweetId}'." });
                }
            }
            catch (Exception ex)
            {
                return ServerError(ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTweetDto dto)
        {
            try
            {
                TweetDto tweetDto = await _service.CreateTweet(dto);
                return Ok(new { Status = "success", Data = tweetDto });
            }
            catch (Exception ex)
            {
                return ServerError(ex.Message);
                throw;
            }
        }

        // Method to return server errors
        private ObjectResult ServerError(string? msg = "Internal server error") =>
            StatusCode(StatusCodes.Status500InternalServerError, new { Status = "error", Message = msg });
    }
}
