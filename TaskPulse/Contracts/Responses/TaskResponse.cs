namespace TaskPulse.API.Contracts.Responses
{
    public class TaskResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset DueAt { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsSlaBreached { get; set; }
    }
}
