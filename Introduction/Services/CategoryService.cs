﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Introduction.Models;
using Microsoft.EntityFrameworkCore;

namespace Introduction.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly NorthwindContext _dbContext;

        public CategoryService(NorthwindContext context)
        {
            _dbContext = context;
        }

        public void CreateCategory(Categories category)
        {
            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();
        }

        public List<Categories> GetAllCategories()
        {
            return _dbContext.Categories.ToList();
        }

        public Categories GetCategory(int id)
        {
            return _dbContext.Categories.FirstOrDefault(_ => _.CategoryID == id);
        }

        public void UpdateCategory(Categories category)
        {
            _dbContext.Attach(category).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
