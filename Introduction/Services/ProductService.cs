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

        private readonly ILogger _logger = Log.Log.CreateLogger<ProductService>();

        public ProductService(NorthwindContext context, IConfiguration configuration)
        {
            _dbContext = context;
            _configuration = configuration;
        }

        public void CreateProduct(Products product)
        {
            try
            {
                _logger.LogInformation("Adding new record to database");
                _dbContext.Products.Add(product);
                _dbContext.SaveChanges();
                _logger.LogInformation("Record added to database successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error adding new record to database! {ex.Message}");
                throw;
            }
        }

        public List<Products> GetAllProducts()
        {
            _logger.LogInformation("Getting all products from database");
            int maxAmount = Convert.ToInt32(_configuration.GetSection("ProductParams").GetSection("MaxAmountPerRequest").Value);
            _logger.LogInformation($"Max amount of products written in configuration: {maxAmount.ToString()}");
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
                _logger.LogInformation("Updating record in database");
                try
                {
                    _dbContext.Attach(product).State = EntityState.Modified;
                    _dbContext.SaveChanges();
                    _logger.LogInformation("Record updated successfully");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error updating record in database: {ex.Message}");
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
            if (product.QuantityPerUnit == null)
                return false;
            return true;
        }
    }
}
