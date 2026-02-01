using Microsoft.EntityFrameworkCore;
using TaskPulse.Application.Abstractions.Repositories;
using TaskPulse.Domain.Entities;

namespace TaskPulse.Infrastructure.Data.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskPulseDbContext _context;

        public TaskRepository(TaskPulseDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TaskEntity task)
        {
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
        }

        public async Task<TaskEntity?> GetByIdAsync(Guid id)
        {
            return await _context.Tasks
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<TaskEntity>> GetAsync(bool? completed)
        {
            var query = _context.Tasks.AsNoTracking();

            if (completed.HasValue)
                query = query.Where(x => x.IsCompleted == completed);

            return await query
                .OrderBy(x => x.DueAt)
                .ToListAsync();
        }

        public async Task UpdateAsync(TaskEntity task)
        {
            await _context.SaveChangesAsync();
        }
    }
}
