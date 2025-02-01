using System.Reflection;
using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
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

builder.Services
    .AddAuthenticationJwtBearer(s => s.SigningKey = builder.Configuration.GetValue<string>("Auth:JWTSecret"))
    .AddAuthorization()
    .SwaggerDocument();

//Mediator Assemblies
List<Assembly> mediatorAssemblies = new List<Assembly>();
builder.Services.AddUserModule(builder.Configuration, mediatorAssemblies, logger);

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseFastEndpoints().UseSwaggerGen();

app.Run();
