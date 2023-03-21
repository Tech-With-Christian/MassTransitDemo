using Domain.Entitites.Categories;
using MediatR;

namespace Application.Features.Categories.Commands
{
    public record AddCategoryCommand(Category category) : IRequest<Category>;
}
