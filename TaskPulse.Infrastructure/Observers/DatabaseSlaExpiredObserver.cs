using MediatR;
using TaskPulse.Application.Abstractions.Repositories;
using TaskPulse.Domain.Entities;
using TaskPulse.Infrastructure.Data;

namespace TaskPulse.Infrastructure.Observers
{
    public class DatabaseSlaExpiredObserver : ISlaExpiredObserver
    {
        private readonly TaskPulseDbContext _context;
        private readonly INotificationRepository _repository;

        public DatabaseSlaExpiredObserver(TaskPulseDbContext context, INotificationRepository repository)
        {
            _context = context;
            _repository = repository;
        }

        public async Task OnSlaExpiredAsync(TaskEntity task)
        {
            var notification = await _repository.GetByTaskIdAsync(task.Id);

            if (notification != null && notification?.Count > 0) return;

            _context.Notifications.Add(
                new Notification(task.Id, "SLA expirado", DateTimeOffset.UtcNow)
            );

            await _context.SaveChangesAsync();
        }
    }
}
