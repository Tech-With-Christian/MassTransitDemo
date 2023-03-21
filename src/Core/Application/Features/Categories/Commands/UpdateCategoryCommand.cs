using Domain.Entitites.Categories;
using MediatR;

namespace Application.Features.Categories.Commands
{
    public record UpdateCategoryCommand(Category category) : IRequest<Category>;
}
