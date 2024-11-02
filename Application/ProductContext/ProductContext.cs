using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
