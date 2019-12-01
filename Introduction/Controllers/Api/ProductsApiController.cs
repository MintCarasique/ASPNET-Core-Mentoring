using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Northwind.Core.Models;
using Northwind.Core.Services;

namespace Northwind.Controllers.Api
{
    [Route("api/Products")]
    public class ProductsApiController : Controller
    {
        private IProductService _productService;

        public ProductsApiController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _productService.GetAllProducts();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public Product Get(int id)
        {
            return _productService.GetProduct(id);
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]Product value)
        {
            _productService.CreateProduct(value);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Product value)
        {
            value.ProductID = id;
            _productService.UpdateProduct(value);
        }
    }
}
