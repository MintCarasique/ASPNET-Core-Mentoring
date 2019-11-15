using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Northwind.Core.Models;

namespace Northwind.Core.Repositories
{
    public class CategoryRepository : IRepository<Category>
    {
        private readonly NorthwindContext _dbContext;

        private readonly ILogger _logger;

        public CategoryRepository(NorthwindContext dbContext, ILogger<CategoryRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public List<Category> GetAll()
        {
            _logger.LogInformation("Getting all categories from database");
            return _dbContext.Categories.ToList();
        }

        public Category Get(int id)
        {
            _logger.LogInformation($"Getting certain category with id = {id} from database");
            return _dbContext.Categories.FirstOrDefault(_ => _.CategoryID == id);
        }

        public void Update(Category category)
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
            catch (DbUpdateConcurrencyException)
            {
                _logger.LogError($"A concurrency violation is encountered while saving category '{category.CategoryID}' to the database.");
            }
            catch (DbUpdateException)
            {
                _logger.LogError($"An error is encountered while saving category '{category.CategoryID}' to the database.");
            }
        }

        public void Create(Category category)
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
            catch (DbUpdateConcurrencyException)
            {
                _logger.LogError($"A concurrency violation is encountered while saving category to the database.");
            }
            catch (DbUpdateException)
            {
                _logger.LogError($"An error is encountered while saving category to the database.");
            }
        }

        private bool IsValid(Category category)
        {
            if (category?.CategoryName == null)
                return false;
            if (category.Description == null)
                return false;
            return true;
        }
    }
}
