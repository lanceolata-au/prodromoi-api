using Autofac;
using Prodromoi.Core.Interfaces;

namespace Prodromoi.Architecture.Tests.Infrastructure.Operations;

public class Operation
{
    protected IReadOnlyRepository _readOnlyRepository;
    protected IReadWriteRepository _readWriteRepository;

    public Operation(IContainer container)
    {
        _readOnlyRepository = container.Resolve<IReadOnlyRepository>();
        _readWriteRepository = container.Resolve<IReadWriteRepository>();
    }
}