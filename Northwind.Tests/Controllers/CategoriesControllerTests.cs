using System.Collections.Generic;
using System.Linq;
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

            var categoryServiceMock = ICategoryService(categoriesList);

            var controller = new CategoriesController(categoryServiceMock.Object);

            // act
            var result = controller.Index();

            // assert
            var viewResult = Assert.IsType<ViewResult>(result);
            categoryServiceMock.Verify(x => x.GetAllCategories(), Times.Once);
            Assert.True(viewResult.Model == categoriesList);
        }

        [Fact]
        public void Edit_IfModelValid_ShouldCallCategoryServiceMethod()
        {
            // arrange
            var category = new Category {CategoryID = 1, CategoryName = "Test"};
            var categoryServiceMock = ICategoryService(new List<Category>{category});
            var controller = new CategoriesController(categoryServiceMock.Object);

            // act
            var result = controller.Edit(category);

            // assert
            Assert.IsType<RedirectToActionResult>(result);
            categoryServiceMock.Verify(_ => _.UpdateCategory(category), Times.Once);
        }

        [Fact]
        public void Edit_IfModelInvalid_ShouldReturnToEditView()
        {
            // arrange
            var category = new Category { CategoryID = 1, CategoryName = "Test" };
            var categoryServiceMock = ICategoryService(new List<Category> { category });
            var controller = new CategoriesController(categoryServiceMock.Object);

            // act
            var result = controller.Edit(null);

            // assert
            Assert.IsType<ViewResult>(result);
            categoryServiceMock.Verify(_ => _.UpdateCategory(category), Times.Never);
        }

        private Mock<ICategoryService> ICategoryService(List<Category> categoriesList)
        {
            var categoryService = new Mock<ICategoryService>();
            categoryService.Setup(_ => _.GetAllCategories()).Returns(categoriesList);
            categoryService.Setup(_ => _.UpdateCategory(categoriesList.FirstOrDefault()));
            return categoryService;
        }
    }
}
