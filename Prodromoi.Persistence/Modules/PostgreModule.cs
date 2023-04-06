using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Prodromoi.Persistence.Modules;

public class PostgreModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
            .Register(del => {
                
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
                return optionsBuilder.Options;
                
            })
            .As<DbContextOptions>()
            .SingleInstance();
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