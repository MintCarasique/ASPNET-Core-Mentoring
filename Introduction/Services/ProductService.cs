using System;
using System.Collections.Generic;
using System.Linq;
using Introduction.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Introduction.Services
{
    public class ProductService : IProductService
    {
        private readonly NorthwindContext _dbContext;

        private readonly IConfiguration _configuration;

        public ProductService(NorthwindContext context, IConfiguration configuration)
        {
            _dbContext = context;
            _configuration = configuration;
        }

        public void CreateProduct(Products product)
        {
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
        }

        public List<Products> GetAllProducts()
        {
            int maxAmount = Convert.ToInt32(_configuration.GetSection("ProductParams").GetSection("MaxAmountPerRequest").Value);

            return _dbContext.Products.Take(maxAmount == 0 ? _dbContext.Products.Count() : maxAmount)
                .Include(product => product.Category)
                .Include(product => product.Supplier)
                .ToList();
        }

        public Products GetProduct(int id)
        {
            return _dbContext.Products.FirstOrDefault(_ => _.ProductID == id);
        }

        public void UpdateProduct(Products product)
        {
            _dbContext.Attach(product).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
