using Prodromoi.Persistence.Features;
using Serilog;

var builder = WebApplication
    .CreateBuilder(args);

builder.Host
    .UseSerilog()
    .ConfigureServices(CoreServiceManager.Configure);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();