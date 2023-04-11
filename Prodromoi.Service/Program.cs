using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Prodromoi.Core.Features;
using Prodromoi.Persistence.Features;
using Prodromoi.Service;
using Serilog;

LicenceInfo.OutputLicense();

Log.Information("Starting Host");

Host.CreateDefaultBuilder(args)
    .UseSerilog()
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(CoreServiceManager.ConfigureContainer)
    .ConfigureContainer<ContainerBuilder>(ServiceServices.ConfigureContainer)
    .ConfigureServices(ServiceServices.Configure)
    .Build()
    .Run();