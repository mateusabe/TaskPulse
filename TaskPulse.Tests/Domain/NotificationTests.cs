using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskPulse.Domain.Entities;
using TaskPulse.Domain.Exceptions;

namespace TaskPulse.Tests.Domain
{
    public class NotificationTests
    {
        [Test]
        public void Should_Create_Notification_With_Valid_Data()
        {
            // Arrange
            var taskId = Guid.NewGuid();
            var message = "SLA expirado";
            var createdAt = DateTimeOffset.UtcNow;

            // Act
            var notification = new Notification(
                taskId,
                message,
                createdAt
            );

            // Assert
            Assert.That(notification.Id, Is.Not.EqualTo(Guid.Empty));
            Assert.That(notification.TaskId, Is.EqualTo(taskId));
            Assert.That(notification.Message, Is.EqualTo(message));
            Assert.That(notification.CreatedAt, Is.EqualTo(createdAt));
            Assert.False(notification.IsRead);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        public void Should_Throw_When_Message_Is_Null_Or_Empty(string message)
        {
            var taskId = Guid.NewGuid();

            Assert.Throws<DomainException>(() =>
                new Notification(
                    taskId,
                    message,
                    DateTimeOffset.UtcNow
                )
            );
        }

        [Test]
        public void Should_Throw_When_Message_Exceeds_Max_Length()
        {
            var taskId = Guid.NewGuid();
            var longMessage = new string('A', 501);

            Assert.Throws<DomainException>(() =>
                new Notification(
                    taskId,
                    longMessage,
                    DateTimeOffset.UtcNow
                )
            );
        }

        [Test]
        public void Should_Mark_Notification_As_Read()
        {
            // Arrange
            var notification = new Notification(
                Guid.NewGuid(),
                "Nova notificação",
                DateTimeOffset.UtcNow
            );

            var readAt = DateTimeOffset.UtcNow.AddMinutes(1);

            // Act
            notification.MarkAsRead(readAt);

            // Assert
            Assert.True(notification.IsRead);
            Assert.That(notification.ReadAt, Is.EqualTo(readAt));
        }

        [Test]
        public void Should_Not_Change_State_When_Marked_As_Read_Twice()
        {
            // Arrange
            var notification = new Notification(
                Guid.NewGuid(),
                "Mensagem",
                DateTimeOffset.UtcNow
            );

            var firstReadAt = DateTimeOffset.UtcNow.AddMinutes(1);
            var secondReadAt = DateTimeOffset.UtcNow.AddMinutes(5);

            // Act
            notification.MarkAsRead(firstReadAt);
            notification.MarkAsRead(secondReadAt);

            // Assert
            Assert.True(notification.IsRead);
            Assert.That(notification.ReadAt, Is.EqualTo(firstReadAt));
        }
    }
}
