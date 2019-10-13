using System;
using System.Collections.Generic;
using System.Linq;
using Introduction.Models;
using Microsoft.EntityFrameworkCore;

namespace Introduction.Services
{
    public class ProductService : IProductService
    {
        private readonly NorthwindContext _dbContext;

        public ProductService(NorthwindContext context)
        {
            _dbContext = context;
        }
        public List<Products> GetAllProducts()
        {
            return _dbContext.Products
                .Include(product => product.Category)
                .Include(product => product.Supplier)
                .ToList();
        }

        public Products GetProduct(int id)
        {
            throw new NotImplementedException();
        }
    }
}
