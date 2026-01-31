namespace TaskPulse.Application.Models
{
    public class FileUpload
    {
        public string FileName { get; }
        public string ContentType { get; }
        public Stream Content { get; }

        public FileUpload(
            string fileName,
            string contentType,
            Stream content)
        {
            FileName = fileName;
            ContentType = contentType;
            Content = content;
        }
    }
}
