using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Application.ProductContext
{
    public class ProductContext : IProductContext
    {
        public ProductContext() { }
        public Task<List<Product>> GetProducts()
        {
            throw new NotImplementedException();
        }
    }
}
