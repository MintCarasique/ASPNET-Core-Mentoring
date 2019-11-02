using Microsoft.AspNetCore.Mvc;
using Northwind.Models;
using Northwind.Services;

namespace Northwind.Controllers
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

        [HttpGet]
        public IActionResult Edit(int id) 
        {
            var model = _categoryService.GetCategory(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid && category != null)
            {
                _categoryService.UpdateCategory(category);
                return RedirectToAction("Index");
            }
            else
            {
                return View(category);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category) 
        {
            if (ModelState.IsValid && category != null)
            {
                _categoryService.CreateCategory(category);
            }
            else
            {
                return View(category);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Image(int id)
        {
            return File(_categoryService.GetCategoryImage(id), "image/bmp");
        }
    }
}