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

            switch (settings.EventbusProvider.ToLowerInvariant())
            {
                case "azure":
                    services._addAzureServiceBus(settings);
                    break;
                case "rabbitmq":
                    services._addRabbitMqServiceBus(settings);
                    break;
                default:
                    throw new NullReferenceException($"The Event Bus Provider {settings.EventbusProvider} is not supported. Please check the settings and update them.");
            }

            // Register Consumers for DI
            services._registerConsumers();

            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        /// <param name="settings">EventBusSettings</param>
        /// <returns>Updated IServiceCollection with Azure Service Bus</returns>
        /// <exception cref="NullReferenceException">Thrown if settings from the configuration is missing</exception>
        private static IServiceCollection _addAzureServiceBus(this IServiceCollection services, EventBusSettings settings)
        {
            if (settings?.AzureServiceBusSettings == null)
            {
                throw new NullReferenceException("The Azure Service Bus Settings has not been configured.");
            }

            services.AddMassTransit(config =>
            {
                config.AddConsumer<ProductCreatedConsumer>();

                config.UsingAzureServiceBus((ctx, cfg) =>
                {
                    cfg.Host(settings.AzureServiceBusSettings.ConnectionString ?? throw new NullReferenceException("The connection string for Azure Service Bus has not been configured."));

                    cfg.ReceiveEndpoint(EventBusConstants.ProductCreatedQueue, consumer =>
                    {
                        consumer.ConfigureConsumer<ProductCreatedConsumer>(ctx);
                    });

                    // Add all consumers here...
                });
            });

            Log.Information($"Infrastructure is now ready to consume and produce messages using Azure as the Service Bus.");
            return services;
        }

        /// <summary>
        /// Add RabbitMq Service Bus and consumers for application.
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        /// <param name="settings">EventBusSettings</param>
        /// <returns>Updated IServiceCollection with RabbitMq Service Bus</returns>
        /// <exception cref="NullReferenceException">Thrown if settings from the configuration is missing</exception>
        private static IServiceCollection _addRabbitMqServiceBus(this IServiceCollection services, EventBusSettings settings)
        {
            if (settings?.RabbitmqSettings == null)
            {
                throw new NullReferenceException("The RabbitMQ Settings has not been configured.");
            }

            services.AddMassTransit(config =>
            {
                config.AddConsumer<ProductCreatedConsumer>();

                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(settings.RabbitmqSettings.Host ?? throw new NullReferenceException("The host has not been specififed for RabbitMQ"), x =>
                    {
                        x.Username(settings.RabbitmqSettings.Username ?? throw new NullReferenceException("The username has not been specififed for RabbitMQ"));
                        x.Password(settings.RabbitmqSettings.Password ?? throw new NullReferenceException("The password has not been specififed for RabbitMQ"));
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

            Log.Information($"Infrastructure is now ready to consume and produce messages using RabbitMQ as the Service Bus.");
            return services;
        }

        /// <summary>
        /// Register Consumers service lifetime here
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        /// <returns>The updated IServiceCollection</returns>
        private static IServiceCollection _registerConsumers(this IServiceCollection services)
        {
            Log.Information("Registering Message Queue Consumers");
            services.AddScoped<ProductCreatedConsumer>();
            Log.Information($"Added {nameof(ProductCreatedConsumer)}");
            // Add other consumers here

            return services;
        }
    }
}
