using Application.Features.Categories.Queries;
using Application.Services.Categories;
using Domain.Entitites.Categories;
using MediatR;

namespace Application.Features.Categories.Handlers.Read
{
    public class GetCategoryByNameHandler : IRequestHandler<GetCategoryByNameQuery, Category>
    {
        private readonly ICategoryService _categories;

        public GetCategoryByNameHandler(ICategoryService categories)
        {
            _categories = categories;
        }

        public async Task<Category> Handle(GetCategoryByNameQuery request, CancellationToken cancellationToken)
        {
            return await _categories.GetCategoryByNameAsync(request.name, cancellationToken);
        }
    }
}
