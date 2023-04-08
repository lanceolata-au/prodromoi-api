using Autofac;
using Microsoft.OpenApi.Models;

namespace Prodromoi.Api;

public static class ApiServices
{
    
    internal static void ConfigureContainer(
        HostBuilderContext hostContext,
        ContainerBuilder containerBuilder)
    {
        
    }
    
    public static void Configure(
        HostBuilderContext context, 
        IServiceCollection services)
    {
        services.AddControllers();
        services.AddCors(options =>
        {
            options.AddPolicy(name: "CorsPolicy",
                policy =>
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowAnyOrigin();
                });
        });
        services.AddSwaggerGen(cnf =>
        {
            cnf.SwaggerDoc("v1", new OpenApiInfo {Title = "Prodromoi", Version = "v1"});
        });

    }
}