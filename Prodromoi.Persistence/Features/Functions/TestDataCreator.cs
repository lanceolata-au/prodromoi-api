using Microsoft.EntityFrameworkCore;
using Prodromoi.Core.Interfaces;
using Prodromoi.DomainModel.Model;

namespace Prodromoi.Persistence.Features.Functions;

public class TestDataCreator : IDisposable
{
    private readonly IReadWriteRepository _readWriteRepository;

    public TestDataCreator(
        IReadWriteRepository readWriteRepository)
    {
        _readWriteRepository = readWriteRepository;
    }

    public void CreateTestUser()
    {
        var test = Actor.Create("test");
        _readWriteRepository.Create<Actor, int>(test);
    }

    public void UpdateTestUsers()
    {
        var users = _readWriteRepository
            .Table<Actor, int>()
            .Include(ae => ae.AuditEntries)
            .Where(a => a.Name.Equals("test"));

        foreach (var user in users)
        {
            user.ChangeName("Updated Name");
        }
    }
    
    public void Dispose()
    {
        
    }
}