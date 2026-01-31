using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskPulse.Application.Abstractions.Repositories;
using TaskPulse.Domain.Entities;
using TaskPulse.Infrastructure.Data.Context;

namespace TaskPulse.Infrastructure.Data.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly NotificationDbContext _context;

        public NotificationRepository(NotificationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Notification>> GetAsync()
        {
            var query = _context.Notifications.AsNoTracking();

            return await query
                .OrderBy(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task UpdateAsync(Notification task)
        {
            await _context.SaveChangesAsync();
        }
    }
}
