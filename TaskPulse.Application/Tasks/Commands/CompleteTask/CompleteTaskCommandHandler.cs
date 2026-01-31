using MediatR;
using TaskPulse.Application.Abstractions.Repositories;

namespace TaskPulse.Application.Tasks.Commands.CompleteTask
{
    public class CompleteTaskCommandHandler : IRequestHandler<CompleteTaskCommand>
    {
        private readonly ITaskRepository _repository;

        public CompleteTaskCommandHandler(ITaskRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(
            CompleteTaskCommand request,
            CancellationToken cancellationToken)
        {
            var task = await _repository.GetByIdAsync(request.TaskId);

            if (task is null)
                throw new InvalidOperationException("Tarefa não encontrada");

            task.Complete(DateTimeOffset.UtcNow);

            await _repository.UpdateAsync(task);
        }
    }
}
