using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TSUApplicationApi.DTOs;
using TSUApplicationApi.Entities;
using TSUApplicationApi.Services;

namespace TSUApplicationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FeedController : ControllerBase
    {
        private readonly IFeedService _service;

        public FeedController(IFeedService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] CreateFeedPostDto dto)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;

            if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out var parsedUserId))
                return Unauthorized();

            var user = await _service.GetUserByIdAsync(parsedUserId);
            if (user == null)
                return Unauthorized();

            var post = new FeedPost
            {
                Content = dto.Content,
                UserId = parsedUserId,
                CreatedAt = DateTime.UtcNow
            };

            await _service.AddPostAsync(post);

            var feedPostDto = new FeedPostDto
            {
                Name = $"{user.FirstName} {user.LastName}",
                Text = post.Content,
            };
            return Ok(feedPostDto);
            //return Ok("Post created successfully");
        }

        [HttpGet("with-comments")]
        public async Task<IActionResult> GetPostsWithComments([FromQuery] int offset = 0, [FromQuery] int limit = 10)
        {
            var posts = await _service.GetPostsWithCommentsAsync(offset, limit);
            return Ok(posts);
        }

        [HttpPost("{postId}/comments")]
        public async Task<IActionResult> AddComment(int postId, [FromBody] CreateCommentDto dto)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;

            if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out var parsedUserId))
                return Unauthorized();

            var user = await _service.GetUserByIdAsync(parsedUserId);
            if (user == null)
                return Unauthorized();

            //await _service.AddCommentAsync(postId, parsedUserId, dto.Text);

            var comment = await _service.AddCommentAsync(postId, parsedUserId, dto.Text);
            return Ok(new FeedCommentDto
            {
                Name = comment.User.FirstName + " " + comment.User.LastName,
                Text = comment.Text
            });
        }
    }
}
