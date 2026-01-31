using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TaskPulse.Infrastructure.Data.Context;
using TaskPulse.Infrastructure.Observers;

namespace TaskPulse.Infrastructure.BackgroundServices
{
    public class SlaMonitorService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IEnumerable<ISlaExpiredObserver> _observers;

        public SlaMonitorService(
            IServiceScopeFactory scopeFactory,
            IEnumerable<ISlaExpiredObserver> observers)
        {
            _scopeFactory = scopeFactory;
            _observers = observers;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await CheckExpiredSlas(stoppingToken);
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }

        private async Task CheckExpiredSlas(CancellationToken cancellationToken)
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider
                .GetRequiredService<TaskDbContext>();

            var expiredTasks = await context.Tasks
                .Where(x => !x.IsCompleted && x.DueAt < DateTimeOffset.UtcNow)
                .ToListAsync(cancellationToken);

            foreach (var task in expiredTasks)
            {
                foreach (var observer in _observers)
                {
                    await observer.OnSlaExpiredAsync(task);
                }
            }
        }
    }
}
