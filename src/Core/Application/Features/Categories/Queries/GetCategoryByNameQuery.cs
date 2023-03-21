using Domain.Entitites.Categories;
using MediatR;

namespace Application.Features.Categories.Queries
{
    public record GetCategoryByNameQuery(string name) : IRequest<Category>;
    
}
