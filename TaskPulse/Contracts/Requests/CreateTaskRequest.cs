using System.ComponentModel.DataAnnotations;

namespace TaskPulse.API.Contracts.Requests
{
    public class CreateTaskRequest
    {
        private const long MaxFileSize = 5 * 1024 * 1024; // 5MB

        [Required]
        public string Title { get; set; } = null!;

        [Range(1, 720)]
        public int SlaHours { get; set; }

        public IFormFile? File { get; set; } = null!;

        public bool IsFileSizeValid()
            => File == null || File.Length <= MaxFileSize;
    }
}
