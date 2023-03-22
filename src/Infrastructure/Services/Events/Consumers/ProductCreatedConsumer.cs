using AutoMapper;
using Domain.Entitites.Products;
using MassTransit;
using MessageBus.Messages.Products;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services.Events.Consumers
{
    internal class ProductCreatedConsumer : IConsumer<ProductCreatedEvent>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<ProductCreatedConsumer> _logger;

        public ProductCreatedConsumer(IMapper mapper, ILogger<ProductCreatedConsumer> logger)
        {
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<ProductCreatedEvent> context)
        {
            // Map the Product Created Event to a Product by using data from the message
            Product product = _mapper.Map<Product>(context.Message);

            // Log the details of the product
            // You could do anything here with the consumed message such as saving it to a database,
            // calling the mediator to dispatch a command, etc.
            _logger.LogInformation($"Consumed Product Created Message. Details: Product {product.Name} has a cost of {product.Price} and we got {product.Stock} in stock.");
            
            // Return a completed task, job done!
            await Task.CompletedTask;
        }
    }
}
