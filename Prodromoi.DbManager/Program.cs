using Microsoft.Extensions.Hosting;
using Prodromoi.DbManager;
using Prodromoi.DbManager.Features;
using Serilog;

LicenceInfo.OutputLicense();

Log.Logger = Logging.CreateLogger();

Host.CreateDefaultBuilder(args)
    .UseSerilog()
    .ConfigureServices(ServicesManager.Configure)
    .Build()
    .Run();

Console.WriteLine("Hello, World!");
