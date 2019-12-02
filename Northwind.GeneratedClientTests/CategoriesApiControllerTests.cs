using System;
using System.Collections.Generic;
using System.Net.Http;
using Xunit;

namespace Northwind.GeneratedClientTests
{
    public class CategoriesApiControllerTests
    {
        [Fact]
        public void GetAllCategories_ShouldReturnNotNull()
        {
            var http = new HttpClient();
            var client = new CategoriesApiClient(http);
            IEnumerable<Category> result = client.GetAllAsync().Result;
            Assert.NotNull(result);
        }

        [Fact]
        public void GetCategoryById_IfIdValid_ShouldReturnCategory()
        {
            var http = new HttpClient();
            var client = new CategoriesApiClient(http);
            var result = client.GetAsync(1).Result;
            Assert.NotNull(result);
            Assert.Equal(1, result.CategoryID);
        }

        [Fact]
        public void GetCategoryById_IfIdInvalid_ShouldReturnNull()
        {
            var http = new HttpClient();
            var client = new CategoriesApiClient(http);
            var result = client.GetAsync(-5).Result;
            Assert.Null(result);
        }
    }
}
