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

        /// <summary>
        /// Returns all products items.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET '/getAll'
        ///
        /// </remarks>
        /// <returns>Returns all products items.</returns>
        /// <response code="200">Successfully returned all products items.</response>
        [AllowAnonymous]
        [MapToApiVersion(1.0)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("getAll")]
        [Produces("application/json")]
        public async Task<JsonResult> GetProducts()
        {
            var products = await myProductContext.GetProducts();
            return new JsonResult(products) { StatusCode = StatusCodes.Status200OK };
        }

        /// <summary>
        /// Returns product items subset.
        /// </summary>
        /// <param name="pageNumber">The page number of the results to retrieve.</param>
        /// <param name="pageSize">The number of items per page. Default is 10.</param>
        /// <returns>Product items subset defined by parameters pageNumber and pageSize.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET '/getProductsPagination?pageNumber=2;pageSize=10'
        ///
        /// </remarks>
        /// <response code="200">Successfully returned subset defined by parameters pageNumber and pageSize.</response>
        [AllowAnonymous]
        [MapToApiVersion(2.0)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("getProductsPagination")]
        [Produces("application/json")]
        public async Task<JsonResult> GetProductsPagination(int pageNumber, int pageSize = 10 )
        {
            var products = await myProductContext.GetProductsPagination(pageSize, pageNumber);
            return new JsonResult(products) { StatusCode = StatusCodes.Status200OK };
        }


        /// <summary>
        /// Returns product defined by id.
        /// </summary>
        /// <param name="id">The id of product to return.</param>
        /// <returns>Product item.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PATCH '/get/3'
        ///
        /// </remarks>
        /// <response code="200">Successfully returned product defined by id.</response>
        /// <response code="404">Product with this id was not found.</response>
        [AllowAnonymous]
        [MapToApiVersion(1.0)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType((StatusCodes.Status404NotFound))]
        [HttpGet("get" + "/{id}")]
        [Produces("application/json")]
        public async Task<JsonResult> GetProduct(int id)
        {
            var result = await myProductContext.GetProductById(id);
            return new JsonResult(result.Data) { StatusCode = (int)result.StatusCode};
        }

        /// <summary>
        /// Updates product's description.
        /// </summary>
        /// <param name="ProductUpdateDescriptionDto">Update object with id and description.</param>
        /// <returns>Product item.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET '/updateProductDescription'
        ///     {
        ///     "id": 0,
        ///     "description": "string"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Successfully updated product's description.</response>
        /// <response code="404">Product with this id was not found.</response>
        /// <response code="500">Server error.</response>
        [AllowAnonymous]
        [MapToApiVersion(1.0)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType((StatusCodes.Status500InternalServerError))]
        [HttpPatch("updateProductDescription")]
        [Produces("application/json")]
        public async Task<IActionResult> UpdateProductDescription(ProductUpdateDescriptionDto productUpdateDescriptionDto)
        {
            var result = await myProductContext.UpdateProductDescription(productUpdateDescriptionDto);
            return StatusCode((int)result.StatusCode, result.Message);
        }
    }
}
