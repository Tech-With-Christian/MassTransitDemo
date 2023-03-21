using Domain.Entitites.Products;
using MediatR;

namespace Application.Features.Products.Queries
{
    public record GetProductByIdQuery(Guid productId) : IRequest<Product>;
}
