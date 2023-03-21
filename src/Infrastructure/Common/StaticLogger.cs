using Serilog;

namespace Infrastructure.Common
{
    public static class StaticLogger
    {
        /// <summary>
        /// Make sure that Serilog has been initialized for logging in the application.
        /// If not this will initialize it with a default configuration.
        /// </summary>
        public static void EnsureLoggerIsInitialized()
        {
            // Check if the logger is the one of type Serilog
            if (Log.Logger is not Serilog.Core.Logger)
            {
                Log.Logger = new LoggerConfiguration()
                    .Enrich.FromLogContext()
                    .WriteTo.Console() // By default we only log to the console
                    .CreateLogger();
            }
        }
    }
}
