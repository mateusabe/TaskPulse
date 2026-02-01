using MediatR;
using TaskPulse.Application.Abstractions.Repositories;
using TaskPulse.Application.Abstractions.Storage;
using TaskPulse.Domain.Entities;
using TaskPulse.Domain.ValueObjects;

namespace TaskPulse.Application.Tasks.Commands.CreateTask
{
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, TaskEntity>
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

        public async Task<TaskEntity> Handle(
            CreateTaskCommand request,
            CancellationToken cancellationToken)
        {
            var sla = new Sla(request.SlaHours);
            var now = DateTimeOffset.UtcNow;
            var filePath = string.Empty;   

            if(request.File != null) filePath = await _fileStorage.SaveAsync(request.File, cancellationToken);

            var task = new TaskEntity(
                request.Title,
                sla,
                filePath,
                now
            );

            await _repository.AddAsync(task);

            return task;
        }
    }
}
