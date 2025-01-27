using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

List<Assembly> mediatorAssemblies = new List<Assembly>();


var app = builder.Build();


app.Run();


