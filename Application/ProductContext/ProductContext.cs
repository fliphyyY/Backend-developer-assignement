using Domain.ICollectionGateway;
using Domain.Models;

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
            return await myProductCollectionGateway.FetchProducts();
        }

        public async Task<List<Product>> GetProductsPagination(int pageSize, int pageNumber)
        {
            return await myProductCollectionGateway.FetchProductsPagination(pageSize, pageNumber);
        }
    }
}
