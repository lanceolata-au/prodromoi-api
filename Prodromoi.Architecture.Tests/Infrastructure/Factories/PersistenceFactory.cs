using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Prodromoi.Persistence.Modules;
using Serilog;

namespace Prodromoi.Architecture.Tests.Infrastructure.Factories;

public class PersistenceFactory
{
    public static IContainer CreateContainer()
    {
        var builder = new ContainerBuilder();

        builder
            .Register(del => {
                
                var optionsBuilder = new DbContextOptionsBuilder();
                optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
                optionsBuilder.UseLoggerFactory(new LoggerFactory().AddSerilog());
                optionsBuilder.UseSnakeCaseNamingConvention();
                return optionsBuilder.Options;
            })
            .As<DbContextOptions>()
            .SingleInstance();
        
        builder.RegisterModule<DatabaseStoreModule>();

        return builder.Build();

    }
}