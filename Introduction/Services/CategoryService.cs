using System.Collections.Generic;
using Northwind.Models;
using Northwind.Repositories;

namespace Northwind.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;

        public CategoryService(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public void CreateCategory(Category category)
        {
            _categoryRepository.Create(category);
        }

        public List<Category> GetAllCategories()
        {
            return _categoryRepository.GetAll();
        }

        public Category GetCategory(int id)
        {
            return _categoryRepository.Get(id);
        }

        public void UpdateCategory(Category category)
        {
            _categoryRepository.Update(category);
        }
    }
}
