using Serilog;

namespace Prodromoi.Service.Features;

public static class Logging
{
    public static ILogger CreateLogger()
    {
        return new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.Console()
            .CreateLogger();
    }
}