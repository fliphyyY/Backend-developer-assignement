using Alza.CustomResponse;
using Domain.Models;

namespace Application.ProductContext
{
    public interface IProductContext
    {
        Task<List<Product>> GetProducts();

        Task<List<Product>> GetProductsPagination(int pageNumber, int pageSize = 10);

        Task<ResponseHandler> GetProductById(int id);


    }
}
