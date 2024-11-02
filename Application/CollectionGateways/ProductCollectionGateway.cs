using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
