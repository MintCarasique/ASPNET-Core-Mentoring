using Introduction.Models;
using System.Collections.Generic;

namespace Introduction.Services
{
    public interface IProductService
    {
        List<Product> GetAllProducts();

        Product GetProduct(int id);

        void UpdateProduct(Product product);

        void CreateProduct(Product product);
    }
}
