using Microsoft.EntityFrameworkCore;
using TaskPulse.Domain.Entities;

namespace TaskPulse.Infrastructure.Data.Context
{
    public class TaskDbContext : DbContext
    {
        public DbSet<TaskEntity> Tasks => Set<TaskEntity>();

        public TaskDbContext(DbContextOptions<TaskDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(TaskDbContext).Assembly);
        }
    }
}
