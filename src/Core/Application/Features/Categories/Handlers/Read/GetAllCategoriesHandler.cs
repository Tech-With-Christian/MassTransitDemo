using Application.Features.Categories.Queries;
using Application.Services.Categories;
using Domain.Entitites.Categories;
using MediatR;

namespace Application.Features.Categories.Handlers.Read
{
    public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery, List<Category>>
    {
        private readonly ICategoryService _categories;

        public GetAllCategoriesHandler(ICategoryService categories)
        {
            _categories = categories;
        }

        public async Task<List<Category>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            return await _categories.GetCategoriesAsync(cancellationToken);
        }
    }
}
