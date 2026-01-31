using TaskPulse.Application.Abstractions.Storage;
using TaskPulse.Application.Models;

namespace TaskPulse.Infrastructure.Storage
{
    public class LocalFileStorage : IFileStorage
    {
        private readonly string _basePath = "uploads";

        public async Task<string> SaveAsync(
            FileUpload file,
            CancellationToken cancellationToken)
        {
            Directory.CreateDirectory(_basePath);

            var filePath = Path.Combine(
                _basePath,
                $"{Guid.NewGuid()}_{file.FileName}");

            using var stream = new FileStream(filePath, FileMode.Create);
            await file.Content.CopyToAsync(stream, cancellationToken);

            return filePath;
        }
    }
}
