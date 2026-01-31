using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskPulse.Application.Abstractions.Repositories;
using TaskPulse.Application.Abstractions.Storage;
using TaskPulse.Infrastructure.BackgroundServices;
using TaskPulse.Infrastructure.Data.Context;
using TaskPulse.Infrastructure.Data.Repositories;
using TaskPulse.Infrastructure.Observers;
using TaskPulse.Infrastructure.Storage;

namespace TaskPulse.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // Database
            services.AddDbContext<TaskDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("DefaultConnection"))
            );

            // Repositories
            services.AddScoped<ITaskRepository, TaskRepository>();

            // File storage
            services.AddScoped<IFileStorage, LocalFileStorage>();

            // SLA observers
            services.AddSingleton<ISlaExpiredObserver, LogSlaExpiredObserver>();

            // Background service
            services.AddHostedService<SlaMonitorService>();

            return services;
        }
    }
}
