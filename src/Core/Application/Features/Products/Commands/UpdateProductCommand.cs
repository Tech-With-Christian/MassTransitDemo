using Domain.Entitites.Products;
using MediatR;

namespace Application.Features.Products.Commands
{
    public record UpdateProductCommand(Product product) : IRequest<Product>;
}
