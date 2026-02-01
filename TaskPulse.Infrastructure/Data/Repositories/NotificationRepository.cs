using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskPulse.Application.Abstractions.Repositories;
using TaskPulse.Domain.Entities;

namespace TaskPulse.Infrastructure.Data.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly TaskPulseDbContext _context;

        public NotificationRepository(TaskPulseDbContext context)
        {
            _context = context;
        }

        public async Task<List<Notification>> GetAsync()
        {
            var notifications = _context.Notifications
                .Include(n => n.TaskInfo)
                .ToList();

            return notifications;
        }

        public async Task<Notification?> GetByIdAsync(Guid id)
        {
            return await _context.Notifications
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Notification>?> GetByTaskIdAsync(Guid taskId)
        {
            return await _context.Notifications
                .Where(x => x.TaskId == taskId)
                .ToListAsync();
        }


        public async Task UpdateAsync(Notification task)
        {
            await _context.SaveChangesAsync();
        }
    }
}
