using Prodromoi.Api;
using Prodromoi.Persistence.Features;
using Serilog;

Log.Logger = Logging.CreateLogger();

var builder = WebApplication
    .CreateBuilder(args);

builder.Host
    .UseSerilog()
    .ConfigureServices(CoreServiceManager.Configure)
    .ConfigureServices(ApiServices.Configure);

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(cfg =>
{
    cfg.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});

app.MapGet("/", () => "Hello World!");

app.Run();