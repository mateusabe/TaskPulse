using TaskPulse.Domain.Exceptions;
using TaskPulse.Domain.ValueObjects;

namespace TaskPulse.Domain.Entities
{
    public class TaskEntity
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset DueAt { get; private set; }
        public bool IsCompleted { get; private set; }
        public DateTimeOffset? CompletedAt { get; private set; }
        public string AttachmentPath { get; private set; }

        private TaskEntity() { } // EF

        public TaskEntity(
            string title,
            Sla sla,
            string attachmentPath,
            DateTimeOffset now)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new DomainException("Título da tarefa é obrigatório");

            Id = Guid.NewGuid();
            Title = title;
            CreatedAt = now;
            DueAt = sla.CalculateDueDate(now);
            AttachmentPath = attachmentPath;
            IsCompleted = false;
        }

        public void Complete(DateTimeOffset now)
        {
            if (IsCompleted)
                throw new DomainException("Tarefa já está concluída");

            IsCompleted = true;
            CompletedAt = now;
        }

        public bool IsSlaExpired(DateTimeOffset now)
            => !IsCompleted && now > DueAt;
    }
}
