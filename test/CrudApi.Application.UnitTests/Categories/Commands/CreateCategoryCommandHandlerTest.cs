using Application.Features.Categories.Commands;
using Application.Features.Categories.Handlers.Create;
using Application.Services.Categories;
using CrudApi.Application.UnitTests.Mocks;
using Domain.Entitites.Categories;
using Moq;
using Shouldly;

namespace CrudApi.Application.UnitTests.Categories.Commands
{
    public class CreateCategoryCommandHandlerTest
    {
        private readonly Mock<ICategoryService> _mockService;
        private readonly Category _category;

        public CreateCategoryCommandHandlerTest()
        {
            _mockService = MockCategoryService.GetCategoryService();

            _category = new Category
            {
                Name = "Underwear",
                Description = "This category will contain only underwear"
            };
        }

        [Fact]
        public async Task CreateNewCategory()
        {
            // Initialize the handler
            var handler = new AddCategoryHandler(_mockService.Object);

            // Add the category using the handler
            var result = await handler.Handle(new AddCategoryCommand(_category), CancellationToken.None);

            // Get all categories from the mocked service
            var categories = await _mockService.Object.GetCategoriesAsync(CancellationToken.None);

            // Make sure that the result we got in return from the command handler is of type Category
            result.ShouldBeOfType<Category>();

            // Make sure that we now have 3 categories as we started with 2 categories in the mock service
            categories.Count.ShouldBe(3);
        }
    }
}
