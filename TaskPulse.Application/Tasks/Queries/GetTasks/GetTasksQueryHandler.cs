using MediatR;
using TaskPulse.Application.Abstractions.Repositories;
using TaskPulse.Domain.Entities;

namespace TaskPulse.Application.Tasks.Queries.GetTasks
{
    public class GetTasksQueryHandler : IRequestHandler<GetTasksQuery, List<TaskEntity>>
    {
        private readonly ITaskRepository _repository;

        public GetTasksQueryHandler(ITaskRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<TaskEntity>> Handle(
            GetTasksQuery request,
            CancellationToken cancellationToken)
        {
            return await _repository.GetAsync(request.Completed);
        }
    }
}
