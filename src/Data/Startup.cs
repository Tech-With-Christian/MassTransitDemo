using Data.Database;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Data
{
    public static class Startup
    {
        /// <summary>
        /// Register Database Context for service container (DI)<br />
        /// By default the database will be registered with an in-memory database.<br />
        /// This can be changed with other database connectors like MS SQL, PostgreSQL, MySQL, etc.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            // Add Database Context for the application
            Log.Information("Adding Database Context to application");
            services.AddDbContext<ApplicationDbContext>();
            return services;
        }
    }
}
