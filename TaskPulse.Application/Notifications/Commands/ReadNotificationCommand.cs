using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskPulse.Application.Notifications.Commands
{
    public record ReadNotificationCommand(Guid NotificationId)
    : IRequest;
}
