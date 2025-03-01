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

var appName = "MyTrade";

logger.Information("Starting {App} Web Host", appName);

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, config) =>
{
    config.ReadFrom.Configuration(builder.Configuration);
});

builder.Services
    .AddAuthenticationJwtBearer(s => s.SigningKey = builder.Configuration.GetValue<string>("Auth:JWTSecret"))
    .AddAuthorization();

builder.Services.AddFastEndpoints().AddSwaggerDocument();

//Mediator Assemblies
List<Assembly> mediatorAssemblies = new List<Assembly>();
builder.Services.AddUserModule(builder.Configuration, mediatorAssemblies, logger);

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseFastEndpoints().UseSwaggerGen();

app.Run();
