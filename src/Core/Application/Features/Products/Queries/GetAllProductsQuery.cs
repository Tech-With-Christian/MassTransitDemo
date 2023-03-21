using Domain.Entitites.Products;
using MediatR;

namespace Application.Features.Products.Queries
{
    public record GetAllProductsQuery : IRequest<List<Product>>;
}
