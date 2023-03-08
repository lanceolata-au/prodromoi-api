using Microsoft.Extensions.Hosting;
using Prodromoi.Service;
using Prodromoi.Service.Features;
using Serilog;

Log.Logger = Logging.CreateLogger();

Log.Information("Starting Host");

Host.CreateDefaultBuilder(args)
    .UseSerilog()
    .ConfigureServices(ServiceManager.Configure)
    .Build()
    .Run();