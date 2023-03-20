using Microsoft.OpenApi.Models;

namespace Prodromoi.Api;

public static class ApiServices
{
    public static void Configure(HostBuilderContext context, IServiceCollection services)
    {
        services.AddSwaggerGen(cnf =>
        {
            cnf.SwaggerDoc("v1", new OpenApiInfo {Title = "My API", Version = "v1"});
        });

        services.AddControllers();
    }
}