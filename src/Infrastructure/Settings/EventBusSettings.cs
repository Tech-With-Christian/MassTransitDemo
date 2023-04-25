namespace Infrastructure.Settings
{
    /// <summary>
    /// Settings file for the EventBus
    /// </summary>
    internal class EventBusSettings
    {
        public string EventbusProvider { get; set; } = "rabbitmq";
        public AzureServiceBusSettings? AzureServiceBusSettings { get; set; } = new AzureServiceBusSettings();
        public RabbitmqSettings? RabbitmqSettings { get; set;} = new RabbitmqSettings();
    }

    internal class AzureServiceBusSettings
    {
        /// <summary>
        /// Primary Connection String for Azure Service Bus.<br />
        /// This can be achieved from the service bus IAM in the Azure Portal.
        /// </summary>
        public string ConnectionString { get; set; } 
    }

    internal class RabbitmqSettings
    {
        /// <summary>
        /// Event Bus Host Address. By default set to "localhost".
        /// </summary>
        public string Host { get; set; } = "localhost";

        /// <summary>
        /// Event Bus Port. By default set to 5672.
        /// </summary>
        public int Port { get; set; } = 5672;

        /// <summary>
        /// Event Bus Username. By default set to "guest".
        /// </summary>
        public string Username { get; set; } = "guest";

        /// <summary>
        /// Event Bus Password. By default set to "guest".
        /// </summary>
        public string Password { get; set; } = "guest";

        /// <summary>
        /// Event Bus Virtual Host. By default set to "/".
        /// </summary>
        public string VirtualHost { get; set; } = "/";
    }
}
