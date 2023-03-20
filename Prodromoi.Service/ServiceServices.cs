using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Prodromoi.Service.Services;

namespace Prodromoi.Service;

public static class ServiceServices
{
    public static void Configure(HostBuilderContext context, IServiceCollection services)
    {
        services.AddHostedService<DummyService>();
    }
}