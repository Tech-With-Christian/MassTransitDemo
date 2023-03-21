using MediatR;

namespace Application.Features.Categories.Commands
{
    public record DeleteCategoryByIdCommand(Guid id) : IRequest;
}
