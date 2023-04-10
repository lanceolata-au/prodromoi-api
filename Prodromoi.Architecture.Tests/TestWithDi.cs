using Autofac;
using Prodromoi.Architecture.Tests.Infrastructure.Factories;
using Prodromoi.Architecture.Tests.Infrastructure.Operations;
using Prodromoi.Core.Interfaces;

#pragma warning disable CS8618

namespace Prodromoi.Architecture.Tests;

public class TestWithDi
{

    protected IContainer Container;

    protected IReadOnlyRepository ReadOnlyRepository;
    protected IReadWriteRepository ReadWriteRepository;
    protected IHashIdTranslator HashIdTranslator;

    protected MemberOperation MemberOperation;
    protected FormationOperations FormationOperations;

    [SetUp]
    public void Setup()
    {
        Container = DiFactory.CreateContainer();
        ReadOnlyRepository = Container.Resolve<IReadOnlyRepository>();
        ReadWriteRepository = Container.Resolve<IReadWriteRepository>();
        HashIdTranslator = Container.Resolve<IHashIdTranslator>();

        MemberOperation = new MemberOperation(Container);
        FormationOperations = new FormationOperations(Container);

    }
}