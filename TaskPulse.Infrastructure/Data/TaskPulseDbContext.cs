using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskPulse.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace TaskPulse.Infrastructure.Data
{
    public class TaskPulseDbContext : DbContext
    {
        public DbSet<TaskEntity> Tasks => Set<TaskEntity>();
        public DbSet<Notification> Notifications => Set<Notification>();

        public TaskPulseDbContext(DbContextOptions<TaskPulseDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(TaskPulseDbContext).Assembly);
        }
    }
}
