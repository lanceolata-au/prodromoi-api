using Microsoft.Extensions.Hosting;
using Prodromoi.Core.Interfaces;
using Prodromoi.DomainModel.Model;
using Prodromoi.Service.Extensions;

namespace Prodromoi.Service.Services;

public class DummyService : IHostedService
{

    private readonly IAuditRepository _auditedRepository;

    public DummyService(IAuditRepository auditedRepository)
    {
        _auditedRepository = auditedRepository;
    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var test = Actor.Create("test");
        
        _auditedRepository.Create<Actor, int>(test);

        _auditedRepository.Commit();
        
        var actors = 
            _auditedRepository
            .Table<Actor, int>()
            .ToList();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}