using Autofac;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Prodromoi.Scheduler;

public static class ServiceServices
{
    internal static void Configure(HostBuilderContext context, IServiceCollection services)
    {
    }

    internal static void ConfigureContainer(
        HostBuilderContext context,
        ContainerBuilder container)
    {        
        Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(context.Configuration)
        .CreateLogger();
    }
    
}