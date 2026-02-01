using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskPulse.Application.Abstractions.Repositories;
using TaskPulse.Application.Abstractions.Storage;
using TaskPulse.Infrastructure.BackgroundServices;
using TaskPulse.Infrastructure.Data;
using TaskPulse.Infrastructure.Data.Repositories;
using TaskPulse.Infrastructure.Observers;
using TaskPulse.Infrastructure.Storage;

namespace TaskPulse.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration,
            bool enableBackgroundServices = true)
        {
            // Repositories
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();

            // File storage
            services.AddScoped<IFileStorage, LocalFileStorage>();            

            if (enableBackgroundServices)
            {
                // Database
                services.AddDbContext<TaskPulseDbContext>(options =>
                    options.UseNpgsql(
                        configuration.GetConnectionString("DefaultConnection"))
                );

                // SLA observers
                services.AddScoped<ISlaExpiredObserver, DatabaseSlaExpiredObserver>();
                services.AddHostedService<SlaMonitorService>();
            }

            return services;
        }
    }
}
