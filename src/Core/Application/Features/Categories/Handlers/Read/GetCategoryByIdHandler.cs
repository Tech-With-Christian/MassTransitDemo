using Application.Features.Categories.Queries;
using Application.Services.Categories;
using Domain.Entitites.Categories;
using MediatR;

namespace Application.Features.Categories.Handlers.Read
{
    public class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery, Category>
    {
        private readonly ICategoryService _categories;

        public GetCategoryByIdHandler(ICategoryService categories)
        {
            _categories = categories;
        }

        public async Task<Category> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            return await _categories.GetCategoryByIdAsync(request.id, cancellationToken);
        }
    }
}
