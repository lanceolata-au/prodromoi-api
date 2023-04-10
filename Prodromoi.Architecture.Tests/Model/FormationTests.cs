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
        
        ReadWriteRepository.Create<Formation, int>(formation);
        ReadWriteRepository.Commit();

        var result = ReadOnlyRepository.Table<Formation, int>();

        result.Count().Should().Be(1);
        result.First().Name.Should().Be(formation.Name);

    }
    
    [Test]
    public void CanCreateWithIncludes()
    {
        var formation = Formation.Create("Test");
        
        ReadWriteRepository.Create<Formation, int>(formation);
        ReadWriteRepository.Commit();

        var section = FormationSection.Create(formation.Id, SectionType.Scouts);
        section.SetMeetingDay(DayOfWeek.Thursday);
        section.SetMeetingTime(TimeOnly.Parse("19:00"));
        section.SetFriendlyCode("TESTCODE");
        ReadWriteRepository.Create<FormationSection, int>(section);
        ReadWriteRepository.Commit();

        var result = ReadOnlyRepository
            .Table<Formation, int>()
            .FullIncludes();

        result.Count().Should().Be(1);
        result.First().Name.Should().Be(formation.Name);

        result.First().AuditEntries.Should().NotBeNull();

        result.First().Sections.Count.Should().Be(1);
        result.First().Sections.First().SectionType.Should().Be(SectionType.Scouts);
        result.First().Sections.First().RegularMeetingDay.Should().Be(DayOfWeek.Thursday);
        var testTime = new TimeOnly(19, 0);
        result.First().Sections.First().RegularMeetingTime.Should().Be(testTime);
        result.First().Sections.First().FriendlyCode.Should().NotBeNull();
        result.First().Sections.First().FriendlyCode.Should().Be("testcode");
    }
    
}