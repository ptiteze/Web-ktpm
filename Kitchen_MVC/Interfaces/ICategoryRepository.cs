using Kitchen_MVC.DTO.Category;
using Kitchen_MVC.Models;

namespace Kitchen_MVC.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category> GetCategoryById(int id);

        Task<bool> CreateCategory(CreateCategoryRequest request);

        Task<bool> UpdateCategory(int id ,UpdateCategoryRequest request);

        Task<bool> DeleteCategory(int id);

        List<CategoryDTO> GetAllCategories();
    }
}
