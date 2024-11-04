using Application.CollectionGateways;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Infrastructure.Context;
using Moq;
using Domain.ICollectionGateway;


namespace Application.Tests
{
    public class ProductCollectionGatewayTests : IDisposable
    {

        private AppDbContext myAppDbContext;
        private AppDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            myAppDbContext = new AppDbContext(options);
            return myAppDbContext;
        }

        [Fact]
        public async Task TC01_FetchAllProduct_AllAndCorrectAssetsReturned()
        {
            // GIVEN:
            // Get DB context
            var appDbContext = GetInMemoryDbContext();
            var collection = new ProductCollectionGateway(appDbContext);

            // Assets are stored to database

            var products = new List<Product>
            {
                new Product {  Name = "Product 1", Price = 15.04f, ImgUri = "/product1.jpg", Description = "Description 1"},
                new Product {  Name = "Product 2", Price = 26.05f, ImgUri = "/product2.jpg" },
                new Product {  Name = "Product 3", Price = 26.99f, ImgUri = "/product3.jpg", Description = "Description 3"}
            };

            await appDbContext.AddRangeAsync(products);
            await appDbContext.SaveChangesAsync();

            // WHEN:
            //Assets are get

            var result = await collection.FetchAllProducts();


            //THEN:
            //Correct assets count
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
            Assert.Equal("Product 1", result[0].Name);
            Assert.Equal(26.05f, result[1].Price);
            Assert.Equal(null, result[1].Description);
            Assert.Equal("Description 3", result[2].Description);

        }

        [Theory]
        [InlineData(1, 10, "Product 10")]
        [InlineData(2, 10, "Product 20")]
        [InlineData(3, 10, "Product 30")]
        [InlineData(4, 7, "Product 37")]
        public async Task TC02_FetchProductsPagination_DefaultPageSize(int pageSize, int expectedCount, string lastAssetName)
        {
            // GIVEN:
            // Get DB context
            var appDbContext = GetInMemoryDbContext();
            var collection = new ProductCollectionGateway(appDbContext);

            // Assets are stored to database

            await appDbContext.AddRangeAsync(GetProducts(37));
            await appDbContext.SaveChangesAsync();

            // WHEN:
            //Assets are get

            var result = await collection.FetchProductsPagination(pageSize);


            //THEN:
            //Correct assets count
            Assert.NotNull(result);
            Assert.Equal(expectedCount, result.Count);
            Assert.Equal(result[^1].Name, lastAssetName);

        }

        private List<Product> GetProducts(int count)
        {

            var products = new List<Product>();
            Random random = new Random();

            for (int i = 1; i <= count; i++)
            {
                products.Add(
                    new Product
                    {
                        Name = $"Product {i}",
                        Price = (float)(random.NextDouble() * (1000 - 50) + 50),
                        ImgUri = $"/products/product{i}.jpg",
                        Description = $"Description {i}"
                    }
                );

            }

            return products;
        }

        public void Dispose()
        {
            myAppDbContext.Database.EnsureDeleted(); 
            myAppDbContext.Dispose();
        }
    }
}
