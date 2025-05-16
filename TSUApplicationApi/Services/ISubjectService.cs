using TSUApplicationApi.DTOs;

namespace TSUApplicationApi.Services
{
    public interface ISubjectService
    {
        Task<SubjectDetailDto> GetByShortNameAsync(string shortName);
    }
}
