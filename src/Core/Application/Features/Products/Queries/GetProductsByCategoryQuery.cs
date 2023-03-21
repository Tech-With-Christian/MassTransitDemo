using Domain.Entitites.Products;
using MediatR;

namespace Application.Features.Products.Queries
{
    public record GetProductsByCategoryQuery(Guid categoryId) : IRequest<List<Product>>;
}
