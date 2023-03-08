using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Prodromoi.Service;

public static class ServiceManager
{
    public static void Configure(HostBuilderContext context, IServiceCollection services)
    {
        services.ConfigureDependencies();
    }


    private static void ConfigureDependencies(this IServiceCollection services)
    {
        var optionsBuilder = new DbContextOptionsBuilder();
        optionsBuilder.UseNpgsql(GenerateConnectionString());
        optionsBuilder.UseSnakeCaseNamingConvention();
        var options = new MemoryCacheOptions
        {
            ExpirationScanFrequency = TimeSpan.FromMilliseconds(100)
        };
        
        var cache = new MemoryCache(options);
        
        optionsBuilder.UseMemoryCache(cache);
        services.AddSingleton<DbContextOptions>(optionsBuilder.Options);
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