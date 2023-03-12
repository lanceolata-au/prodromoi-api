using Microsoft.Extensions.Hosting;
using Prodromoi.Core.Interfaces;
using Prodromoi.DomainModel.Model.Events;

namespace Prodromoi.Service.Services;

public class DummyService : IHostedService
{

    private readonly IAuditRepository _auditedRepository;

    public DummyService(IAuditRepository auditedRepository)
    {
        _auditedRepository = auditedRepository;
    }
    
    public Task StartAsync(CancellationToken cancellationToken)
    {
        var someEvent = Event.Create("test", new DateOnly(2022, 12, 12));
        _auditedRepository.Create<Event, int>(someEvent);
        Task.Delay(200, cancellationToken);
        _auditedRepository.Commit();
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}