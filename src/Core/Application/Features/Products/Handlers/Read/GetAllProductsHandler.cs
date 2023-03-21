using Application.Features.Products.Queries;
using Application.Services.Products;
using Domain.Entitites.Products;
using MediatR;

namespace Application.Features.Products.Handlers.Read
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, List<Product>>
    {
        private readonly IProductService _products;

        public GetAllProductsHandler(IProductService products)
        {
            _products = products;
        }

        public async Task<List<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            return await _products.GetProductsAsync(cancellationToken);
        }
    }
}
