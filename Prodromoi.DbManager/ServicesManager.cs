using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Prodromoi.DbManager;

public class ServicesManager
{
    public static void Configure(HostBuilderContext context, IServiceCollection services)
    {
        services.AddHostedService<TestService>();
    }

}