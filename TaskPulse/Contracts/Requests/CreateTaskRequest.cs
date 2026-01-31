using System.ComponentModel.DataAnnotations;

namespace TaskPulse.API.Contracts.Requests
{
    public class CreateTaskRequest
    {
        [Required]
        public string Title { get; set; } = null!;

        [Range(1, 720)]
        public int SlaHours { get; set; }

        public IFormFile? File { get; set; } = null!;
    }
}
