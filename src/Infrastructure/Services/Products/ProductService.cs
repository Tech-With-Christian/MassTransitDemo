using Application.Services.Categories;
using Application.Services.Products;
using Data.Database;
using Domain.Entitites.Categories;
using Domain.Entitites.Products;
using Domain.Helpers.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Products
{
    internal class ProductService : IProductService
    {
        #region Constructor
        private readonly ApplicationDbContext _db;
        private readonly ICategoryService _categories;

        public ProductService(ApplicationDbContext db, ICategoryService categories)
        {
            _db = db;
            _categories = categories;
        }

        #endregion Constructor

        #region Create (C)

        /// <summary>
        /// Create a new product in the database by checking if a product with the same name not already exist<br />
        /// and that the category exist in the database. If a product already exist in the database with the same name a Custom Exception is thrown.<br />
        /// Next the method will check if the category exist by using the Category service. This will automatically throw an exception if it's not present in the database.
        /// </summary>
        /// <param name="product">Product for creation in database</param>
        /// <returns>The created product in the database</returns>
        /// <exception cref="CustomException">Thrown if a product already exist in the database with the same name</exception>
        public async Task<Product> CreateProductAsync(Product product, CancellationToken ct)
        {
            // Make sure that we do not have any products in the database with the same name
            if (await _db.Products.AsNoTracking().AnyAsync(x => x.Name == product.Name))
                throw new CustomException($"A product with the name {product.Name} already exists.");

            // Make sure that the category exist, before adding the product to the database
            await _categories.GetCategoryByIdAsync(product.CategoryId, ct);

            // Everything looks good - Add the new product to the database
            _db.Products.Add(product);
            await _db.SaveChangesAsync(ct);
            return product;
        }

        #endregion Create (C)

        #region Read (R)

        /// <summary>
        /// Get a single product by the ID of the product.<br />
        /// If the product doesn't exist in the database a Key Not Found Exception is thrown.
        /// </summary>
        /// <param name="productId">ID of the product to find in the database</param>
        /// <returns>A single product matching the provided ID in the request</returns>
        public async Task<Product> GetProductByIdAsync(Guid productId, CancellationToken ct)
        {
            // Request helper method to get the product
            return await getProductByIdAsync(productId, ct);
        }

        /// <summary>
        /// Get a single product by it's name.<br />
        /// This is possible because a product name can only exist in the database one time.
        /// </summary>
        /// <param name="name">Name of the product</param>
        /// <returns>A single product matching the name specified for the request</returns>
        public async Task<Product> GetProductByNameAsync(string name, CancellationToken ct)
        {
            // Request helper method to get the product
            return await getProductByNameAsync(name, ct);
        }

        /// <summary>
        /// Get all products from the database.<br />
        /// If no products are available we will trow a Key Not Found Exception with an error message.<br />
        /// If we got 1 or more products, we will return the products to the requesting method.
        /// </summary>
        /// <returns>A list of products from the database</returns>
        /// <exception cref="KeyNotFoundException">Thrown if no products are available in the database</exception>
        public async Task<List<Product>> GetProductsAsync(CancellationToken ct)
        {
            // Get the products from the database
            List<Product> products = await _db.Products
                .AsNoTracking()
                .ToListAsync(ct);

            // Check that we actually got some products from the database
            if (products.Count() == 0)
            {
                // No products were available in the database
                throw new KeyNotFoundException("There are no products in the database. Please add one or more products and try again.");
            }

            // We got 1 or more products to return
            return products;
        }

        /// <summary>
        /// Get all products within a specific category.<br />
        /// The method will check if the category exist before requesting products from the database.<br />
        /// If the category exist in the database, we will request all products with that category id and make sure that we actually got some products.<br />
        /// If no products are returned, we will throw a new Custom Exception. If we got some products for that category, we will return them.
        /// </summary>
        /// <param name="categoryId">The category to get the products for</param>
        /// <returns>A list of products within a specific category</returns>
        /// <exception cref="KeyNotFoundException">Thrown if no products are available within that specific category</exception>
        public async Task<List<Product>> GetProductsByCategoryIdAsync(Guid categoryId, CancellationToken ct)
        {
            // Make sure that the category exists before requesting products
            Category category = await _categories.GetCategoryByIdAsync(categoryId, ct);

            // Get all products that has the specific category id applied
            List<Product> products = await _db.Products
                .AsNoTracking()
                .Where(x => x.CategoryId == categoryId)
                .ToListAsync(ct);

            // Make sure we got some products
            if (products.Count() == 0)
            {
                throw new KeyNotFoundException($"The category {category.Name} contains 0 products. Add one or more products to the category and try again.");
            }

            // Return the products
            return products;
        }

        #endregion Read (R)

        #region Update (U)

        /// <summary>
        /// This method will start by checking if the product exist in the database.<br />
        /// It will then continue to validate the product to update and make sure that no product already exist with the same name
        /// and that the product we would like to update isn't of the same name as the update data.<br />
        /// Finally it will update the entity in the database and return the updated values.
        /// </summary>
        /// <param name="productToUpdate">The product we would like to update</param>
        /// <returns>The updated product</returns>
        /// <exception cref="CustomException">Thrown if a product in the database with the same name already exists</exception>
        public async Task<Product> UpdateProductAsync(Product productToUpdate, CancellationToken ct)
        {
            // Make sure that the product actually exist in the database
            Product product = await getProductByIdAsync(productToUpdate.Id, ct);

            // Validation before we update the product
            // We do not want any product in the database with the same names
            if (product.Name != productToUpdate.Name && await _db.Products.AnyAsync(x => x.Name == productToUpdate.Name))
            {
                throw new CustomException($"A product with the name {productToUpdate.Name} already exist in the database. Please choose another name for the product and try again.");
            }

            // Validation has no problems, let's continue
            _db.Products.Update(productToUpdate);
            await _db.SaveChangesAsync(ct);
            return productToUpdate;
        }

        #endregion Update (U)

        #region Delete (D)

        /// <summary>
        ///  This method will request the helper method to make sure that the product exists in the database<br />
        ///  If the product exist it will be removed and the database will be updated.
        /// </summary>
        /// <param name="productId">ID of the product to delete</param>
        public async Task DeleteProductAsync(Guid productId, CancellationToken ct)
        {
            Product product = await getProductByIdAsync(productId, ct);
            _db.Products.Remove(product);
            await _db.SaveChangesAsync(ct);
        }

        #endregion Delete (D)

        #region Helpers

        /// <summary>
        /// Get a specific product by specifying the ID of the product.<br />
        /// A product in the database can only exist once per id.
        /// </summary>
        /// <param name="id">ID of the product to find in the database</param>
        /// <returns>Requested product</returns>
        /// <exception cref="KeyNotFoundException">Thrown if no product in the database has the given ID</exception>
        private async Task<Product> getProductByIdAsync(Guid id, CancellationToken ct)
        {
            Product product = await _db.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, ct);
            if (product == null) throw new KeyNotFoundException($"The product with ID: {id} was not found in the database.");
            return product;
        }

        /// <summary>
        /// Get a specific product by specifying the name of the product.<br />
        /// A product in the database can only exist once per name.
        /// </summary>
        /// <param name="name">Name of the product to find in database</param>
        /// <returns>Requested product</returns>
        /// <exception cref="KeyNotFoundException">Thrown if the product wasn't found in the database</exception>
        private async Task<Product> getProductByNameAsync(string name, CancellationToken ct)
        {
            Product product = await _db.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Name == name, ct);
            if (product == null) throw new KeyNotFoundException($"The product with name: {name} was not found in the database.");
            return product;
        }



        #endregion Helpers
    }
}
