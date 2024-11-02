using Domain.Models;

namespace Domain.ICollectionGateway
{
    public interface IProductCollectionGateway
    {
        Task<List<Product>> FetchProducts();

        Task<List<Product>> FetchProductsPagination(int pageSize, int pageNumber);
    }
}
