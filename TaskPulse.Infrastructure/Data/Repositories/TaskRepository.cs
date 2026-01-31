using Microsoft.EntityFrameworkCore;
using TaskPulse.Application.Abstractions.Repositories;
using TaskPulse.Domain.Entities;
using TaskPulse.Infrastructure.Data.Context;

namespace TaskPulse.Infrastructure.Data.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskDbContext _context;

        public TaskRepository(TaskDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TaskEntity task)
        {
            await _context.Tasks.AddAsync(task);
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
