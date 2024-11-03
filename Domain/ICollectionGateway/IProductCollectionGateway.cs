using Domain.Models;

namespace Domain.ICollectionGateway
{
    public interface IProductCollectionGateway
    {
        Task<List<Product>> FetchAllProducts();

        Task<List<Product>> FetchProductsPagination(int pageNumber, int pageSize = 10);

        Task<Product?> FetchProduct(int id);
    }
}
