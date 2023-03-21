using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Infrastructure.Settings
{
    internal static class Startup
    {
        internal static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration configuration)
        {
            Log.Information($"Configuring settings for application.");

            // Configure settings for the event bus
            services.Configure<EventBusSettings>(options => configuration.GetSection(nameof(EventBusSettings)).Bind(options));

            return services;
        }
    }
}
