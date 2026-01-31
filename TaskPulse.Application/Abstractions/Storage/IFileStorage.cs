using TaskPulse.Application.Models;

namespace TaskPulse.Application.Abstractions.Storage
{
    public interface IFileStorage
    {
        Task<string> SaveAsync(FileUpload file);
    }
}
