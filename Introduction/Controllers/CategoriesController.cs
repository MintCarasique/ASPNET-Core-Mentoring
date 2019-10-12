using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Introduction.Models;
using Microsoft.AspNetCore.Mvc;

namespace Introduction.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly NorthwindContext _dbContext;

        public CategoriesController(NorthwindContext context)
        {
            _dbContext = context;
        }

        public IActionResult Index()
        {
            var categoriesList = _dbContext.categories.ToList();

            return View(categoriesList);
        }
    }
}