using NetCoreHexagonal.Application.Services;
using NetCoreHexagonal.EventsDispatching;
using NetCoreHexagonal.Persistence;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build()
    .GetConnectionString("DefaultConnection");

builder.Services
    .ConfigureApplicationServices()
    .ConfigurePersistenceServices(connectionString, true)
    .ConfigureEventsDispatchingServices();

builder.Services
    .AddControllers();

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();

var app = builder.Build();

app.UseSwagger().UseSwaggerUI();

app.MapControllers();

app.Run();
