using Application.Features.Categories.Commands;
using Application.Services.Categories;
using Domain.Entitites.Categories;
using MediatR;

namespace Application.Features.Categories.Handlers.Create
{
    public class AddCategoryHandler : IRequestHandler<AddCategoryCommand, Category>
    {
        private readonly ICategoryService _categories;

        public AddCategoryHandler(ICategoryService categories)
        {
            _categories = categories;
        }

        public async Task<Category> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            return await _categories.CreateCategoryAsync(request.category, cancellationToken);
        }
    }
}
