using TSUApplicationApi.Entities;

namespace TSUApplicationApi.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetCategoriesAsync();
    }
}
