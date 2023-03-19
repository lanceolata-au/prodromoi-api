using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Prodromoi.Core.Features;
using Prodromoi.Core.Interfaces;
using Prodromoi.Persistence.Features.Data;
using Prodromoi.Persistence.Features.Functions;
using Serilog;

namespace Prodromoi.Persistence.Features;

public static class CoreServiceManager
{
    public static void Configure(HostBuilderContext context, IServiceCollection services)
    {
        services.ConfigureDependencies();
    }


    private static void ConfigureDependencies(this IServiceCollection services)
    {

        var optionsBuilder = new DbContextOptionsBuilder();
        optionsBuilder.UseNpgsql(GenerateConnectionString());
        optionsBuilder.UseLoggerFactory(new LoggerFactory().AddSerilog());
        optionsBuilder.UseSnakeCaseNamingConvention();
        //optionsBuilder.EnableDetailedErrors();
        optionsBuilder.EnableSensitiveDataLogging();
        var options = new MemoryCacheOptions
        {
            ExpirationScanFrequency = TimeSpan.FromMilliseconds(100)
        };
        var cache = new MemoryCache(options);
        optionsBuilder.UseMemoryCache(cache);
        
        services.AddSingleton(optionsBuilder.Options);
        services.AddTransient<DbContext, CoreContext>();

        services.AddTransient<IReadOnlyRepository, ReadOnlyRepository>();
        services.AddTransient<IReadWriteRepository, ReadWriteRepository>();

        services.AddTransient<TestDataCreator>();

    }
    
    private static string GenerateConnectionString()
    {
        var dbHost 
            = Environment.GetEnvironmentVariable("DB_HOST");
        var dbPort 
            = Environment.GetEnvironmentVariable("DB_PORT");
        var dbUser 
            = Environment.GetEnvironmentVariable("DB_USER");
        var dbPass 
            = Environment.GetEnvironmentVariable("DB_PASS");
        var dbDatabase 
            = Environment.GetEnvironmentVariable("DB_DATABASE");
        
        return $"Host={dbHost}:{dbPort};"
               +$"Username={dbUser};"
               +$"Password={dbPass};"
               +$"Database={dbDatabase};";
    }
    
}