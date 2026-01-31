using MediatR;
using TaskPulse.Domain.Entities;

namespace TaskPulse.Application.Tasks.Queries.GetNotifications
{
    public record GetNotificationsQuery()
    : IRequest<List<Notification>>;
}
