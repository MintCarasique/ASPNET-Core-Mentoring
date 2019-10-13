using Introduction.Services;
using Microsoft.AspNetCore.Mvc;

namespace Introduction.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View(_productService.GetAllProducts());
        }

        public IActionResult Edit(int id)
        {
            var model = _productService.GetProduct(id);
            if (model == null) 
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}