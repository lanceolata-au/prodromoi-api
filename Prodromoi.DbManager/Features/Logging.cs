using Serilog;

namespace Prodromoi.DbManager.Features;

public class Logging
{
    public static ILogger CreateLogger()
    {
        return new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.Console()
            .CreateLogger();
    }
}