using System.Collections.Generic;
using Northwind.Models;

namespace Northwind.Services
{
    public interface ICategoryService
    {
        List<Category> GetAllCategories();

        Category GetCategory(int id);

        void UpdateCategory(Category category);

        void CreateCategory(Category category);
    }
}
