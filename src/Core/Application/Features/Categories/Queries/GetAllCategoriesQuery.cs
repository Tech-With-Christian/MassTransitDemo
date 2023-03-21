using Domain.Entitites.Categories;
using MediatR;

namespace Application.Features.Categories.Queries
{
    public record GetAllCategoriesQuery : IRequest<List<Category>>;
}
