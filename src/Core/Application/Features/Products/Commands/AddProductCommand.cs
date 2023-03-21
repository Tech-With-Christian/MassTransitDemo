using Domain.Entitites.Products;
using MediatR;

namespace Application.Features.Products.Commands
{
    public record AddProductCommand(Product product) : IRequest<Product>;
}
