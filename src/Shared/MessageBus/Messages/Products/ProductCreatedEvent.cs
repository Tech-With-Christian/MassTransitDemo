namespace MessageBus.Messages.Products
{
    /// <summary>
    /// Event class to hold values for a newly created product in the API
    /// </summary>
    public class ProductCreatedEvent : IntegrationBaseEvent
    {
        /// <summary>
        /// Product Name
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Product Description
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Product Price
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Amount of products in stock
        /// </summary>
        public int Stock { get; set; }
    }
}
