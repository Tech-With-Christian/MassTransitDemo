using Application.Features.Products.Commands;
using Application.Services.Products;
using Domain.Entitites.Products;
using MediatR;

namespace Application.Features.Products.Handlers.Update
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, Product>
    {
        private readonly IProductService _products;

        public UpdateProductHandler(IProductService products)
        {
            _products = products;
        }

        public async Task<Product> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            return await _products.UpdateProductAsync(request.product, cancellationToken);
        }
    }
}
