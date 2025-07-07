using TSUApplicationApi.DTOs;
using TSUApplicationApi.Entities;

namespace TSUApplicationApi.Services
{
    public interface ILecturerService
    {
        Task<LecturerDetailDto> GetByIdAsync(int id);
        Task AddReviewAsync(LecturerReview review);
        Task<bool> LecturerExistsAsync(int lecturerId);
        Task<User?> GetUserByIdAsync(Guid userId);


        Task<LecturerReview?> GetLecturerReviewByIdAsync(int reviewId);
        Task UpdateLecturerReviewAsync(LecturerReview review);
        Task<bool> DeleteLecturerReviewAsync(int reviewId);

    }
}
