using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xunit;

namespace Northwind.GeneratedClientTests
{
    public class ProductsApiControllerTests
    {
        [Fact]
        public void GetAllProducts_ShouldReturnNotNull()
        {
            var http = new HttpClient();
            var client = new ProductsApiClient(http);
            IEnumerable<Product> result = client.GetAllAsync().Result;
            Assert.NotNull(result);
        }

        [Fact]
        public void GetProductById_IfIdValid_ShouldReturnProduct()
        {
            var http = new HttpClient();
            var client = new CategoriesApiClient(http);
            var result = client.GetAsync(1).Result;
            Assert.NotNull(result);
            Assert.Equal(1, result.CategoryID);
        }
    }
}
