using Application.Features.Products.Commands;
using Application.Features.Products.Handlers.Create;
using Application.Features.Products.Notifications;
using Application.Services.Products;
using CrudApi.Application.UnitTests.Mocks;
using Domain.Entitites.Products;
using MediatR;
using Moq;
using Shouldly;

namespace CrudApi.Application.UnitTests.Products.Commands
{
    public class CreateProductCommandHandlerTest
    {
        private readonly Mock<IProductService> _mockProductService;
        private readonly Product _product;

        public CreateProductCommandHandlerTest()
        {
            // Get the mocked product service
            _mockProductService = MockProductService.GetProductService();

            // Create a new product object we can use in our tests
            _product = new Product
            {
                Name = "Shoes",
                Description = "Cool sneakers to wear in the sun",
                Price = 27.95,
                Stock = 7,
                CategoryId = Guid.Parse("278a69c1-badb-44a9-a3e5-ba8407ddf13a")
            };
        }

        /// <summary>
        /// Test if a new product could be added and that we were able to
        /// publish a notification for the product created command.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task AddNewProduct()
        {
            // Arrange

            // Create a new mock object for IMediator
            var mediator = new Mock<IMediator>();

            // Create a new instance of AddProductHandler, passing in the mock product service and mediator objects
            var handler = new AddProductHandler(_mockProductService.Object, mediator.Object);

            // Create a cancellation token for use in our tests
            CancellationToken cancellationToken = CancellationToken.None;

            // Act

            // Use the handler to handle an AddProductCommand, passing in the product and cancellation token
            var result = await handler.Handle(new AddProductCommand(_product), CancellationToken.None);

            // Get all products from the mocked service
            var products = await _mockProductService.Object.GetProductsAsync(CancellationToken.None);

            // Assert that the result is of type Product
            result.ShouldBeOfType<Product>();

            // Assert that the count of products has increased by one, indicating that a new product was added
            products.Count.ShouldBe(3);

            // Verify

            // Verify that the CreateProductAsync method on the mock product service was called once with the correct arguments
            _mockProductService.Verify(x => x.CreateProductAsync(_product, cancellationToken), Times.Once);
            mediator.Verify(x => x.Publish(It.IsAny<ProductCreatedNotification>(), cancellationToken), Times.Once);
        }
    }
}
