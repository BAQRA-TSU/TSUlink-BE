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
            var result = await _service.GetByIdAsync(id);
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
                Name = $"{user.FirstName} {user.LastName}",
                Review = review.Text
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

    }
}
