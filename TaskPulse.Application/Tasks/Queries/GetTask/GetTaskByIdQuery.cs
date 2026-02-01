using MediatR;
using TaskPulse.Domain.Entities;

namespace TaskPulse.Application.Tasks.Queries.GetTask
{
    public record GetTaskByIdQuery(Guid id)
    : IRequest<TaskEntity>;
}
