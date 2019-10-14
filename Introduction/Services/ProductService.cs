using System;
using System.Collections.Generic;
using System.Linq;
using Introduction.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Introduction.Services
{
    public class ProductService : IProductService
    {
        private readonly NorthwindContext _dbContext;

        private readonly IConfiguration _configuration;

        private readonly ILogger<ProductService> _logger;

        public ProductService(NorthwindContext context, IConfiguration configuration, ILogger<ProductService> logger)
        {
            _dbContext = context;
            _configuration = configuration;
            _logger = logger;
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
            if (IsValid(product))
            {
                _logger.LogInformation("Adding record to database");
                try
                {
                    _dbContext.Attach(product).State = EntityState.Modified;
                    _dbContext.SaveChanges();
                    _logger.LogInformation("Record added to database successfully");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error adding new record to database: {ex.Message}");
                }
            }
            
        }

        private bool IsValid(Products product) 
        {
            if (product.CategoryID == null)
                return false;
            if (product.SupplierID == null)
                return false;
            if (product.UnitPrice < 0)
                return false;
            if (product.UnitsInStock < 0)
                return false;
            if (product.UnitsOnOrder < 0)
                return false;
            if (product.ReorderLevel < 0)
                return false;
            if (Convert.ToInt32(product.QuantityPerUnit) < 0)
                return false;
            return true;
        }
    }
}
