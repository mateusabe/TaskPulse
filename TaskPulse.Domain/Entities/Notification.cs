using TaskPulse.Domain.Exceptions;

namespace TaskPulse.Domain.Entities
{
    public class Notification
    {
        public Guid Id { get; }
        public Guid TaskId { get; }
        public string Message { get; private set; }
        public DateTimeOffset CreatedAt { get; }
        public DateTimeOffset ReadAt { get; private set; }
        public bool IsRead { get; private set; } = false;
        public TaskEntity TaskInfo { get; set; }

        public Notification(
            Guid taskId,
            string message,
            DateTimeOffset createdAt)
        {
            if(string.IsNullOrWhiteSpace(message))
                throw new DomainException("Mensagem nula ou vazia");

            if(message.Length > 500)
                throw new DomainException("Mensagem excede o tamanho máximo de 500 caracteres");

            Id = Guid.NewGuid();
            Message = message;
            TaskId = taskId;
            CreatedAt = createdAt;
        }

        public void MarkAsRead(DateTimeOffset readAt)
        {
            if (IsRead) return;

            IsRead = true;
            ReadAt = readAt;
        }
    }
}
