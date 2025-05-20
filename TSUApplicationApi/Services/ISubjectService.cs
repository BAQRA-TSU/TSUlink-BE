using TSUApplicationApi.DTOs;
using TSUApplicationApi.Entities;

namespace TSUApplicationApi.Services
{
    public interface ISubjectService
    {
        Task<SubjectDetailDto> GetByShortNameAsync(string shortName);
        Task AddReviewAsync(SubjectReview review);
        Task<bool> SubjectExistsAsync(int subjectId);
    }
}
