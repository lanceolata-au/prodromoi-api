using Autofac;
using Prodromoi.Core.Features;
using Prodromoi.Core.Interfaces;

namespace Prodromoi.Persistence.Modules;

public class HelpersModule: Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
            .Register<HashIdTranslator>(cfg => 
                new HashIdTranslator("TODO CHANGE ME"))
            .As<IHashIdTranslator>()
            .SingleInstance();
    }
}