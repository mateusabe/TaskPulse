using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskPulse.Domain.Entities;
using TaskPulse.Domain.ValueObjects;

namespace TaskPulse.Tests.Domain
{
    public class TaskEntityTests
    {
        [Test]
        public void Should_Create_Task_With_Valid_Data()
        {
            var sla = new Sla(2);
            var now = new DateTimeOffset(2026, 1, 1, 13, 57, 0, TimeSpan.Zero);
            var title = "Tarefa teste";

            var task = new TaskEntity(
                title,
                sla,
                "file.txt",
                now
            );

            Assert.That(title, Is.EqualTo(task.Title));
            Assert.False(task.IsCompleted);
            Assert.That(now.AddHours(2), Is.EqualTo(task.DueAt));
        }

        [Test]
        public void Should_Complete_Task()
        {
            var sla = new Sla(1);
            var now = new DateTimeOffset(2026, 1, 1, 8, 30, 0, TimeSpan.Zero);

            var task = new TaskEntity(
                "Finalizar",
                sla,
                "file.txt",
                now
            );

            task.Complete();

            Assert.True(task.IsCompleted);
            Assert.That(task.CompletedAt, Is.Not.Null);
            Assert.That(task.CompletedAt, Is.GreaterThan(now));
        }

        [Test]
        public void Should_Expire_Sla()
        {
            var sla = new Sla(1);
            var now = new DateTimeOffset(2026, 1, 1, 10, 0, 0, TimeSpan.Zero);

            var task = new TaskEntity(
                "SLA",
                sla,
                "file.txt",
                now
            );

            var expired = task.IsSlaExpired(now.AddHours(2));

            Assert.True(expired);
        }
    }
}
