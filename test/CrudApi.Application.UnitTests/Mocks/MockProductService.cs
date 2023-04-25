using Application.Services.Products;
using Domain.Entitites.Products;
using Moq;

namespace CrudApi.Application.UnitTests.Mocks
{
    public static class MockProductService
    {
        public static Mock<IProductService> GetProductService()
        {
            List<Product> products = new List<Product>()
            {
                    new Product
                    {
                        Name = "T-Shirt",
                        Description = "This is a t-shirt",
                        Price = 10.00,
                        Stock = 5,
                        CategoryId = Guid.Parse("2439dd41-cbc1-4d71-8f3a-926267e3f9f4")
                    },
                    new Product
                    {
                        Name = "Shorts",
                        Description = "Nice blue shorts",
                        Stock = 2,
                        Price = 24.95,
                        CategoryId = Guid.Parse("6b383e42-8a94-4a86-a69e-064834c5bd6f")
                    }
            };

            // Initialize a Mock of type IProductService
            var mockProductService = new Mock<IProductService>();

            mockProductService.Setup(p => p.GetProductsAsync(CancellationToken.None)).ReturnsAsync(products);

            mockProductService.Setup(p => p.CreateProductAsync(It.IsAny<Product>(), CancellationToken.None)).ReturnsAsync((Product product, CancellationToken ct) =>
            {
                products.Add(product);
                return product;
            });

            return mockProductService;
        }
    }
}
