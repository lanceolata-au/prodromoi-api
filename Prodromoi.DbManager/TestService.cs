using Microsoft.Extensions.Hosting;
using Serilog;

namespace Prodromoi.DbManager;

public class TestService : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        var task = new Task(Action);
        
        task.Start();
        
        return Task.CompletedTask;
    }

    private async void Action()
    {
        await Task.Delay(20000);
        Log.Warning("Stopping with timeout");
        Environment.Exit(0);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}