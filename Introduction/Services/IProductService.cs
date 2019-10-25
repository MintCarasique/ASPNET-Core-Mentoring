using System.Collections.Generic;
using Northwind.Models;

namespace Northwind.Services
{
    public interface IProductService
    {
        List<Product> GetAllProducts();

        Product GetProduct(int id);

        void UpdateProduct(Product product);

        void CreateProduct(Product product);
    }
}
