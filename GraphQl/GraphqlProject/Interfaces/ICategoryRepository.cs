using GraphqlProject.Models;

namespace GraphqlProject.Interfaces
{
    public interface ICategoryRepository
    {
        List<Category> GetAllCategories();
        Category AddCategory(Category category);
        Category GetCategoryById(int id);
        //List<Category> GetFilteredCategory(int? minId, int? maxId);
        Category UpdateCategory(int categoryId, Category category);
        void DeleteCategory(int id);
    }
}
