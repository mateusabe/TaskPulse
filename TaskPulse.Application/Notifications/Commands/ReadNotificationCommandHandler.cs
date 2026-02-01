using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskPulse.Application.Abstractions.Repositories;
using TaskPulse.Application.Tasks.Commands.CompleteTask;

namespace TaskPulse.Application.Notifications.Commands
{
    public class ReadNotificationCommandHandler : IRequestHandler<ReadNotificationCommand>
    {
        private readonly INotificationRepository _repository;

        public ReadNotificationCommandHandler(INotificationRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(
            ReadNotificationCommand request,
            CancellationToken cancellationToken)
        {
            var notification = await _repository.GetByIdAsync(request.NotificationId);

            if (notification is null)
                throw new InvalidOperationException("Tarefa não encontrada");

            notification.MarkAsRead(DateTimeOffset.UtcNow);

            await _repository.UpdateAsync(notification);
        }
    }
}
