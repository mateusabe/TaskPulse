using TaskPulse.Domain.Entities;

namespace TaskPulse.Application.Abstractions.Repositories
{
    public interface ITaskRepository
    {
        Task AddAsync(TaskEntity task);
        Task<TaskEntity?> GetByIdAsync(Guid id);
        Task<List<TaskEntity>> GetAsync(bool? completed);
        Task UpdateAsync(TaskEntity task);
    }
}
