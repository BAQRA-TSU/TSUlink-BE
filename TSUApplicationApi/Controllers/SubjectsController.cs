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
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectService _service;

        public SubjectsController(ISubjectService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubject(int id)
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
        public async Task<IActionResult> PostReview([FromBody] CreateSubjectReviewDto dto)
        {
            
            var userId = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;

            if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out var parsedUserId))
                return Unauthorized();

            var lecturerExists = await _service.SubjectExistsAsync(dto.SubjectId);
            if (!lecturerExists)
                return NotFound("Lecturer not found");

            var user = await _service.GetUserByIdAsync(parsedUserId);
            if (user == null)
                return Unauthorized();

            var review = new SubjectReview
            {
                SubjectId = dto.SubjectId,
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

        //[HttpPost("{subjectId}/upload")]
        //public async Task<IActionResult> UploadFile(int subjectId, IFormFile file)
        //{
        //    var result = await _service.UploadFileAsync(subjectId, file);

        //    if (!result.Success)
        //        return BadRequest(result.Message);

        //    return Ok(new { result.FileName });
        //}

        [HttpPost("{subjectId}/upload-db")]
        public async Task<IActionResult> UploadFileToDb(int subjectId, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var (success, message, uploadedFile) = await _service.UploadFileAsync(subjectId, file);
            if (!success)
                return BadRequest(message);

            return Ok(uploadedFile); 
        }
        [HttpGet("{subjectId}/files/{fileId}")]
        public async Task<IActionResult> DownloadFileFromDb(int subjectId, int fileId)
        {
            var file = await _service.DownloadFileAsync(fileId, subjectId);
            if (file == null)
                return NotFound("File not found.");

            return File(file.FileContent, file.ContentType, file.FileName);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("review/{reviewId}/approve")]
        public async Task<IActionResult> ApproveSubjectReview(int reviewId)
        {
            var review = await _service.GetSubjectReviewByIdAsync(reviewId);
            if (review == null)
                return NotFound();

            review.IsApproved = true;
            await _service.UpdateSubjectReviewAsync(review);

            return Ok("Review approved.");
        }

        //[Authorize(Roles = "Admin")]
        //[HttpDelete("review/{reviewId}")]
        //public async Task<IActionResult> DeleteSubjectReview(int reviewId)
        //{
        //    var success = await _service.DeleteSubjectReviewAsync(reviewId);
        //    if (!success)
        //        return NotFound();

        //    return Ok("Review deleted.");
        //}

        [HttpDelete("review/{reviewId}")]
        public async Task<IActionResult> DeleteSubjectReview(int reviewId)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var parsedUserId))
                return Unauthorized();

            var review = await _service.GetSubjectReviewByIdAsync(reviewId);
            if (review == null)
                return NotFound();

            var isAdmin = User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Admin");

            if (!isAdmin && review.UserId != parsedUserId)
                return Forbid("You can only delete your own review.");

            var success = await _service.DeleteSubjectReviewAsync(reviewId);
            if (!success)
                return NotFound();

            return Ok("Review deleted.");
        }



    }
}
