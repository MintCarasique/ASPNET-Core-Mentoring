using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Northwind.Core.Models;
using Northwind.Core.Services;


namespace Northwind.Controllers.Api
{
    [Route("api/Categories")]
    public class CategoriesApiController : Controller
    {
        private ICategoryService _categoryService;

        public CategoriesApiController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Category> Get()
        {
            return _categoryService.GetAllCategories();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public Category Get(int id)
        {
            return _categoryService.GetCategory(id);
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]Category value)
        {
            _categoryService.CreateCategory(value);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Category value)
        {
            value.CategoryID = id;
            _categoryService.UpdateCategory(value);
        }
    }
}
