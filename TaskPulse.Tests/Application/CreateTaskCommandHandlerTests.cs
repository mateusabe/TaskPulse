using Moq;
using NUnit.Framework;
using TaskPulse.Application.Abstractions.Repositories;
using TaskPulse.Application.Abstractions.Storage;
using TaskPulse.Application.Models;
using TaskPulse.Application.Tasks.Commands.CreateTask;
using TaskPulse.Domain.Entities;

namespace TaskPulse.Tests.Application
{
    public class CreateTaskCommandHandlerTests
    {
        [Test]
        public async Task Should_Create_Task()
        {
            // Arrange
            var repo = new Mock<ITaskRepository>();
            var storage = new Mock<IFileStorage>();
            CancellationToken ct = default;

            storage
                .Setup(x => x.SaveAsync(It.IsAny<FileUpload>(), ct))
                .ReturnsAsync("file.txt");

            var handler = new CreateTaskCommandHandler(
                repo.Object,
                storage.Object
            );

            var file = new FileUpload(
                "file.txt",
                "text/plain",
                new MemoryStream("conteudo"u8.ToArray())
            );

            var command = new CreateTaskCommand(
                "Teste",
                2,
                file
            );

            // Act
            var id = await handler.Handle(command, default);

            // Assert
            repo.Verify(x => x.AddAsync(It.IsAny<TaskEntity>()), Times.Once);
            Assert.That(id, Is.Not.EqualTo(Guid.Empty));
        }
    }
}
