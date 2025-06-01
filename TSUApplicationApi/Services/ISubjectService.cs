using TSUApplicationApi.DTOs;
using TSUApplicationApi.Entities;

namespace TSUApplicationApi.Services
{
    public interface ISubjectService
    {
        Task<SubjectDetailDto> GetByIdAsync(int id);
        Task AddReviewAsync(SubjectReview review);
        Task<bool> SubjectExistsAsync(int subjectId);
    }
}
