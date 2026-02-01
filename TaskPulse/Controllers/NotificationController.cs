using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskPulse.API.Contracts.Responses;
using TaskPulse.Application.Notifications.Commands;
using TaskPulse.Application.Notifications.Queries.GetNotifications;

namespace TaskPulse.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class NotificationController : Controller
    {
        private readonly IMediator _mediator;

        public NotificationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<NotificationResponse>> Get()
        {
            var tasks = await _mediator.Send(
                new GetNotificationsQuery());

            var response = tasks.Select(x => new NotificationResponse
            {
                Id = x.Id,
                Message = x.Message,
                CreatedAt = x.CreatedAt,
                IsRead = x.IsRead,
                TaskId = x.TaskId,
                TaskTitle = x.TaskInfo.Title,
            }).ToList();

            return response;
        }

        [HttpPut("{notificationId}/read")]
        public async Task<IActionResult> MarkAsRead(Guid notificationId)
        {
            await _mediator.Send(
                new ReadNotificationCommand(notificationId));
            return NoContent();
        }
    }
}
