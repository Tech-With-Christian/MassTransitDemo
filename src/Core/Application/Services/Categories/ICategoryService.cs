using Application.Services.Common;
using Domain.Entitites.Categories;

namespace Application.Services.Categories
{
    public interface ICategoryService : ITransientService
    {
        /// <summary>
        /// Get a single category by the ID of the category.
        /// If no category exist in the database with the ID, an exception will be thrown and a status code of 404 will be returned.
        /// </summary>
        /// <param name="id">Category ID</param>
        /// <returns>A Single category</returns>
        Task<Category> GetCategoryByIdAsync(Guid id, CancellationToken ct);

        /// <summary>
        /// Get a single category by the name of the category.
        /// If no category exist in the database with the name provided, an exception will be thrown and a status code of 404 will be returned.
        /// </summary>
        /// <param name="name">Category name</param>
        /// <returns>A Single category</returns>
        Task<Category> GetCategoryByNameAsync(string name, CancellationToken ct);

        /// <summary>
        /// Get all categories in the database (if any).<br />
        /// If no categories exist in the database, an exception will be thrown and a status code of 404 will be returned.
        /// </summary>
        /// <returns>A list of categories to enumerate</returns>
        Task<List<Category>> GetCategoriesAsync(CancellationToken ct);

        /// <summary>
        /// Add a new category to the database.<br />
        /// This method will check if the category already exist in the database by checking the name of the category.<br />
        /// If a caetgory already exists in the database with the same name, an exception will be thrown and a status code of 400 (Bad Request) will be returned.
        /// </summary>
        /// <param name="category">Category to add in database</param>
        /// <returns>The category that has been added in the database</returns>
        Task<Category> CreateCategoryAsync(Category category, CancellationToken ct);

        /// <summary>
        /// Update a category in the database.<br />
        /// If the category doesn't exist a status code of 404 (Not Found) will be returned.<br />
        /// If the category name matches the name of another category in the database, a status code of 400 (Bad Request) will be returned.
        /// The updated category name can also not be the same as the one of the current category.
        /// </summary>
        /// <param name="category">Category to update</param>
        /// <returns>The updated category in the database</returns>
        Task<Category> UpdateCategoryAsync(Category category, CancellationToken ct);

        /// <summary>
        /// Delete a specific category by the ID of the category.<br />
        /// If the category doesn't exist a status code of 404 (Not Found) will be returned.
        /// </summary>
        /// <param name="id">Category ID</param>
        /// <returns></returns>
        Task DeleteCategoryAsync(Guid id, CancellationToken ct);
    }
}
