using Domain.ICollectionGateway;
using Domain.Models;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Application.CollectionGateways
{
    public class ProductCollectionGateway : IProductCollectionGateway
    {
        private readonly AppDbContext myAppDbContext;
        public ProductCollectionGateway(AppDbContext appDbContext)
        {
            myAppDbContext = appDbContext;
        }
        public async Task<List<Product>> FetchProducts()
        {
            var products = myAppDbContext.Products;
            return await products.ToListAsync();
        }

        public async Task<List<Product>> FetchProductsPagination(int pageSize, int pageNumber)
        {
            var products = await myAppDbContext.Products.AsNoTracking()
                .OrderBy(x => x.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return products;
        }
    }
}
