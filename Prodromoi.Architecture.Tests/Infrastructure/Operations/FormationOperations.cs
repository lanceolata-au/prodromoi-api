using Autofac;
using Prodromoi.DomainModel.Model.Formations;
using Prodromoi.Dto.Formations;

namespace Prodromoi.Architecture.Tests.Infrastructure.Operations;

public class FormationOperations : Operation
{
    public FormationOperations(IContainer container) : base(container)
    {
    }

    public void TestFormationAndSectionIsCreated()
    {
        var formation = Formation.Create("Test");
        _readWriteRepository.Create<Formation, int>(formation);
        _readWriteRepository.Commit();

        var section = FormationSection.Create(formation.Id, SectionType.Scouts);
        _readWriteRepository.Create<FormationSection, int>(section);
        _readWriteRepository.Commit();

    }
    
}