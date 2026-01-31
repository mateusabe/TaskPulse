using MediatR;
using TaskPulse.Application.Models;

namespace TaskPulse.Application.Tasks.Commands.CreateTask
{

    public record CreateTaskCommand(
        string Title,
        int SlaHours,
        FileUpload File
    ) : IRequest<Guid>;
}
