using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Prodromoi.DbManager.Features;
using Prodromoi.DbManager.Features.Sql;
using Prodromoi.DbManager.Services;

namespace Prodromoi.DbManager;

public static class ServicesManager
{
    public static void Configure(HostBuilderContext context, IServiceCollection services)
    {
        services.RegisterDependencies();
        services.AddHostedService<UpgradeService>();
    }


    private static void RegisterDependencies(this IServiceCollection services)
    {
        services.AddSingleton<SqlConnection>();
        services.AddSingleton<SqlFunctions>();
        services.AddSingleton<StateManager>();
    }

}