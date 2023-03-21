using Application.Features.Categories.Commands;
using Application.Services.Categories;
using Domain.Entitites.Categories;
using MediatR;

namespace Application.Features.Categories.Handlers.Update
{
    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, Category>
    {
        private readonly ICategoryService _categories;

        public UpdateCategoryHandler(ICategoryService categories)
        {
            _categories = categories;
        }

        public async Task<Category> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            return await _categories.UpdateCategoryAsync(request.category, cancellationToken);
        }
    }
}
