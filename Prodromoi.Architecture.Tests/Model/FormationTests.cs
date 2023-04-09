using FluentAssertions;
using Prodromoi.DomainModel.Inclusions;
using Prodromoi.DomainModel.Model.Formations;
using Prodromoi.Dto.Formations;

namespace Prodromoi.Architecture.Tests.Model;

public class FormationTests : TestWithDi
{
 
    [SetUp]
    public void LocalSetup()
    {
        
    }

    [Test]
    public void CanCreateAndFetch()
    {
        var formation = Formation.Create("Test");
        
        _readWriteRepository.Create<Formation, int>(formation);
        _readWriteRepository.Commit();

        var result = _readOnlyRepository.Table<Formation, int>();

        result.Count().Should().Be(1);
        result.First().Name.Should().Be(formation.Name);

    }
    
    [Test]
    public void CanCreateWithIncludes()
    {
        var formation = Formation.Create("Test");
        
        _readWriteRepository.Create<Formation, int>(formation);
        _readWriteRepository.Commit();

        var section = FormationSection.Create(formation.Id, SectionType.Scouts);
        section.SetMeetingDay(DayOfWeek.Thursday);
        _readWriteRepository.Create<FormationSection, int>(section);
        _readWriteRepository.Commit();

        var result = _readOnlyRepository
            .Table<Formation, int>()
            .FullIncludes();

        result.Count().Should().Be(1);
        result.First().Name.Should().Be(formation.Name);

        result.First().AuditEntries.Should().NotBeNull();

        result.First().Sections.Count.Should().Be(1);
        result.First().Sections.First().SectionType.Should().Be(SectionType.Scouts);
        result.First().Sections.First().RegularMeetingDay.Should().Be(DayOfWeek.Thursday);

    }
    
}