using Application.Features.Products.Handlers.Read;
using Application.Features.Products.Queries;
using Application.Services.Products;
using CrudApi.Application.UnitTests.Mocks;
using Domain.Entitites.Products;
using Moq;
using Shouldly;

namespace CrudApi.Application.UnitTests.Products.Queries
{
    public class GetAllProductsQueriesHandlerTest
    {
        private readonly Mock<IProductService> _mockProductService;

        public GetAllProductsQueriesHandlerTest()
        {
            _mockProductService = MockProductService.GetProductService();
        }

        [Fact]
        public async Task GetAllProducts()
        {
            CancellationToken cancellationToken = CancellationToken.None;

            // Create a new instance of the GetAllProductsHandler class, passing in the mocked product service
            var handler = new GetAllProductsHandler(_mockProductService.Object);

            // Use the handler to handle a GetAllProductsQuery, passing in the cancellation token
            var result = await handler.Handle(new GetAllProductsQuery(), cancellationToken);

            // Assert that the result is not null
            result.ShouldNotBeNull();

            // Assert that the result is a List<Product>
            result.ShouldBeAssignableTo<List<Product>>();

            // Assert that the result contains the correct number of products
            result.Count.ShouldBe(2);

            // Assert that the first product in the list has the correct name, description, price and stock count
            result[0].Name.ShouldBe("T-Shirt");
            result[0].Description.ShouldBe("This is a t-shirt");
            result[0].Price.ShouldBe(10.00);
            result[0].Stock.ShouldBe(5);


            result[1].Name.ShouldBe("Shorts");
            result[1].Description.ShouldBe("Nice blue shorts");
            result[1].Price.ShouldBe(24.95);
            result[1].Stock.ShouldBe(2);

            // Verify

            // Verify that the GetProductsAsync method on the mock product service was called once with the correct arguments
            _mockProductService.Verify(x => x.GetProductsAsync(cancellationToken), Times.Once);
        }
    }
}
