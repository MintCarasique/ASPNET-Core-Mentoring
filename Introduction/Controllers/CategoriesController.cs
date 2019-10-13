using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Introduction.Models;
using Introduction.Services;
using Microsoft.AspNetCore.Mvc;

namespace Introduction.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            return View(_categoryService.GetAllCategories());
        }

        public IActionResult Edit(int id) 
        {
            return View(_categoryService.GetCategory(id));
        }

        [HttpPost]
        public IActionResult Edit(Categories categories)
        {
            return View("Index", _categoryService.GetAllCategories());
        }
    }
}