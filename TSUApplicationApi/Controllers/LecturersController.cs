using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TSUApplicationApi.Data;
using TSUApplicationApi.DTOs;
using TSUApplicationApi.Entities;
using TSUApplicationApi.Services;

namespace TSUApplicationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LecturersController : ControllerBase
    {
        private readonly ILecturerService _service;

        public LecturersController(ILecturerService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLecturer(int id)
        {
            var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            var userIdStr = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            Guid.TryParse(userIdStr, out var userId);

            var result = await _service.GetByIdAsync(id, role, userId);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost("review")]
        public async Task<IActionResult> PostReview([FromBody] CreateLecturerReviewDto dto)
        {
            // მომხმარებლის ID მიღება JWT-დან
            var userId = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;

            if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out var parsedUserId))
                return Unauthorized();

            var lecturerExists = await _service.LecturerExistsAsync(dto.LecturerId);
            if (!lecturerExists)
                return NotFound("Lecturer not found");

            var user = await _service.GetUserByIdAsync(parsedUserId);
            if (user == null)
                return Unauthorized();

            var review = new LecturerReview
            {
                LecturerId = dto.LecturerId,
                UserId = parsedUserId,
                Text = dto.Text
            };

            await _service.AddReviewAsync(review);

            var reviewDto = new ReviewDto
            {
                Id = review.Id,
                Name = $"{user.FirstName} {user.LastName}",
                Review = review.Text,
                CanDelete = true,
                Status = "pending"
            };

            return Ok(reviewDto);
            //return Ok("Review saved successfully");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("review/{reviewId}/approve")]
        public async Task<IActionResult> ApproveLecturerReview(int reviewId)
        {
            var review = await _service.GetLecturerReviewByIdAsync(reviewId);
            if (review == null)
                return NotFound();

            review.IsApproved = true;
            await _service.UpdateLecturerReviewAsync(review);

            return Ok("Review approved.");
        }

        //[Authorize(Roles = "Admin")]
        //[HttpDelete("review/{reviewId}")]
        //public async Task<IActionResult> DeleteLecturerReview(int reviewId)
        //{
        //    var success = await _service.DeleteLecturerReviewAsync(reviewId);
        //    if (!success)
        //        return NotFound();

        //    return Ok("Review deleted.");
        //}

        [HttpDelete("review/{reviewId}")]
        public async Task<IActionResult> DeleteLecturerReview(int reviewId)
        {
            var userIdStr = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            if (string.IsNullOrEmpty(userIdStr) || !Guid.TryParse(userIdStr, out var userId))
                return Unauthorized();

            var review = await _service.GetLecturerReviewByIdAsync(reviewId);
            if (review == null)
                return NotFound();

            var isAdmin = User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Admin");

            if (!isAdmin && review.UserId != userId)
                return Forbid("You can only delete your own review.");

            var success = await _service.DeleteLecturerReviewAsync(reviewId);
            if (!success)
                return NotFound();

            return Ok("Review deleted.");
        }

    }
}
