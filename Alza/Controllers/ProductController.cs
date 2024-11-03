using Application.ProductContext;
using Asp.Versioning;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Alza.Controllers
{
    [ApiController]
    [ApiVersion(1.0)]
    [ApiVersion(2.0)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProductController : ControllerBase
    {

        private readonly IProductContext myProductContext;

        public ProductController(IProductContext productContext)
        {
            myProductContext = productContext;
        }

        [AllowAnonymous]
        [MapToApiVersion(1.0)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("getAll")]
        public async Task<JsonResult> GetProducts()
        {
            var products = await myProductContext.GetProducts();
            return new JsonResult(products) { StatusCode = StatusCodes.Status200OK };
        }

        [AllowAnonymous]
        [MapToApiVersion(2.0)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("getProductsPagination")]
        public async Task<JsonResult> GetProductsPagination(int pageNumber, int pageSize = 10 )
        {
            var products = await myProductContext.GetProductsPagination(pageSize, pageNumber);
            return new JsonResult(products) { StatusCode = StatusCodes.Status200OK };
        }

        [AllowAnonymous]
        [MapToApiVersion(1.0)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType((StatusCodes.Status404NotFound))]
        [HttpGet("get" + "/{id}")]
        public async Task<JsonResult> GetProduct(int id)
        {
            var result = await myProductContext.GetProductById(id);
            return new JsonResult(result.Data) { StatusCode = (int)result.StatusCode};
        }

        [AllowAnonymous]
        [MapToApiVersion(1.0)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType((StatusCodes.Status500InternalServerError))]
        [HttpPatch("updateProductDescription")]
        public async Task<IActionResult> UpdateProductDescription(ProductUpdateDescriptionDto productUpdateDescriptionDto)
        {
            var result = await myProductContext.UpdateProductDescription(productUpdateDescriptionDto);
            return StatusCode((int)result.StatusCode, result.Message);
        }
    }
}
