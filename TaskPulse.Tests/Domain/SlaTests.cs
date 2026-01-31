using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskPulse.Domain.Exceptions;
using TaskPulse.Domain.ValueObjects;

namespace TaskPulse.Tests.Domain
{
    public class SlaTests
    {
        [Test]
        public void Should_Calculate_DueDate_Correctly()
        {
            // Arrange
            var sla = new Sla(4);
            var now = new DateTimeOffset(
                2026, 1, 1, 
                8, 0, 0,
                TimeSpan.Zero);

            // Act
            var dueAt = sla.CalculateDueDate(now);

            // Assert
            Assert.That(
                dueAt,
                Is.EqualTo(new DateTimeOffset(
                2026, 1, 1,
                12, 0, 0,
                TimeSpan.Zero))
            );
        }

        [Test]
        public void Should_Throw_When_Sla_Is_Invalid()
        {
            Assert.Throws<DomainException>(() => new Sla(0));
        }
    }
}
