using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskPulse.Application.Abstractions.Storage;
using TaskPulse.Application.Models;

namespace TaskPulse.Tests.Fakes
{
    public class FakeFileStorage : IFileStorage
    {
        public Task<string> SaveAsync(
            FileUpload file,
            CancellationToken cancellationToken)
        {
            // Simula salvamento sem IO real
            return Task.FromResult("fake/path/file.txt");
        }
    }
}
