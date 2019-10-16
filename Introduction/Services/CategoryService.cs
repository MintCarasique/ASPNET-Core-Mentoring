using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Introduction.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Introduction.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly NorthwindContext _dbContext;

        private readonly ILogger _logger;

        public CategoryService(NorthwindContext context, ILogger<CategoryService> logger)
        {
            _dbContext = context;
            _logger = logger;
        }

        public void CreateCategory(Categories category)
        {
            try
            {
                if (IsValid(category))
                {
                    _logger.LogInformation("Adding new category to database");
                    _dbContext.Categories.Add(category);
                    _dbContext.SaveChanges();
                    _logger.LogInformation("Category added to database successfully");
                }
                else 
                {
                    _logger.LogError("Error adding new category to database! Model is invalid");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error adding new category to database! {ex.Message}");
            }
        }

        public List<Categories> GetAllCategories()
        {
            _logger.LogInformation("Getting all categories from database");
            return _dbContext.Categories.ToList();
        }

        public Categories GetCategory(int id)
        {
            return _dbContext.Categories.FirstOrDefault(_ => _.CategoryID == id);
        }

        public void UpdateCategory(Categories category)
        {
            _logger.LogInformation("Updating category in database");
            try
            {
                if (IsValid(category))
                {
                    _dbContext.Attach(category).State = EntityState.Modified;
                    _dbContext.SaveChanges();
                    _logger.LogInformation("Category updated successfully");
                }
                else 
                {
                    _logger.LogError("Error updating category in database. Model is invalid.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating record in database: {ex.Message}");
            }
            
        }

        private bool IsValid(Categories category) 
        {
            if (category == null)
                return false;
            if (category.CategoryName == null)
                return false;
            if (category.Description == null)
                return false;
            return true;
        }
    }
}
