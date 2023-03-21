using Application.Services.Common;
using Domain.Entitites.Products;

namespace Application.Services.Products
{
    public interface IProductService : ITransientService
    {
        /// <summary>
        /// Get a single product by specifying the ID of the product.
        /// </summary>
        /// <param name="productId">Product ID</param>
        /// <returns>A single product matching the ID provided in the request</returns>
        Task<Product> GetProductByIdAsync(Guid productId, CancellationToken ct);

        /// <summary>
        /// Get a list of products. This will return all products in the database.
        /// </summary>
        /// <returns>A list of all products in the database.</returns>
        Task<List<Product>> GetProductsAsync(CancellationToken ct);

        /// <summary>
        /// Get a list of products that belong to a specific category.
        /// </summary>
        /// <param name="categoryId">Category ID to get products for</param>
        /// <returns>List of products that belongs to a specific category</returns>
        Task<List<Product>> GetProductsByCategoryIdAsync(Guid categoryId, CancellationToken ct);

        /// <summary>
        /// Create a new product in the database. If a product with the same name already exist, it will throw an exception.<br />
        /// The exception will contain an error message with details on what to do.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        Task<Product> CreateProductAsync(Product product, CancellationToken ct);

        /// <summary>
        /// Update a product in the database.<br /> If a product with the same name already exist, it will throw an exception.
        /// </summary>
        /// <param name="product">The product that should be updated in the database</param>
        /// <returns>The updated product in the database</returns>
        Task<Product> UpdateProductAsync(Product product, CancellationToken ct);

        /// <summary>
        /// Delete a specific product in the database by it's ID.
        /// </summary>
        /// <param name="productId">ID of the product to delete</param>
        /// <returns></returns>
        Task DeleteProductAsync(Guid productId, CancellationToken ct);
    }
}
