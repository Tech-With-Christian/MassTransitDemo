using Application.Features.Products.Notifications;
using AutoMapper;
using MassTransit;
using MediatR;
using MessageBus.Messages.Products;
using Microsoft.Extensions.Logging;

namespace Application.Features.Products.Handlers.Create
{
    public class ProductCreatedNotificationHandler : INotificationHandler<ProductCreatedNotification>
    {
        private readonly IPublishEndpoint _publisher;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductCreatedNotificationHandler> _logger;

        public ProductCreatedNotificationHandler(
            IPublishEndpoint publisher,
            IMapper mapper,
            ILogger<ProductCreatedNotificationHandler> logger)
        {
            _publisher = publisher;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Handle(ProductCreatedNotification notification, CancellationToken cancellationToken)
        {
            // Map product to Product Created Event
            ProductCreatedEvent productCreatedEvent = _mapper.Map<ProductCreatedEvent>(notification.product);

            // Publish the event as a new message to RabbitMQ using MassTransit
            _logger.LogInformation($"Publishing product created event to message bus service for product: {notification.product.Name}.");
            await _publisher.Publish(productCreatedEvent);
        }
    }
}
