using Application.Features.Products.Commands;
using Application.Services.Products;
using Domain.Entitites.Products;
using MediatR;

namespace Application.Features.Products.Handlers.Create
{
    public class AddProductHandler : IRequestHandler<AddProductCommand, Product>
    {
        private readonly IProductService _products;

        public AddProductHandler(IProductService products)
        {
            _products = products;
        }

        public async Task<Product> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            return await _products.CreateProductAsync(request.product, cancellationToken);
        }
    }
}
