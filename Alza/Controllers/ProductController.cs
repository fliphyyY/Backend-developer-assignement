using Application.ProductContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Alza.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IProductContext myProductContext;

        public ProductController(IProductContext productContext)
        {
            myProductContext = productContext;
        }

        [AllowAnonymous]
        [HttpGet("getProducts")]
        public async Task<JsonResult> GetProducts()
        {
            var products = await myProductContext.GetProducts();
            return new JsonResult(products) { StatusCode = StatusCodes.Status200OK };
        }

        [AllowAnonymous]
        [HttpGet("getProductsPagination")]
        public async Task<JsonResult> GetProductsPagination(int pageSize, int pageNumber)
        {
            var products = await myProductContext.GetProductsPagination(pageSize, pageNumber);
            return new JsonResult(products) { StatusCode = StatusCodes.Status200OK };
        }
    }
}
