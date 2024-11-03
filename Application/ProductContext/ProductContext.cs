using Alza.CustomResponse;
using Domain.ICollectionGateway;
using Domain.Models;
using System.Net;

namespace Application.ProductContext
{
    public class ProductContext : IProductContext
    {
        private readonly IProductCollectionGateway myProductCollectionGateway;

        public ProductContext(IProductCollectionGateway productCollection)
        {
            myProductCollectionGateway = productCollection;
        }
        public async Task<List<Product>> GetProducts()
        {
            return await myProductCollectionGateway.FetchAllProducts();
        }

        public async Task<List<Product>> GetProductsPagination(int pageNumber, int pageSize = 10)
        {
            if (IsPaginationInputValid(pageNumber, pageSize))
            {
                return await myProductCollectionGateway.FetchProductsPagination(pageSize, pageNumber);

            }

            return new List<Product>();
        }

        public async Task<ResponseHandler> GetProductById(int id)
        {
            Product? product = null;
            if (id > 0)
            {
                product = await myProductCollectionGateway.FetchProduct(id);

            }

            return new ResponseHandler()
            {
                StatusCode = product is null ? HttpStatusCode.NotFound : HttpStatusCode.OK,
                Message = String.Empty,
                Succeeded = product is null,
                Data = product,
            };
        }

        private bool IsPaginationInputValid(int pageNumber, int pageSize)
        {
            if (pageNumber > 0 && pageSize > 0)
            {
                return true;
            }
            return false;
        }
    }
}
