using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Northwind.Core.Models;
using Northwind.Core.Services;

namespace Northwind.Controllers
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

            var categoryListItems = _categoryService.GetAllCategories().ConvertAll(a => new SelectListItem()
            {
                Text = a.CategoryName,
                Value = a.CategoryID.ToString(),
                Selected = a.CategoryID == model.CategoryID
            });

            var suppliersListItems = _supplierService.GetAllSuppliers().ConvertAll(a => new SelectListItem()
            {
                Text = a.CompanyName,
                Value = a.SupplierID.ToString(),
                Selected = a.SupplierID == model.SupplierID
            });

            ViewBag.Categories = categoryListItems;
            ViewBag.Suppliers = suppliersListItems;
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid && product != null)
            {
                _productService.UpdateProduct(product);
                return RedirectToAction("Index");
            }
            else 
            {
                return View(product);
            }
            
        }

        [HttpGet]
        public IActionResult Create()
        {
            var categoryListItems = _categoryService.GetAllCategories().ConvertAll(a => new SelectListItem()
            {
                Text = a.CategoryName,
                Value = a.CategoryID.ToString(),
                Selected = false
            });

            var suppliersListItems = _supplierService.GetAllSuppliers().ConvertAll(a => new SelectListItem()
            {
                Text = a.CompanyName,
                Value = a.SupplierID.ToString(),
                Selected = false
            });

            ViewBag.Categories = categoryListItems;
            ViewBag.Suppliers = suppliersListItems;

            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid && product != null)
            {
                _productService.CreateProduct(product);
            }
            else
            {
                return View(product);
            }

            return RedirectToAction("Index");
        }
    }
}