using Infrastructure.Services.Events.Consumers;
using Infrastructure.Settings;
using MassTransit;
using MessageBus.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Infrastructure.Services.Events
{
    internal static class Startup
    {
        internal static IServiceCollection AddEventBusService(this IServiceCollection services, IConfiguration configuration)
        {
            // Get the event bus settings from the configuration
            EventBusSettings? settings = configuration.GetSection(nameof(EventBusSettings)).Get<EventBusSettings>();

            if (settings == null)
            {
                throw new NullReferenceException("The Event Bus Settings has not been configured. Please check the settings and update them.");
            }

            Log.Information($"Registering MassTransit Service for API.");

            services.AddMassTransit(config =>
            {
                config.AddConsumer<ProductCreatedConsumer>();

                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(settings.Host, x =>
                    {
                        x.Username(settings.Username);
                        x.Password(settings.Password);
                    });

                    // Set up receiver endpoint for the ProductCreated event
                    // using the contancts from the messagebus library
                    cfg.ReceiveEndpoint(EventBusConstants.ProductCreatedQueue, c =>
                    {
                        c.ConfigureConsumer<ProductCreatedConsumer>(ctx);
                    });

                    // Add all consumers here...

                });
            });

            // Register the consumers
            Log.Information("Registering Message Queue Consumers");
            services.AddScoped<ProductCreatedConsumer>();

            Log.Information($"Infrastructure is now ready to consume messages at Message Broker {settings.Host}.");
            return services;
        }
    }
}
