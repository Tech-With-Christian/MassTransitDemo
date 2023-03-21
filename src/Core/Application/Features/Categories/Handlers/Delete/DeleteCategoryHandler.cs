using Application.Features.Categories.Commands;
using Application.Services.Categories;
using MediatR;

namespace Application.Features.Categories.Handlers.Delete
{
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryByIdCommand>
    {
        private readonly ICategoryService _categories;

        public DeleteCategoryHandler(ICategoryService categories)
        {
            _categories = categories;
        }

        public async Task Handle(DeleteCategoryByIdCommand request, CancellationToken cancellationToken)
        {
            await _categories.DeleteCategoryAsync(request.id, cancellationToken);
        }
    }
}
