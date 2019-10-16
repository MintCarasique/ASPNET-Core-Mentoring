using Introduction.Models;
using Introduction.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Introduction.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        private readonly ICategoryService _categoryService;

        private readonly ISupplierService _supplierService;

        private readonly ILogger<ProductsController> _logger;

        public ProductsController(
            IProductService productService, 
            ICategoryService categoryService, 
            ISupplierService supplierService,
            ILogger<ProductsController> logger)
        {
            _productService = productService;
            _categoryService = categoryService;
            _supplierService = supplierService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("log test");
            return View(_productService.GetAllProducts());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = _productService.GetProduct(id);
            if (model == null) 
            {
                return RedirectToAction("Index");
            }

            List<SelectListItem> categoryListItems = _categoryService.GetAllCategories().ConvertAll(a => 
            {
                return new SelectListItem()
                {
                    Text = a.CategoryName,
                    Value = a.CategoryID.ToString(),
                    Selected = a.CategoryID == model.CategoryID ? true : false
                };
            });

            List<SelectListItem> suppliersListItems = _supplierService.GetAllSuppliers().ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.CompanyName,
                    Value = a.SupplierID.ToString(),
                    Selected = a.SupplierID == model.SupplierID ? true : false
                };
            });

            ViewBag.Categories = categoryListItems;
            ViewBag.Suppliers = suppliersListItems;
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _productService.UpdateProduct(product);
            }
            
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            List<SelectListItem> categoryListItems = _categoryService.GetAllCategories().ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.CategoryName,
                    Value = a.CategoryID.ToString(),
                    Selected = false
                };
            });

            List<SelectListItem> suppliersListItems = _supplierService.GetAllSuppliers().ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.CompanyName,
                    Value = a.SupplierID.ToString(),
                    Selected = false
                };
            });

            ViewBag.Categories = categoryListItems;
            ViewBag.Suppliers = suppliersListItems;

            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _productService.CreateProduct(product);
            }

            return RedirectToAction("Index");
        }
    }
}