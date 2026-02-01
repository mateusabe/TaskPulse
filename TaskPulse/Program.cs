using Microsoft.Extensions.Configuration;
using TaskPulse.Application;
using TaskPulse.Infrastructure;
using TaskPulse.Infrastructure.BackgroundServices;
using TaskPulse.Infrastructure.Observers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var allowedOrigins = builder.Configuration
    .GetSection("Cors:AllowedOrigins")
    .Get<string[]>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("DefaultCorsPolicy", policy =>
    {
        policy
            .WithOrigins(allowedOrigins)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .WithExposedHeaders("Content-Disposition");
    });
});

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

app.UseCors("DefaultCorsPolicy");

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

public partial class Program { }