using MediatR;
using TaskPulse.Application.Models;
using TaskPulse.Domain.Entities;

namespace TaskPulse.Application.Tasks.Commands.CreateTask
{

    public record CreateTaskCommand(
        string Title,
        int SlaHours,
        FileUpload? File
    ) : IRequest<TaskEntity>;
}
