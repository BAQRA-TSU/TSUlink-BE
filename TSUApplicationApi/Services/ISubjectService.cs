using TSUApplicationApi.DTOs;
using TSUApplicationApi.Entities;

namespace TSUApplicationApi.Services
{
    public interface ISubjectService
    {
        Task<SubjectDetailDto> GetByIdAsync(int id);
        Task AddReviewAsync(SubjectReview review);
        Task<bool> SubjectExistsAsync(int subjectId);
        Task<User?> GetUserByIdAsync(Guid userId);
        //Task<(bool Success, string Message, string FileName)> UploadFileAsync(int subjectId, IFormFile file);
        Task<(bool Success, string Message, FileDto? File)> UploadFileAsync(int subjectId, IFormFile file);

        Task<SubjectFile?> DownloadFileAsync(int fileId, int subjectId);

        Task<SubjectReview?> GetSubjectReviewByIdAsync(int reviewId);
        Task UpdateSubjectReviewAsync(SubjectReview review);
        Task<bool> DeleteSubjectReviewAsync(int reviewId);

    }
}
