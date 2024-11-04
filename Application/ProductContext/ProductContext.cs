using Alza.CustomResponse;
using Domain.ICollectionGateway;
using Domain.Models;
using Infrastructure.Context;
using System.Net;

namespace Application.ProductContext
{
    public class ProductContext : IProductContext
    {
        private readonly IProductCollectionGateway myProductCollectionGateway;
        private readonly AppDbContext myAppDbContext;

        public ProductContext(IProductCollectionGateway productCollection, AppDbContext appDbContext)
        {
            myProductCollectionGateway = productCollection;
            myAppDbContext = appDbContext;
        }

        public async Task<List<Product>> GetProducts()
        {
            return await myProductCollectionGateway.FetchAllProducts();
        }

        public async Task<List<Product>> GetProductsPagination(int pageNumber, int pageSize = 10)
        {
            if (IsPaginationInputValid(pageNumber, pageSize))
            {
                return await myProductCollectionGateway.FetchProductsPagination(pageNumber, pageSize);

            }

            return new List<Product>();
        }

        public async Task<ResponseHandler> GetProductById(int id)
        {
            Product? product = null;
            if (id > 0)
            {
                product = await myProductCollectionGateway.FetchProduct(id);

            }

            return new ResponseHandler()
            {
                StatusCode = product is null ? HttpStatusCode.NotFound : HttpStatusCode.OK,
                Message = String.Empty,
                Succeeded = product is null,
                Data = product,
            };
        }

        public async Task<ResponseHandler> UpdateProductDescription(
            ProductUpdateDescriptionDto productUpdateDescriptionDto)
        {
            using (var transaction = await myAppDbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    Product? product = null;

                    if (productUpdateDescriptionDto.Id > 0)
                    {
                        product = await myProductCollectionGateway.FetchProduct(productUpdateDescriptionDto.Id);
                    }

                    if (product is null)
                    {
                        return new ResponseHandler()
                        {
                            Message = $"Update of product with id `{productUpdateDescriptionDto.Id}` has failed!",
                            StatusCode = HttpStatusCode.NotFound,
                            Succeeded = false
                        };
                    }

                    product.Description = productUpdateDescriptionDto.Description;
                    var result = await myProductCollectionGateway.UpdateProduct(product);
                    await transaction.CommitAsync();

                    return new ResponseHandler()
                    {
                        StatusCode = result > 0 ? HttpStatusCode.OK : HttpStatusCode.InternalServerError,
                        Message = result > 0
                            ? $"Update of product's description with id `{product.Id}` has been successful!"
                            : $"Update of product with id `{productUpdateDescriptionDto.Id}` has failed!",
                        Succeeded = result > 0
                    };
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return new ResponseHandler()
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Message = $"Update of product with id `{productUpdateDescriptionDto.Id}` has failed!",
                        Succeeded = false
                    };
                }

            }
        }
    

    private bool IsPaginationInputValid(int pageNumber, int pageSize)
        {
            if (pageNumber > 0 && pageSize > 0)
            {
                return true;
            }
            return false;
        }
    }
}
