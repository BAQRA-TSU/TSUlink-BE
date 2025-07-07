using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var result = await _service.GetByIdAsync(id);
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
                Name = $"{user.FirstName} {user.LastName}",
                Review = review.Text
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

        [Authorize(Roles = "Admin")]
        [HttpDelete("review/{reviewId}")]
        public async Task<IActionResult> DeleteLecturerReview(int reviewId)
        {
            var success = await _service.DeleteLecturerReviewAsync(reviewId);
            if (!success)
                return NotFound();

            return Ok("Review deleted.");
        }

    }
}
