using FluentAssertions;
using Prodromoi.Api.Controllers;
using Prodromoi.Architecture.Tests;
using Prodromoi.DomainModel.Model.Attendance;
using Prodromoi.DomainModel.Model.Members;
using Prodromoi.Dto.Attendance;
using Prodromoi.Dto.Members;

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
                .Table<Member, int>();
        var sectionAttendances
            = _readWriteRepository
                .Table<SectionRecordedAttendance, int>();
        
        members.Count().Should().Be(1);
        sectionAttendances.Count().Should().Be(1);
        
        var member = members.First();
        member.Name.Should().Be(_attendanceDto.RecordingAdult.Name);
        member.PhoneNumber.Should().Be(_attendanceDto.RecordingAdult.PhoneNumber);


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
                .Table<Member, int>();
        var sectionAttendances
            = _readWriteRepository
                .Table<SectionRecordedAttendance, int>();
        
        members.Count().Should().Be(1);
        var member = members.First();
        member.Name.Should().Be(_attendanceDto.RecordingAdult.Name);
        member.PhoneNumber.Should().Be(_attendanceDto.RecordingAdult.PhoneNumber);
        
        sectionAttendances.Count().Should().Be(2);
        
    }
    
}