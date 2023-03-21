using Application.Services.Categories;
using Data.Database;
using Domain.Entitites.Categories;
using Domain.Helpers.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Categories
{
    internal class CategoryService : ICategoryService
    {
        #region Constructor
        private readonly ApplicationDbContext _db;

        public CategoryService(ApplicationDbContext db)
        {
            _db = db;
        }

        #endregion Constructor

        #region Create (C)

        /// <summary>
        /// This method will start by checking if the category has already been created in the database.<br />
        /// If it already exists, it will throw an exception of type CustomException with an error message.<br />
        /// If the category doesn't exist, it will be added and saved in the database and then returned to the requesting method.
        /// </summary>
        /// <param name="category">Category to create in the database</param>
        /// <returns>The category that has been created in the database</returns>
        /// <exception cref="CustomException">Throws if the category has already been added</exception>
        public async Task<Category> CreateCategoryAsync(Category category, CancellationToken ct)
        {
            // Make sure that we do not have any categories in the database with the same name
            if (await _db.Categories.AsNoTracking().AnyAsync(x => x.Name == category.Name))
                throw new CustomException($"A category with the name {category.Name} already exists.");

            // Add and save the category in the database
            _db.Categories.Add(category);
            await _db.SaveChangesAsync(ct);
            return category;
        }

        #endregion Create (C)

        #region Read (R)

        /// <summary>
        /// Async method that enumerates the Categories asynchronously and returns them as an IEnumerable.
        /// </summary>
        /// <returns>List of categories from database</returns>
        public async Task<List<Category>> GetCategoriesAsync(CancellationToken ct)
        {
            List<Category> categories = await _db.Categories
                .AsNoTracking()
                .ToListAsync(cancellationToken: ct);

            if (categories.Count() == 0)
            {
                throw new KeyNotFoundException("There are no categories in the database. Please add a category and try again.");
            }

            return categories;
        }

        /// <summary>
        /// Get a category by it's ID.
        /// </summary>
        /// <param name="id">Category ID</param>
        /// <returns>A category</returns>
        public async Task<Category> GetCategoryByIdAsync(Guid id, CancellationToken ct)
        {
            return await getCategoryByIdAsync(id, ct);
        }

        /// <summary>
        /// Get a category by it's name.
        /// </summary>
        /// <param name="name">Category Name</param>
        /// <returns>A category</returns>
        public async Task<Category> GetCategoryByNameAsync(string name, CancellationToken ct)
        {
            return await getCategoryByNameAsync(name, ct);
        }

        #endregion Read (R)

        #region Update (U)

        /// <summary>
        /// This method will start by checking if the category exist in the database.<br />
        /// It will then continue to validate the category to update and make sure that no category already exist with the same name
        /// and that the category we would like to update isn't of the same name as the update data.<br />
        /// Finally it will update the entity in the database and return the updated values.
        /// </summary>
        /// <param name="categoryToUpdate">The category we would like to update</param>
        /// <returns>The updated category</returns>
        /// <exception cref="CustomException">Thrown if a category in the database with the same name already exists</exception>
        public async Task<Category> UpdateCategoryAsync(Category categoryToUpdate, CancellationToken ct)
        {
            // Make sure that the category exist in the database
            Category category = await getCategoryByIdAsync(categoryToUpdate.Id, ct);

            // Validation before updating the category
            // We don't want any categories in the database with the same name
            if (category.Name != categoryToUpdate.Name && await _db.Categories.AnyAsync(x => x.Name == categoryToUpdate.Name))
            {
                throw new CustomException($"A category with the name {categoryToUpdate.Name} already exist in the database. Please choose another name.");
            }

            // No problems, let's update the category
            _db.Categories.Update(categoryToUpdate);
            await _db.SaveChangesAsync(cancellationToken: ct);
            return categoryToUpdate;
        }

        #endregion Update (U)

        #region Delete (D)

        /// <summary>
        ///  This method will request the helper method to make sure that the category exists in the database<br />
        ///  If the category exists it will be removed and the database will be updated.
        /// </summary>
        /// <param name="id">ID of the category to delete</param>
        public async Task DeleteCategoryAsync(Guid id, CancellationToken ct)
        {
            // Make sure no products are connected to the category
            if (await _db.Products.AnyAsync(x => x.CategoryId == id))
            { 
                throw new CustomException("There are products connected to this category. Please remove the products from the category and try again.");
            }

            // No products connected. Continue removing the category from the database
            Category category = await getCategoryByIdAsync(id, ct);
            _db.Categories.Remove(category);
            await _db.SaveChangesAsync(cancellationToken: ct);
        }

        #endregion Delete (D)

        #region Helpers

        /// <summary>
        /// Get a category by the id of the category.
        /// </summary>
        /// <param name="id">ID for category</param>
        /// <returns>The category matching the ID provided for the request.</returns>
        /// <exception cref="KeyNotFoundException">Thrown if the category wasn't found in the database</exception>
        private async Task<Category> getCategoryByIdAsync(Guid id, CancellationToken ct)
        {
            Category category = await _db.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken: ct);
            if (category == null) throw new KeyNotFoundException("The category was not found in the database.");
            return category;
        }

        /// <summary>
        /// Get a category by it's name.
        /// </summary>
        /// <param name="name">Name of the category</param>
        /// <returns>The category matching the name provided for the request.</returns>
        /// <exception cref="KeyNotFoundException">Thrown if the category wasn't found in the database</exception>
        private async Task<Category> getCategoryByNameAsync(string name, CancellationToken ct)
        {
            Category category = await _db.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Name == name, cancellationToken: ct);
            if (category == null) throw new KeyNotFoundException("The category was not found in the database.");
            return category;
        }

        #endregion Helpers

    }
}
