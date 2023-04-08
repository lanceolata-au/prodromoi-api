using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Prodromoi.Persistence.Features;
using Prodromoi.Service;
using Serilog;

Log.Information("Starting Host");

Host.CreateDefaultBuilder(args)
    .UseSerilog()
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(CoreServiceManager.ConfigureContainer)
    .ConfigureContainer<ContainerBuilder>(ServiceServices.ConfigureContainer)
    .ConfigureServices(ServiceServices.Configure)
    .Build()
    .Run();