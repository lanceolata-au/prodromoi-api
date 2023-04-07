using FluentAssertions;
using Prodromoi.Api.Controllers;
using Prodromoi.Architecture.Tests;
using Prodromoi.Core.Extensions;
using Prodromoi.DomainModel.Inclusions;
using Prodromoi.DomainModel.Model.Attendance;
using Prodromoi.DomainModel.Model.Members;
using Prodromoi.Dto.Attendance;
using Prodromoi.Dto.Members;
using Prodromoi.Persistence.Extensions;

namespace Prodromoi.Api.Tests.Api;

[TestFixture]
public class AttendanceTests : TestWithDi
{
    private readonly QuickAttendanceDto _attendanceDto = new QuickAttendanceDto();
    private AttendanceController _attendanceController;
    
    [SetUp]
    public void LocalSetup()
    {
        _attendanceController 
            = new AttendanceController(
            _readOnlyRepository, 
            _readWriteRepository);
        
        _attendanceDto.RecordingAdult = new MemberDto()
        {
            Name = "Test Adult",
            PhoneNumber = "04 12 345 678"
        };
    }

    [Test]
    public void CanCreateAttendanceWithJustAdult()
    {
        _attendanceController.Create(_attendanceDto);

        //This would be a part of the controller lifecycle, however we aren't mocking that here.
        _readWriteRepository.Dispose();

        var members
            = _readOnlyRepository
                .Table<Member, int>()
                .IncludeAudits();
        var sectionAttendances
            = _readOnlyRepository
                .Table<SectionRecordedAttendance, int>()
                .IncludeAudits();
        var recordedAttendances
            = _readOnlyRepository
                .Table<RecordedAttendance, short>()
                .FullIncludes();
        
        members.Count().Should().Be(1);
        sectionAttendances.Count().Should().Be(1);
        recordedAttendances.Count().Should().Be(1);
        
        var member = members.First();
        member.Name.Should().Be(_attendanceDto.RecordingAdult.Name);
        member.PhoneNumber.Should().Be(_attendanceDto.RecordingAdult.PhoneNumber.PhoneNumberString());
        member.MemberType.Should().Be(MemberType.AdultUnknown);
        
        foreach (var attendance in recordedAttendances)
        {
            attendance
                .Member!
                .Name
                .Should()
                .Be(_attendanceDto.RecordingAdult.Name);
            attendance
                .Member!
                .PhoneNumber
                .Should()
                .Be(_attendanceDto.RecordingAdult.PhoneNumber.PhoneNumberString());
        }
        
    }
    
    [Test]
    public void CanCreateSecondAttendanceWithJustAdult()
    {
        _attendanceController.Create(_attendanceDto);
        //This would be a part of the controller lifecycle, however we aren't mocking that here.
        _readWriteRepository.Dispose();

        _attendanceController.Create(_attendanceDto);
        //This would be a part of the controller lifecycle, however we aren't mocking that here.
        _readWriteRepository.Dispose();

        var members
            = _readOnlyRepository
                .Table<Member, int>()
                .IncludeAudits();
        
        var sectionAttendances
            = _readOnlyRepository
                .Table<SectionRecordedAttendance, int>()
                .IncludeAudits();
        
        var recordedAttendances
            = _readOnlyRepository
                .Table<RecordedAttendance, short>()
                .FullIncludes();
        
        members.Count().Should().Be(1);
        sectionAttendances.Count().Should().Be(2);;
        recordedAttendances.Count().Should().Be(2);
        
        var member = members.First();
        member.Name.Should().Be(_attendanceDto.RecordingAdult.Name);
        member.PhoneNumber.Should().Be(_attendanceDto.RecordingAdult.PhoneNumber.PhoneNumberString());
        member.MemberType.Should().Be(MemberType.AdultUnknown);

        foreach (var attendance in recordedAttendances)
        {
            attendance
                .Member!
                .Name
                .Should()
                .Be(_attendanceDto.RecordingAdult.Name);
            attendance
                .Member!
                .PhoneNumber
                .Should()
                .Be(_attendanceDto.RecordingAdult.PhoneNumber.PhoneNumberString());
        }
        
    }
    
}