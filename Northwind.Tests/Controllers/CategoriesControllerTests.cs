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
        private CategoriesController _categoriesController;

        private Mock<ICategoryService> _categoryServiceMock;

        private List<Category> _categoryList;

        [Fact]
        public void Index_ShouldReturnViewResult_ShouldReturnModel()
        {
            // arrange
            InitializeController();

            // act
            var result = _categoriesController.Index();

            // assert
            var viewResult = Assert.IsType<ViewResult>(result);
            _categoryServiceMock.Verify(x => x.GetAllCategories(), Times.Once);
            Assert.True(viewResult.Model == _categoryList);
        }

        [Fact]
        public void Edit_IfModelValid_ShouldCallCategoryServiceMethod()
        {
            // arrange
            InitializeController();

            // act
            var result = _categoriesController.Edit(_categoryList.FirstOrDefault());

            // assert
            Assert.IsType<RedirectToActionResult>(result);
            _categoryServiceMock.Verify(_ => _.UpdateCategory(_categoryList.FirstOrDefault()), Times.Once);
        }

        [Fact]
        public void Edit_IfModelInvalid_ShouldReturnToEditView()
        {
            // arrange
            InitializeController();

            // act
            var result = _categoriesController.Edit(null);

            // assert
            Assert.IsType<ViewResult>(result);
            _categoryServiceMock.Verify(_ => _.UpdateCategory(_categoryList.FirstOrDefault()), Times.Never);
        }

        [Fact]
        public void Edit_IfIdValid_ShouldReturnView()
        {
            // arrange
            InitializeController();

            // act
            var result = _categoriesController.Edit(1);

            // assert
            Assert.IsType<ViewResult>(result);
            _categoryServiceMock.Verify(_ => _.GetCategory(1), Times.Once);
        }

        [Fact]
        public void Edit_IfIdInvalid_ShouldRedirectToAction()
        {
            // arrange
            InitializeController();

            // act
            var result = _categoriesController.Edit(0);

            // assert
            Assert.IsType<RedirectToActionResult>(result);
            _categoryServiceMock.Verify(_ => _.GetCategory(0), Times.Once);
        }

        [Fact]
        public void Create_IfCategoryValid_ShouldCreateNewCategory_ShouldReturnReturnRedirectToAction()
        {
            // arrange
            InitializeController();
            var newCategory = new Category { CategoryID = 3, CategoryName = "New_Test" };

            // act
            var result = _categoriesController.Create(newCategory);

            // assert
            Assert.IsType<RedirectToActionResult>(result);
            _categoryServiceMock.Verify(_ => _.CreateCategory(newCategory), Times.Once);
        }

        [Fact]
        public void Create_IfCategoryInvalid_IfModelStateInvalid_ShouldReturnView()
        {
            // arrange
            InitializeController();

            // act
            var result = _categoriesController.Create(null);

            // assert
            Assert.IsType<ViewResult>(result);
            _categoryServiceMock.Verify(_ => _.CreateCategory(It.IsAny<Category>()), Times.Never);
        }

        private Mock<ICategoryService> InitializeCategoryService(List<Category> categoriesList)
        {
            var categoryService = new Mock<ICategoryService>();
            categoryService.Setup(_ => _.GetCategory(1)).Returns(categoriesList.FirstOrDefault);
            categoryService.Setup(_ => _.GetCategory(0)).Returns((Category) null);
            categoryService.Setup(_ => _.GetAllCategories()).Returns(categoriesList);
            categoryService.Setup(_ => _.UpdateCategory(It.IsAny<Category>()));
            categoryService.Setup(_ => _.CreateCategory(It.IsAny<Category>()));
            return categoryService;
        }

        private void InitializeController()
        {
            _categoryList = new List<Category>
            {
                new Category {CategoryID = 1, CategoryName = "Test"},
                new Category {CategoryID = 2, CategoryName = "Test2"}
            };
            _categoryServiceMock = InitializeCategoryService(_categoryList);
            _categoriesController = new CategoriesController(_categoryServiceMock.Object);
        }
    }
}
