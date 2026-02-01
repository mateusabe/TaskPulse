using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskPulse.Application.Abstractions.Repositories;
using TaskPulse.Application.Abstractions.Storage;
using TaskPulse.Infrastructure;
using TaskPulse.Infrastructure.BackgroundServices;
using TaskPulse.Infrastructure.Data;
using TaskPulse.Infrastructure.Data.Repositories;
using TaskPulse.Tests.Fakes;

namespace TaskPulse.Tests.API
{
    public class TaskApiFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Test");

            builder.ConfigureServices((context, services) =>
            {
                // 🔥 Remove DbContext original
                services.RemoveAll<TaskPulseDbContext>();
                services.RemoveAll<DbContextOptions<TaskPulseDbContext>>();

                // 🔥 Remove SlaMonitorService (BackgroundService)
                var slaServices = services
                    .Where(s =>
                        s.ServiceType == typeof(IHostedService) &&
                        s.ImplementationType == typeof(SlaMonitorService))
                    .ToList();

                foreach (var service in slaServices)
                    services.Remove(service);

                // 🔥 Registra InMemory
                services.AddDbContext<TaskPulseDbContext>(options =>
                {
                    options.UseInMemoryDatabase("TestDb");
                });

                // 🔥 Infra (repos, handlers, etc)
                services.AddInfrastructure(context.Configuration, false);

                // 🔥 Substituições específicas de teste
                services.AddScoped<IFileStorage, FakeFileStorage>();
            });
        }
    }
}
