using TaskPulse.Domain.Entities;
using TaskPulse.Infrastructure.Data.Context;

namespace TaskPulse.Infrastructure.Observers
{
    public class DatabaseSlaExpiredObserver : ISlaExpiredObserver
    {
        private readonly NotificationDbContext _context;

        public DatabaseSlaExpiredObserver(NotificationDbContext context)
        {
            _context = context;
        }

        public async Task OnSlaExpiredAsync(TaskEntity task)
        {
            _context.Notifications.Add(
                new Notification(task.Id, "SLA expirado", DateTimeOffset.Now)
            );

            await _context.SaveChangesAsync();
        }
    }
}
