using Introduction.Models;
using System.Collections.Generic;

namespace Introduction.Services
{
    public interface ICategoryService
    {
        List<Category> GetAllCategories();

        Category GetCategory(int id);

        void UpdateCategory(Category category);

        void CreateCategory(Category category);
    }
}
