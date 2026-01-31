using Microsoft.Extensions.Logging;
using TaskPulse.Domain.Entities;

namespace TaskPulse.Infrastructure.Observers
{
    public class LogSlaExpiredObserver : ISlaExpiredObserver
    {
        private readonly ILogger<LogSlaExpiredObserver> _logger;

        public LogSlaExpiredObserver(ILogger<LogSlaExpiredObserver> logger)
        {
            _logger = logger;
        }

        public Task OnSlaExpiredAsync(TaskEntity task)
        {
            _logger.LogWarning(
                "SLA expirado para tarefa {TaskId} - {Title}",
                task.Id,
                task.Title
            );

            return Task.CompletedTask;
        }
    }
}
