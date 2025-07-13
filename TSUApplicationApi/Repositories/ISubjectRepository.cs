using TSUApplicationApi.Entities;

namespace TSUApplicationApi.Repositories
{
    public interface ISubjectRepository
    {
        Task<Subject?> GetByIdWithDetailsAsync(int id);
        Task<List<SubjectFile>> GetFilesAsync(int subjectId);
        Task<Subject?> FindByIdAsync(int subjectId);
        Task AddReviewAsync(SubjectReview review);
        Task<User?> GetUserByIdAsync(Guid userId);
        Task AddFileAsync(SubjectFile file);
        Task<SubjectFile?> GetFileAsync(int fileId, int subjectId);
        Task<SubjectReview?> GetReviewByIdAsync(int reviewId);
        Task UpdateReviewAsync(SubjectReview review);
        Task<bool> DeleteReviewAsync(int reviewId);
    }
}
