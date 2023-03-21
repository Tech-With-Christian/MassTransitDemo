using Domain.Entitites.Categories;

namespace Domain.Entitites.Products
{
    public class Product : Base
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; } = 0;
        public int Stock { get; set; } = 0;

        // Relationship
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
