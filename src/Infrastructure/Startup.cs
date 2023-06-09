﻿using Data;
using Infrastructure.Middleware;
using Infrastructure.Services;
using Infrastructure.Services.Events;
using Infrastructure.Settings;
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
            services.AddSettings(configuration);
            services.AddServices();
            services.AddDatabase();
            services.AddEventBusService(configuration);
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
