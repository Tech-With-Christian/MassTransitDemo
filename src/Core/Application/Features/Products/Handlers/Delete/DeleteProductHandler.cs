using Application.Features.Products.Commands;
using Application.Services.Products;
using MediatR;

namespace Application.Features.Products.Handlers.Delete
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IProductService _products;

        public DeleteProductHandler(IProductService products)
        {
            _products = products;
        }

        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            await _products.DeleteProductAsync(request.productId, cancellationToken);
        }
    }
}
