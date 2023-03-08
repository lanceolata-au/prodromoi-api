using Microsoft.Extensions.Hosting;
using Prodromoi.DbManager.Features;
using Serilog;

namespace Prodromoi.DbManager.Services;

public class UpgradeService : IHostedService
{
    private StateManager _stateManager;
    
    public UpgradeService(StateManager stateManager)
    {
        _stateManager = stateManager;
    }
    
    public Task StartAsync(CancellationToken cancellationToken)
    {
        var task = new Task(Action);
        
        task.Start();
        
        return Task.CompletedTask;
    }

    private async void Action()
    {
        _stateManager.ResolveDatabaseState();
        
        _stateManager.ResolveSchemaState();
        
        Environment.Exit(0);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}