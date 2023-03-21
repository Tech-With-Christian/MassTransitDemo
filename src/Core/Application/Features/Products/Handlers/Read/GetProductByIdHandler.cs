using Application.Features.Products.Queries;
using Application.Services.Products;
using Domain.Entitites.Products;
using MediatR;

namespace Application.Features.Products.Handlers.Read
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, Product>
    {
        private readonly IProductService _products;

        public GetProductByIdHandler(IProductService products)
        {
            _products = products;
        }

        public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            return await _products.GetProductByIdAsync(request.productId, cancellationToken);
        }
    }
}
