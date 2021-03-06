﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Northwind.Core.Models;
using Northwind.Core.Services;

namespace Northwind.Core.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly NorthwindContext _dbContext;

        private readonly IConfigurationService _configurationService;

        private readonly ILogger _logger;

        public ProductRepository(NorthwindContext context, IConfigurationService configurationService, ILogger<ProductRepository> logger)
        {
            _dbContext = context;
            _configurationService = configurationService;
            _logger = logger;
        }

        public void Create(Product product)
        {
            try
            {
                if (IsValid(product))
                {
                    _logger.LogInformation("Adding new record to database");
                    _dbContext.Products.Add(product);
                    _dbContext.SaveChanges();
                    _logger.LogInformation("Record added to database successfully");
                }
                else
                {
                    _logger.LogError("Error adding new product to database. Model is invalid.");
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                _logger.LogError($"A concurrency violation is encountered while saving product to the database.");
            }
            catch (DbUpdateException)
            {
                _logger.LogError($"An error is encountered while saving product to the database.");
            }
        }

        public Product Get(int id)
        {
            _logger.LogInformation($"Getting certain product with id = {id} from database");
            return _dbContext.Products.FirstOrDefault(_ => _.ProductID == id);
        }

        public List<Product> GetAll()
        {
            _logger.LogInformation("Getting all products from database");
            int maxAmount = _configurationService.MaxProductCount;
            _logger.LogInformation($"Max amount of products written in configuration: {maxAmount.ToString()}");
            return _dbContext.Products.Take(maxAmount == 0 ? _dbContext.Products.Count() : maxAmount)
                .Include(product => product.Category)
                .Include(product => product.Supplier)
                .ToList();
        }

        public void Update(Product product)
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
                catch (DbUpdateConcurrencyException)
                {
                    _logger.LogError($"A concurrency violation is encountered while saving product '{product.ProductID}' to the database.");
                }
                catch (DbUpdateException)
                {
                    _logger.LogError($"An error is encountered while saving product '{product.ProductID}' to the database.");
                }
            }
            else
            {
                _logger.LogError($"Error updating product '{product.ProductID}' in database. Model is invalid.");
            }
        }

        private bool IsValid(Product product)
        {
            if (product?.CategoryID == null)
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
