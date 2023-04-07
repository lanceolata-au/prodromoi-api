using Autofac;
using Prodromoi.Architecture.Tests.Infrastructure.Factories;
using Prodromoi.Architecture.Tests.Infrastructure.Operations;
using Prodromoi.Core.Interfaces;

namespace Prodromoi.Architecture.Tests;

public class TestWithDi
{
#pragma warning disable CS8618
    
    private IContainer _container;

    public IReadOnlyRepository _readOnlyRepository;
    public IReadWriteRepository _readWriteRepository;

    internal MemberOperation _memberOperation;
    
#pragma warning restore CS8618
    
    [SetUp]
    public void Setup()
    {
        _container = PersistenceFactory.CreateContainer();
        _readOnlyRepository = _container.Resolve<IReadOnlyRepository>();
        _readWriteRepository = _container.Resolve<IReadWriteRepository>();

        _memberOperation = new MemberOperation(_container);

    }
}