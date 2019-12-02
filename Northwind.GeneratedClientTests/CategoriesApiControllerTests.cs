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
    }
}
