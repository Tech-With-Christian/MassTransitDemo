namespace MessageBus.Messages
{
    /// <summary>
    /// Base event for integration events
    /// </summary>
    public class IntegrationBaseEvent
    {
        /// <summary>
        /// Constructor to generate a new id and set the creation date
        /// </summary>
        public IntegrationBaseEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.Now;
        }

        /// <summary>
        /// Constructor if you got an external method settings the properties.
        /// </summary>
        /// <param name="id">Event ID</param>
        /// <param name="creationDate">Event Creation Date</param>
        public IntegrationBaseEvent(Guid id, DateTime creationDate)
        {
            Id = id;
            CreationDate = creationDate;
        }

        /// <summary>
        /// Event ID
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Event Creation Date
        /// </summary>
        public DateTime CreationDate { get; private set; }
    }
}
