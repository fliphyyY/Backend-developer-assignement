using Domain.Models;

namespace Application.ProductContext
{
    public interface IProductContext
    {
        Task<List<Product>> GetProducts();

        Task<List<Product>> GetProductsPagination(int pageSize, int pageNumber);
    }
}
