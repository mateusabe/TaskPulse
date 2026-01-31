using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskPulse.Domain.Entities;

namespace TaskPulse.Infrastructure.Data.Context
{
    public class NotificationDbContext : DbContext
    {
        public DbSet<Notification> Notifications => Set<Notification>();

        public NotificationDbContext(DbContextOptions<TaskDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(TaskDbContext).Assembly);
        }
    }
}
