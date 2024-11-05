using System.Net;
using Alza.Controllers;
using Alza.CustomResponse;
using Application.ProductContext;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Alza.Tests
{
    public class ProductControllerTests
    {
        private ProductController mySut;
        private Mock<IProductContext> myProductContextMock;

        public ProductControllerTests()
        {
            myProductContextMock = new Mock<IProductContext> { DefaultValue = DefaultValue.Mock }; ;
            var httpContext = new DefaultHttpContext();

            var controllerContext = new ControllerContext
            {
                HttpContext = httpContext,
            };

            mySut = new ProductController(myProductContextMock.Object)
            {
                ControllerContext = controllerContext
            };

        }

        [Fact]
        public async void GetAllProducts_CorrectMethodIsCalled_OKStatusReturned()
        {

            var products = new List<Product>()
            {
                new Product()
                {
                    Id = 1,
                    Name = "Product1",
                    ImgUri = "/products/product1.jpg",
                    Price = 341.23M,
                    Description = "Lorem Ipsum"
                },

            };
            myProductContextMock.Setup(m => m.GetProducts()).ReturnsAsync(products);
           JsonResult jsonResult =  await mySut.GetAllProducts();

           myProductContextMock.Verify(m => m.GetProducts(), Times.Once);
           Assert.NotNull(jsonResult);
           Assert.Equal(StatusCodes.Status200OK, jsonResult.StatusCode);
           Assert.Equal(products, jsonResult.Value);

        }

        [Fact]
        public async void GetProductsPagination_CorrectMethodIsCalled_OKStatusReturned()
        {

            var products = new List<Product>()
            {
                new Product()
                {
                    Id = 1,
                    Name = "Product1",
                    ImgUri = "/products/product1.jpg",
                    Price = 341.23M,
                    Description = "Lorem Ipsum"
                },

            };
            myProductContextMock.Setup(m => m.GetProductsPagination(1, 10)).ReturnsAsync(products);
            JsonResult jsonResult = await mySut.GetProductsPagination(1);

            myProductContextMock.Verify(m => m.GetProductsPagination(1, 10), Times.Once);
            Assert.NotNull(jsonResult);
            Assert.Equal(StatusCodes.Status200OK, jsonResult.StatusCode);
            Assert.Equal(products, jsonResult.Value);

        }

        [Fact]
        public async void GetProduct_CorrectMethodIsCalled_OKStatusReturned()
        {

            var response = new ResponseHandler()
            {

                StatusCode = HttpStatusCode.OK,
                Message = String.Empty,
                Succeeded = true,
                Data = new Product()
                {
                    Id = 1,
                    Name = "Product1",
                    ImgUri = "/products/product1.jpg",
                    Price = 341.23M,
                    Description = "Lorem Ipsum"
                },

            };
            myProductContextMock.Setup(m => m.GetProductById(1)).ReturnsAsync(response);
            JsonResult jsonResult = await mySut.GetProduct(1);

            myProductContextMock.Verify(m => m.GetProductById(1), Times.Once);
            Assert.NotNull(jsonResult);
            Assert.Equal(StatusCodes.Status200OK, jsonResult.StatusCode);
            Assert.Equal(response.Data, jsonResult.Value);
        }

        [Fact]
        public async void UpdateProductDescription_CorrectMethodIsCalled_OKStatusReturned()
        {
            var productUpdateDescriptionDto = new ProductUpdateDescriptionDto()
            {
                Id = 1,
                Description = "New description for product"
            };

            var response = new ResponseHandler()
            {

                StatusCode = HttpStatusCode.OK,
                Message = "Update of product's description with id 1 has been successful!",
                Succeeded = true,
            };

            myProductContextMock.Setup(m => m.UpdateProductDescription(productUpdateDescriptionDto)).ReturnsAsync(response);
            IActionResult result = await mySut.UpdateProductDescription(productUpdateDescriptionDto);

            myProductContextMock.Verify(m => m.UpdateProductDescription(productUpdateDescriptionDto), Times.Once);
            Assert.IsType<ObjectResult>(result);

            Assert.Equal(StatusCodes.Status200OK, ((ObjectResult)result).StatusCode); ;
            Assert.Equal(response.Message, ((ObjectResult)result).Value);

        }
    }
}