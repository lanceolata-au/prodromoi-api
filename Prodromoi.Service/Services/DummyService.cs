using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Prodromoi.Core.Interfaces;
using Prodromoi.DomainModel.Model;
using Prodromoi.Persistence.Features.Functions;

namespace Prodromoi.Service.Services;

public class DummyService : IHostedService
{

    private readonly IReadWriteRepository _readWriteRepository;
    private readonly TestDataCreator _testDataCreator;

    public DummyService( 
        TestDataCreator testDataCreator, IReadWriteRepository readWriteRepository)
    {
        _testDataCreator = testDataCreator;
        _readWriteRepository = readWriteRepository;
    }
    
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _testDataCreator.CreateTestUser();
        _testDataCreator.CreateTestUser();
        _testDataCreator.CreateTestUser();
        _testDataCreator.UpdateTestUsers();

        _readWriteRepository.Commit();
        
        var actors = 
            _readWriteRepository
            .Table<Actor, int>()
            .Include(a => a.AuditEntries)
            .ToList();

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}