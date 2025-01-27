using System.Reflection;
using MyTrade.Users.Extensions;
using Serilog;

var logger = Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

logger.Information("Starting MyTrade Web Host");

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, config) =>
{
    config.ReadFrom.Configuration(builder.Configuration);
});

List<Assembly> mediatorAssemblies = new List<Assembly>();

//Services
builder.Services.AddUserModule(builder.Configuration, mediatorAssemblies, logger);

// Add Swagger services
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

var app = builder.Build();

// Configure Swagger middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
