namespace TaskPulse.API.Contracts.Responses
{
    public class NotificationResponse
    {
        public Guid Id { get; set; }
        public Guid TaskId { get; set; }
        public string Message { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public bool IsRead { get; set; }
        public string TaskTitle { get; set; }
        public bool IsSlaBreached { get; set; }
    }
}
