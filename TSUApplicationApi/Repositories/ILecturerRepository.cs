using TSUApplicationApi.Entities;

namespace TSUApplicationApi.Repositories
{
    public interface ILecturerRepository
    {
        Task<Lecturer?> GetLecturerWithDetailsAsync(int id);
        Task<bool> LecturerExistsAsync(int id);
        Task AddReviewAsync(LecturerReview review);
        Task<LecturerReview?> GetReviewByIdAsync(int reviewId);
        Task UpdateReviewAsync(LecturerReview review);
        Task<bool> DeleteReviewAsync(int reviewId);
        Task<User?> GetUserByIdAsync(Guid userId);
    }
}
