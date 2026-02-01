using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskPulse.Application.Abstractions.Repositories;
using TaskPulse.Application.Tasks.Queries.GetTasks;
using TaskPulse.Domain.Entities;

namespace TaskPulse.Application.Tasks.Queries.GetTask
{
    public class GetTaskByIdQueryHandler
    : IRequestHandler<GetTaskByIdQuery, TaskEntity>
    {
        private readonly ITaskRepository _repository;

        public GetTaskByIdQueryHandler(ITaskRepository repository)
        {
            _repository = repository;
        }

        public async Task<TaskEntity> Handle(
            GetTaskByIdQuery request,
            CancellationToken cancellationToken)
        {
            var task = await _repository.GetByIdAsync(
                request.id);

            if (task is null)
                throw new KeyNotFoundException(
                    $"Task com id {request.id} não encontrada.");

            return task;
        }
    }

}
