 using Application.CollectionGateways;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Infrastructure.Context;


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

        [Theory]
        [InlineData(10, 5, "Product 6", "/products/product6.jpg", "Description 6")]
        [InlineData(15, 12, "Product 13", "/products/product13.jpg", null)]
        [InlineData(36, 23, "Product 24", "/products/product24.jpg", "Description 24")]

        public async Task TC01_FetchAllProduct_AllAndCorrectAssetsReturned(int count, int id, string assetName, string assetImgUri, string assetDescription)
        {
            // GIVEN:
            // Get DB context
            var appDbContext = GetInMemoryDbContext();
            var collection = new ProductCollectionGateway(appDbContext);

            // Assets are stored to database


            await appDbContext.AddRangeAsync(GetAssets(count));
            await appDbContext.SaveChangesAsync();

            // WHEN:
            //Assets are get

            var result = await collection.FetchAllProducts();


            //THEN:
            //Correct assets count
            Assert.NotNull(result);
            Assert.Equal(count, result.Count);
            Assert.Equal(assetName, result[id].Name);
            Assert.Equal(assetImgUri, result[id].ImgUri);
            Assert.Equal(assetDescription, result[id].Description);
        }

        [Theory]
        [InlineData(1, 10, "Product 10")]
        [InlineData(2, 10, "Product 20")]
        [InlineData(3, 10, "Product 30")]
        [InlineData(4, 7, "Product 37")]
        public async Task TC02_FetchProductsPagination_DefaultPageSize(int pageNumber, int expectedCount, string lastAssetName)
        {
            // GIVEN:
            // Get DB context
            var appDbContext = GetInMemoryDbContext();
            var collection = new ProductCollectionGateway(appDbContext);

            // Assets are stored to database

            await appDbContext.AddRangeAsync(GetAssets(37));
            await appDbContext.SaveChangesAsync();

            // WHEN:
            //Assets are get

            var result = await collection.FetchProductsPagination(pageNumber);


            //THEN:
            //Correct assets count
            Assert.NotNull(result);
            Assert.Equal(expectedCount, result.Count);
            Assert.Equal(result[^1].Name, lastAssetName);

        }

        [Theory]
        [InlineData(1, 5, 5, "Product 5")]
        [InlineData(2, 7, 7,"Product 14")]
        [InlineData(3, 11, 11, "Product 33")]
        [InlineData(4, 12, 1, "Product 37")]
        public async Task TC03_FetchProductsPagination_NoDefaultPageSize(int pageNumber, int pageSize, int expectedCount, string lastAssetName)
        {
            // GIVEN:
            // Get DB context
            var appDbContext = GetInMemoryDbContext();
            var collection = new ProductCollectionGateway(appDbContext);

            // Assets are stored to database

            await appDbContext.AddRangeAsync(GetAssets(37));
            await appDbContext.SaveChangesAsync();

            // WHEN:
            //Assets are get

            var result = await collection.FetchProductsPagination(pageNumber, pageSize);


            //THEN:
            //Correct assets count
            Assert.NotNull(result);
            Assert.Equal(expectedCount, result.Count);
            Assert.Equal(result[^1].Name, lastAssetName);

        }

        [Theory]
        [InlineData(10, "Product 10", "/products/product10.jpg", "Description 10")]
        [InlineData(23, "Product 23", "/products/product23.jpg", "Description 23")]
        [InlineData(36, "Product 36", "/products/product36.jpg", "Description 36")]

        public async Task TC04_FetchProduct_CorrectAssetFetched(int id, string assetName, string assetImgUri, string assetDescription)
        {
            // GIVEN:
            // Get DB context
            var appDbContext = GetInMemoryDbContext();
            var collection = new ProductCollectionGateway(appDbContext);

            // Assets are stored to database

            await appDbContext.AddRangeAsync(GetAssets(37));
            await appDbContext.SaveChangesAsync();

            // WHEN:
            //Asset is get

            var result = await collection.FetchProduct(id);


            //THEN:
            //Correct asset values
            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
            Assert.Equal(result.Name, assetName);
            Assert.Equal(result.ImgUri, assetImgUri);
            Assert.Equal(result.Description, assetDescription);
        }

        [Theory]
        [InlineData(8, "Product 8Updated", "/products/product8Updated.jpg", "Description 8Updated")]
        [InlineData(16, "Product 16Updated", "/products/product16Updated.jpg", "Description 16Updated")]
        [InlineData(27, "Product 27Updated", "/products/product27Updated.jpg", "Description 27Updated")]

        public async Task TC05_UpdateProduct_AssetIsCorrectlyUpdated(int id, string assetName, string assetImgUri, string assetDescription)
        {
            // GIVEN:
            // Get DB context
            var appDbContext = GetInMemoryDbContext();
            var collection = new ProductCollectionGateway(appDbContext);

            // Assets are stored to database

            await appDbContext.AddRangeAsync(GetAssets(37));
            await appDbContext.SaveChangesAsync();

            // WHEN:
            //Asset is get

            var result = await collection.FetchProduct(id);
            result.Name = assetName;
            result.ImgUri = assetImgUri;
            result.Description = assetDescription;

            await collection.UpdateProduct(result);

            var updatedResult = await collection.FetchProduct(id);

            //THEN:
            //Correct asset values
            Assert.NotNull(updatedResult);
            Assert.Equal(updatedResult.Id, id);
            Assert.Equal(updatedResult.Name, assetName);
            Assert.Equal(updatedResult.ImgUri, assetImgUri);
            Assert.Equal(updatedResult.Description, assetDescription);
        }

        private List<Product> GetAssets(int count)
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
                        Description = i == 13 ? null : $"Description {i}"
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
