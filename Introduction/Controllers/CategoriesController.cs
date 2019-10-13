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

        public CategoriesController(ICategoryService categoriesService)
        {
            _categoryService = categoriesService;
        }

        public IActionResult Index()
        {
            return View(_categoryService.GetAllCategories());
        }
    }
}