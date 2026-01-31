using TaskPulse.Domain.Entities;

namespace TaskPulse.Infrastructure.Observers
{
    public interface ISlaExpiredObserver
    {
        Task OnSlaExpiredAsync(TaskEntity task);
    }
}
