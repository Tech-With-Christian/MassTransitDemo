using Application.Features.Products.Queries;
using Application.Services.Products;
using Domain.Entitites.Products;
using MediatR;

namespace Application.Features.Products.Handlers.Read
{
    public class GetProductsByCategoryHandler : IRequestHandler<GetProductsByCategoryQuery, List<Product>>
    {
        private readonly IProductService _products;

        public GetProductsByCategoryHandler(IProductService products)
        {
            _products = products;
        }

        public async Task<List<Product>> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
        {
            return await _products.GetProductsByCategoryIdAsync(request.categoryId, cancellationToken);
        }
    }
}
