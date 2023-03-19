using Microsoft.Extensions.Hosting;
using Prodromoi.Persistence.Features;
using Prodromoi.Service;
using Serilog;

Log.Logger = Logging.CreateLogger();

Log.Information("Starting Host");

Host.CreateDefaultBuilder(args)
    .UseSerilog()
    .ConfigureServices(CoreServiceManager.Configure)
    .ConfigureServices(ServiceServices.Configure)
    .Build()
    .Run();