using Domain.Entitites.Categories;
using MediatR;

namespace Application.Features.Categories.Queries
{
    public record GetCategoryByIdQuery(Guid id) : IRequest<Category>;
}
