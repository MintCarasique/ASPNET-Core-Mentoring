﻿using System.Collections.Generic;
using Northwind.Core.Models;
using Northwind.Core.Repositories;

namespace Northwind.Core.Services
{
    public class ProductService : IProductService
    {

        private readonly IRepository<Product> _productRepository;

        public ProductService(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public void CreateProduct(Product product)
        {
            _productRepository.Create(product);
        }

        public List<Product> GetAllProducts()
        {
            return _productRepository.GetAll();
        }

        public Product GetProduct(int id)
        {
            return _productRepository.Get(id);
        }

        public void UpdateProduct(Product product)
        {
            _productRepository.Update(product);
        }
    }
}
