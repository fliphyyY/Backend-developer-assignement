using Application.ProductContext;
using Asp.Versioning;
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

        [MapToApiVersion(1.0)]
        [HttpGet("getProducts")]
        public async Task<JsonResult> GetProducts()
        {
            var products = await myProductContext.GetProducts();
            return new JsonResult(products) { StatusCode = StatusCodes.Status200OK };
        }

        [MapToApiVersion(2.0)]
        [HttpGet("getProductsPagination")]
        public async Task<JsonResult> GetProductsPagination(int pageSize, int pageNumber)
        {
            var products = await myProductContext.GetProductsPagination(pageSize, pageNumber);
            return new JsonResult(products) { StatusCode = StatusCodes.Status200OK };
        }
    }
}
