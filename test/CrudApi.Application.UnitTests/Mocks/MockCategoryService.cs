using Application.Services.Categories;
using CrudApi.Application.UnitTests.Mocks;
using Domain.Entitites.Categories;
using Moq;

namespace CrudApi.Application.UnitTests.Mocks
{
    public static class MockCategoryService
    {
        /// <summary>
        /// Mock the Category Service
        /// </summary>
        /// <returns></returns>
        public static Mock<ICategoryService> GetCategoryService()
        {
            // Create new list of categories and add the categories
            List<Category> categories = new List<Category>
            {
                new Category
                {
                    Name = "T-Shirts",
                    Description = "This category contains only t-shirts."
                },
                new Category
                {
                    Name = "Shorts",
                    Description = "This category will only contain shorts."
                }
            };

            // Initialize new Mock of type ICategoryService
            var mockCategoryService = new Mock<ICategoryService>();

            // Setup method to test Get Categories (The get all service)
            mockCategoryService.Setup(s => s.GetCategoriesAsync(CancellationToken.None)).ReturnsAsync(categories);

            // Configuration of what happens when we call the Create new Category method
            mockCategoryService.Setup(s => s.CreateCategoryAsync(It.IsAny<Category>(), CancellationToken.None)).ReturnsAsync((Category category, CancellationToken ct) =>
            {
                categories.Add(category);
                return category;
            });

            // Return the mocked service for categories
            return mockCategoryService;
        }
    }
}