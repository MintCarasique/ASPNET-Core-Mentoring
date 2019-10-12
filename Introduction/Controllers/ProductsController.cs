using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Introduction.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Introduction.Controllers
{
    public class ProductsController : Controller
    {
        private readonly NorthwindContext _dbContext;

        public ProductsController(NorthwindContext context)
        {
            _dbContext = context;
        }

        public IActionResult Index()
        {
            var products = _dbContext.Products
                .Include(product => product.Category)
                .Include(product => product.Supplier)
                .ToList();
            return View(products);
        }
    }
}