using Logger.Interfaces;
using Logger.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace Logger
{
    public static class LoggerRegistration
    {
        public static void AddEventLogger(this IServiceCollection services)
        {
            services.AddScoped<IEventLogger, EventLoggerToFile>();
        }
    }
}
