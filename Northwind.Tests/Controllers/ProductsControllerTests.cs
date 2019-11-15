using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Northwind.Controllers;
using Northwind.Core.Models;
using Northwind.Core.Services;
using Xunit;

namespace Northwind.Tests.Controllers
{
    public class ProductsControllerTests
    {
        private ProductsController _productsController;

        private Mock<IProductService> _productServiceMock;

        private Mock<ISupplierService> _supplierServiceMock;

        private Mock<ICategoryService> _categoryServiceMock;

        private List<Product> _productList;

        [Fact]
        public void Index_ShouldGetAllProducts_ShouldReturnView()
        {
            // arrange
            InitializeController();

            // act
            var result = _productsController.Index();

            // assert
            Assert.IsType<ViewResult>(result);
            _productServiceMock.Verify(_ => _.GetAllProducts(), Times.Once);
        }

        [Fact]
        public void EditGet_IfIdValid_ShouldReturnView_ShouldReturnModel()
        {
            // arrange
            InitializeController();

            // act
            var result = _productsController.Edit(1);

            // assert
            Assert.IsType<ViewResult>(result);
            _categoryServiceMock.Verify(_ => _.GetAllCategories(), Times.Once);
            _supplierServiceMock.Verify(_ => _.GetAllSuppliers(), Times.Once);
            _productServiceMock.Verify(_ => _.GetProduct(1), Times.Once);
        }

        [Fact]
        public void EditGet_IfIdInvalid_ShouldRedirectToAction()
        {
            // arrange
            InitializeController();

            // act
            var result = _productsController.Edit(0);

            // assert
            Assert.IsType<RedirectToActionResult>(result);
            _categoryServiceMock.Verify(_ => _.GetAllCategories(), Times.Never);
            _supplierServiceMock.Verify(_ => _.GetAllSuppliers(), Times.Never);
        }

        [Fact]
        public void EditPost_IfProductValid_ShouldUpdateProduct_ShouldRedirectToAction()
        {
            // arrange
            InitializeController();
            var testProduct = _productList.FirstOrDefault();

            // act
            var result = _productsController.Edit(testProduct);

            // assert
            Assert.IsType<RedirectToActionResult>(result);
            _productServiceMock.Verify(_ => _.UpdateProduct(testProduct));
        }

        [Fact]
        public void EditPost_IfProductInvalid_ShouldReturnView_ShouldNotUpdateProduct()
        {
            // arrange
            InitializeController();

            // act
            var result = _productsController.Edit(null);

            // assert
            Assert.IsType<ViewResult>(result);
            _productServiceMock.Verify(_ => _.UpdateProduct(It.IsAny<Product>()), Times.Never);
        }

        [Fact]
        public void Create_IfProductIsNotNull_IfModelStateValid_ShouldCreateProduct_ShouldRedirectToAction()
        {
            // arrange
            InitializeController();

            // act
            var result = _productsController.Create(new Product());

            // assert
            Assert.IsType<RedirectToActionResult>(result);
            _productServiceMock.Verify(_ => _.CreateProduct(It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        public void Create_IfProductIsNull_IfModelStateValid_ShouldReturnView_ShouldNotCreateProduct()
        {
            // arrange
            InitializeController();

            // act
            var result = _productsController.Create(null);

            // assert
            Assert.IsType<ViewResult>(result);
            _productServiceMock.Verify(_ => _.UpdateProduct(It.IsAny<Product>()), Times.Never);
        }

        [Fact]
        public void Create_IfProductIsNotNull_IfModelStateInvalid_ShouldReturnView_ShouldNotCreateProduct()
        {
            // arrange
            InitializeController();
            _productsController.ModelState.AddModelError("testKey", "test error message");

            // act
            var result = _productsController.Create(new Product());

            // assert
            Assert.IsType<ViewResult>(result);
            _productServiceMock.Verify(_ => _.UpdateProduct(It.IsAny<Product>()), Times.Never);

        }

        private void InitializeController()
        {
            _productList = new List<Product>
            {
                new Product{ProductID = 1, ProductName = "Test", QuantityPerUnit = "1", ReorderLevel = 3, UnitPrice = 4, UnitsInStock = 1, UnitsOnOrder = 5, Discontinued = true, CategoryID = 1, SupplierID = 1}
            };
            _productServiceMock = InitializeProductService(_productList);
            _supplierServiceMock = InitializeSupplierService();
            _categoryServiceMock = InitializeCategoryService();

            _productsController = new ProductsController(_productServiceMock.Object, _categoryServiceMock.Object, _supplierServiceMock.Object, Mock.Of<ILogger<ProductsController>>());
        }

        private Mock<IProductService> InitializeProductService(List<Product> productList)
        {
            var productService = new Mock<IProductService>();
            productService.Setup(_ => _.GetAllProducts()).Returns(productList);
            productService.Setup(_ => _.GetProduct(1)).Returns(productList.FirstOrDefault());
            productService.Setup(_ => _.GetProduct(0)).Returns((Product) null);
            return productService;
        }

        private Mock<ISupplierService> InitializeSupplierService()
        {
            var supplierService = new Mock<ISupplierService>();
            supplierService.Setup(_ => _.GetAllSuppliers()).Returns(new List<Supplier>());
            return supplierService;
        }

        private Mock<ICategoryService> InitializeCategoryService()
        {
            var categoryService = new Mock<ICategoryService>();
            categoryService.Setup(_ => _.GetAllCategories()).Returns(new List<Category>());
            return categoryService;
        }
    }
}
