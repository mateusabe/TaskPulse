using MediatR;
using TaskPulse.Domain.Entities;

namespace TaskPulse.Application.Notifications.Queries.GetNotifications
{
    public record GetNotificationsQuery()
    : IRequest<List<Notification>>;
}
