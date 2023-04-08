using Autofac;
using Autofac.Extensions.DependencyInjection;
using Prodromoi.Api;
using Prodromoi.Persistence.Features;
using Serilog;

Log.Logger = Logging.CreateLogger();

var builder = WebApplication
    .CreateBuilder(args);

builder.Host
    .UseSerilog()
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(CoreServiceManager.ConfigureContainer)
    .ConfigureContainer<ContainerBuilder>(ApiServices.ConfigureContainer)
    .ConfigureServices(ApiServices.Configure);

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(cfg =>
{
    cfg.SwaggerEndpoint("/swagger/v1/swagger.json", "Prodromoi V1");
});

app.MapGet("/", () => "OK");
app.UseCors("CorsPolicy");
app.MapControllers();

app.Run();