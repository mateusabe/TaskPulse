using MediatR;
using TaskPulse.Domain.Entities;

namespace TaskPulse.Application.Tasks.Queries.GetTasks
{
    public record GetTasksQuery(bool? Completed)
    : IRequest<List<TaskEntity>>;
}
