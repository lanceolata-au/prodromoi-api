using Autofac;
using Microsoft.EntityFrameworkCore;
using Prodromoi.Core.Features;
using Prodromoi.Core.Interfaces;
using Prodromoi.Persistence.Features.Data;

namespace Prodromoi.Persistence.Modules;

public class DatabaseStoreModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {

        builder
            .RegisterType<CoreContext>()
            .As<DbContext>()
            .InstancePerDependency();

        builder
            .RegisterType<ReadOnlyRepository>()
            .As<IReadOnlyRepository>()
            .InstancePerDependency();
        
        builder
            .RegisterType<ReadWriteRepository>()
            .As<IReadWriteRepository>()
            .InstancePerDependency();

    }
}