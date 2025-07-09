using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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
                Id = post.Id,
                Name = $"{user.FirstName} {user.LastName}",
                Text = post.Content,
            };
            return Ok(feedPostDto);
            //return Ok("Post created successfully");
        }

        [HttpGet("with-comments")]
        public async Task<IActionResult> GetPostsWithComments([FromQuery] int offset = 0, [FromQuery] int limit = 10)
        {
            var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value; //added
            var userIdStr = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            Guid.TryParse(userIdStr, out var userId);

            var posts = await _service.GetPostsWithCommentsAsync(offset, limit, role, userId);
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
         //------------------------------//

        [Authorize(Roles = "Admin")]
        [HttpPost("{postId}/approve")]
        public async Task<IActionResult> ApprovePost(int postId)
        {
            var post = await _service.GetPostByIdAsync(postId);
            if (post == null) return NotFound();

            post.IsApproved = true;
            await _service.UpdatePostAsync(post);
            return Ok("Post approved.");
        }

        //[Authorize(Roles = "Admin")]
        //[HttpDelete("{postId}")]
        //public async Task<IActionResult> DeletePost(int postId)
        //{
        //    var success = await _service.DeletePostAsync(postId);
        //    if (!success) return NotFound();

        //    return Ok("Post deleted.");
        //}

        [HttpDelete("{postId}")]
        public async Task<IActionResult> DeletePost(int postId)
        {
            var userIdStr = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            if (string.IsNullOrEmpty(userIdStr) || !Guid.TryParse(userIdStr, out var userId))
                return Unauthorized();

            var post = await _service.GetPostByIdAsync(postId);
            if (post == null)
                return NotFound();

            var isAdmin = User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Admin");

            if (!isAdmin && post.UserId != userId)
                return Forbid("You can only delete your own post.");

            var success = await _service.DeletePostAsync(postId);
            if (!success)
                return NotFound();

            return Ok("Post deleted.");
        }



        //[Authorize(Roles = "Admin")]
        //[HttpPost("comments/{commentId}/approve")]
        //public async Task<IActionResult> ApproveComment(int commentId)
        //{
        //    var comment = await _service.GetCommentByIdAsync(commentId);
        //    if (comment == null) return NotFound();

        //    comment.IsApproved = true;
        //    await _service.UpdateCommentAsync(comment);
        //    return Ok("Comment approved.");
        //}

        //[Authorize(Roles = "Admin")]
        //[HttpDelete("comments/{commentId}")]
        //public async Task<IActionResult> DeleteComment(int commentId)
        //{
        //    var success = await _service.DeleteCommentAsync(commentId);
        //    if (!success) return NotFound();

        //    return Ok("Comment deleted.");
        //}

        [HttpDelete("comments/{commentId}")]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            var userIdStr = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            if (string.IsNullOrEmpty(userIdStr) || !Guid.TryParse(userIdStr, out var userId))
                return Unauthorized();

            var comment = await _service.GetCommentByIdAsync(commentId);
            if (comment == null)
                return NotFound();

            var isAdmin = User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Admin");

            if (!isAdmin && comment.UserId != userId)
                return Forbid("You can only delete your own comment.");

            var success = await _service.DeleteCommentAsync(commentId);
            if (!success)
                return NotFound();

            return Ok("Comment deleted.");
        }




    }
}
