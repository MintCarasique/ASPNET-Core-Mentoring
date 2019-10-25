using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Northwind.Controllers;
using Northwind.Models;
using Northwind.Services;
using Xunit;

namespace Northwind.Tests.Controllers
{
    public class CategoriesControllerTests
    {
        [Fact]
        public void Index_ShouldReturnViewResult_ShouldReturnModel()
        {
            // arrange
            var categoriesList = new List<Category>
            {
                new Category {CategoryID = 1, CategoryName = "Test"},
                new Category {CategoryID = 2, CategoryName = "Test2"}
            };

            var categoryService = ICategoryService(categoriesList).Object;

            var controller = new CategoriesController(categoryService);

            // act
            var result = controller.Index();

            // assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.True(viewResult.Model == categoriesList);
        }

        private IMock<ICategoryService> ICategoryService(List<Category> categoriesList)
        {
            var categoryService = new Mock<ICategoryService>();
            categoryService.Setup(_ => _.GetAllCategories()).Returns(categoriesList);
            return categoryService;
        }
    }
}
