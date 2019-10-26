using System.Collections.Generic;
using Northwind.Models;

namespace Northwind.Services
{
    public interface IProductService
    {
        /// <summary>
        /// Get list with all products
        /// </summary>
        /// <returns>List of products</returns>
        List<Product> GetAllProducts();

        /// <summary>
        /// Get certain product from database
        /// </summary>
        /// <param name="id">Id of product</param>
        /// <returns>Product object</returns>
        Product GetProduct(int id);

        /// <summary>
        /// Update product in database
        /// </summary>
        /// <param name="product">Product object</param>
        void UpdateProduct(Product product);

        /// <summary>
        /// Create new product in database
        /// </summary>
        /// <param name="product">New product object</param>
        void CreateProduct(Product product);
    }
}
