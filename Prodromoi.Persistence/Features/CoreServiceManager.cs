using Autofac;
using Microsoft.Extensions.Hosting;
using Prodromoi.Persistence.Features.Functions;
using Prodromoi.Persistence.Modules;

namespace Prodromoi.Persistence.Features;

public static class CoreServiceManager
{
    public static void ConfigureContainer(
        HostBuilderContext hostContext,
        ContainerBuilder builder)
    {

        builder.RegisterModule<PostgreModule>();
        
        builder.RegisterModule<DatabaseStoreModule>();
        
        builder
            .RegisterType<TestDataCreator>()
            .InstancePerDependency();
        
    }
}