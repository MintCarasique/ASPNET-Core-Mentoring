using System.Collections.Generic;
using Northwind.Core.Models;

namespace Northwind.Core.Services
{
    public interface ICategoryService
    {
        /// <summary>
        /// Get all categories from database
        /// </summary>
        /// <returns>List of categories</returns>
        List<Category> GetAllCategories();

        /// <summary>
        /// Get certain category from database
        /// </summary>
        /// <param name="id">ID of category</param>
        /// <returns>Category object</returns>
        Category GetCategory(int id);

        /// <summary>
        /// Update certain category in database
        /// </summary>
        /// <param name="category">Updating category</param>
        void UpdateCategory(Category category);

        /// <summary>
        /// Create new category
        /// </summary>
        /// <param name="category">New category</param>
        void CreateCategory(Category category);

        byte[] GetCategoryImage(int id);
    }
}
