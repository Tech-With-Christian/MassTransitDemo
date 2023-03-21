using Application.Features.Products.Commands;
using Application.Features.Products.Notifications;
using Application.Services.Products;
using Domain.Entitites.Products;
using MediatR;

namespace Application.Features.Products.Handlers.Create
{
    public class AddProductHandler : IRequestHandler<AddProductCommand, Product>
    {
        private readonly IProductService _products;
        private readonly IMediator _mediator;

        public AddProductHandler(IProductService products, IMediator mediator)
        {
            _products = products;
            _mediator = mediator;
        }

        public async Task<Product> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            Product createdProduct = await _products.CreateProductAsync(request.product, cancellationToken);
            await _mediator.Publish(new ProductCreatedNotification(createdProduct), cancellationToken);

            return createdProduct;
        }
    }
}
