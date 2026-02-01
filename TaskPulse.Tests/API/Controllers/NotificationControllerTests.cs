using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Net;
using System.Net.Http.Json;
using TaskPulse.API.Contracts.Responses;
using TaskPulse.Domain.Entities;
using TaskPulse.Domain.ValueObjects;
using TaskPulse.Infrastructure.Data;

namespace TaskPulse.Tests.API.Controllers
{
    public class NotificationControllerTests
    {
        private TaskApiFactory _factory = null!;
        private HttpClient _client = null!;

        [OneTimeSetUp]
        public void Setup()
        {
            _factory = new TaskApiFactory();
            _client = _factory.CreateClient();
        }

        [Test]
        public async Task Should_Return_Notifications()
        {
            // Arrange
            // (seed no banco)
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider
                .GetRequiredService<TaskPulseDbContext>();

            var task = new TaskEntity(
                "Teste",
                new Sla(1),
                null,
                DateTimeOffset.UtcNow
            );

            context.Tasks.Add(task);

            var notification = new Notification(
                task.Id,
                "SLA expirado",
                DateTimeOffset.UtcNow
            );

            context.Notifications.Add(notification);
            await context.SaveChangesAsync();

            // Act
            var response = await _client.GetAsync("/api/v1/notification");

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var content = await response.Content
                .ReadFromJsonAsync<List<NotificationResponse>>();

            Assert.That(content, Is.Not.Empty);
            Assert.That(content![0].Message, Is.EqualTo("SLA expirado"));
        }

        [Test]
        public async Task Should_Mark_Notification_As_Read()
        {
            Guid notificationId;

            // Seed
            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider
                    .GetRequiredService<TaskPulseDbContext>();

                var task = new TaskEntity(
                    "Teste",
                    new Sla(1),
                    null,
                    DateTimeOffset.UtcNow
                );

                context.Tasks.Add(task);

                var notification = new Notification(
                    task.Id,
                    "SLA expirado",
                    DateTimeOffset.UtcNow
                );

                context.Notifications.Add(notification);
                await context.SaveChangesAsync();

                notificationId = notification.Id;
            }

            // Act
            var response = await _client.PutAsync(
                $"/api/v1/notification/{notificationId}/read",
                null
            );

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));

            // Validate state change
            using var verifyScope = _factory.Services.CreateScope();
            var verifyContext = verifyScope.ServiceProvider
                .GetRequiredService<TaskPulseDbContext>();

            var updated = await verifyContext.Notifications
                .FindAsync(notificationId);

            Assert.True(updated!.IsRead);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _client.Dispose();
            _factory.Dispose();
        }
    }
}
