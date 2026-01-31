using MediatR;

namespace TaskPulse.Application.Tasks.Commands.CompleteTask
{
    public record CompleteTaskCommand(Guid TaskId)
    : IRequest;
}
