using Microsoft.Extensions.Configuration;
using TaskPulse.Application;
using TaskPulse.Infrastructure;
using TaskPulse.Infrastructure.BackgroundServices;
using TaskPulse.Infrastructure.Observers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();

if (!builder.Environment.IsEnvironment("Test"))
{
    builder.Services.AddInfrastructure(
        builder.Configuration,
        enableBackgroundServices: true);
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

public partial class Program { }