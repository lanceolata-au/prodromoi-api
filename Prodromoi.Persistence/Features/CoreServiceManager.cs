using Autofac;
using Microsoft.Extensions.Hosting;
using Prodromoi.Persistence.Modules;
using Serilog;

namespace Prodromoi.Persistence.Features;

public static class CoreServiceManager
{
    public static void ConfigureContainer(
        HostBuilderContext hostContext,
        ContainerBuilder builder)
    {
        
        builder.RegisterModule<HelpersModule>();
        
        builder.RegisterModule<PostgreModule>();
        
        builder.RegisterModule<DatabaseStoreModule>();
        
    }
}