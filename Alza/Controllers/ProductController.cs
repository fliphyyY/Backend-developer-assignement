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
        public async Task<JsonResult> Get()
        {
            await Task.Delay(1);
            return new JsonResult(new object());
        }
    }
}
