using TSUApplicationApi.DTOs;

namespace TSUApplicationApi.Services
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetCategoriesAsync();
    }
}
