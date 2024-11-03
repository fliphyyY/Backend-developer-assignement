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
        public async Task<List<Product>> FetchAllProducts()
        {
            var products = myAppDbContext.Products;
            return await products.ToListAsync();
        }

        public async Task<List<Product>> FetchProductsPagination(int pageNumber, int pageSize = 10)
        {
            var products = await myAppDbContext.Products.AsNoTracking()
                .OrderBy(x => x.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return products;
        }

        public async Task<Product?> FetchProduct(int id)
        {
            return await myAppDbContext.Products.FirstOrDefaultAsync(item => item.Id == id);
        }

        public async Task<int> UpdateProduct(Product product)
        {
            myAppDbContext.Products.Attach(product);
            myAppDbContext.Entry(product).State = EntityState.Modified;

           return await myAppDbContext.SaveChangesAsync();

        }
    }
}
