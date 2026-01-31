using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskPulse.Domain.Exceptions;

namespace TaskPulse.Domain.ValueObjects
{
    public class Sla
    {
        public int Hours { get; }

        public Sla(int hours)
        {
            if (hours <= 0)
                throw new DomainException("SLA deve ser maior que zero");

            Hours = hours;
        }

        public DateTimeOffset CalculateDueDate(DateTimeOffset createdAt)
            => createdAt.AddHours(Hours);
    }
}
