using Data;
using Infrastructure.Middleware;
using Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Infrastructure
{
    public static class Startup
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            Log.Information("Adding Infrastructure services");
            services.AddServices();
            services.AddDatabase();
            return services;
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app, IConfiguration configuration)
        {
            Log.Information("Using Infrastructure services");
            app.UseMiddleware<ErrorHandlerMiddleware>();
            return app;
        }
    }
}
