using TSUApplicationApi.DTOs;

namespace TSUApplicationApi.Services
{
    public interface ILecturerService
    {
        Task<LecturerDetailDto> GetByIdAsync(int id);
    }
}
