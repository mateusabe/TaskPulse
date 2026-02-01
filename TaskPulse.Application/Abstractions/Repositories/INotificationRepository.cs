using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskPulse.Domain.Entities;

namespace TaskPulse.Application.Abstractions.Repositories
{
    public interface INotificationRepository
    {
        Task<List<Notification>> GetAsync();
        Task<Notification?> GetByIdAsync(Guid id);
        Task<List<Notification>?> GetByTaskIdAsync(Guid taskId);
        Task UpdateAsync(Notification notification);
    }
}
