using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Introduction.Models;

namespace Introduction.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly NorthwindContext _dbContext;

        public CategoryService(NorthwindContext context)
        {
            _dbContext = context;
        }

        public List<Categories> GetAllCategories()
        {
            return _dbContext.Categories.ToList();
        }

        public Categories GetCategory()
        {
            throw new NotImplementedException();
        }
    }
}
