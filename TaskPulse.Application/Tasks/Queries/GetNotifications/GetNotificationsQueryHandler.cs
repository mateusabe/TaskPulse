using MediatR;
using TaskPulse.Application.Abstractions.Repositories;
using TaskPulse.Domain.Entities;

namespace TaskPulse.Application.Tasks.Queries.GetNotifications
{
    public class GetNotificationsQueryHandler : IRequestHandler<GetNotificationsQuery, List<Notification>>
    {
        private readonly INotificationRepository _repository;

        public GetNotificationsQueryHandler(INotificationRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Notification>> Handle(
            GetNotificationsQuery request,
            CancellationToken cancellationToken)
        {
            return await _repository.GetAsync();
        }
    }
}
