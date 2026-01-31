using MediatR;
using TaskPulse.Application.Abstractions.Repositories;
using TaskPulse.Application.Abstractions.Storage;
using TaskPulse.Domain.Entities;
using TaskPulse.Domain.ValueObjects;

namespace TaskPulse.Application.Tasks.Commands.CreateTask
{
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Guid>
    {
        private readonly ITaskRepository _repository;
        private readonly IFileStorage _fileStorage;

        public CreateTaskCommandHandler(
            ITaskRepository repository,
            IFileStorage fileStorage)
        {
            _repository = repository;
            _fileStorage = fileStorage;
        }

        public async Task<Guid> Handle(
            CreateTaskCommand request,
            CancellationToken cancellationToken)
        {
            var sla = new Sla(request.SlaHours);
            var now = DateTime.UtcNow;

            var filePath = await _fileStorage.SaveAsync(request.File);

            var task = new TaskEntity(
                request.Title,
                sla,
                filePath,
                now
            );

            await _repository.AddAsync(task);

            return task.Id;
        }
    }
}
