using Microsoft.AspNetCore.Mvc;
using Prodromoi.Core.Interfaces;
using Prodromoi.DomainModel.Model.Attendance;
using Prodromoi.DomainModel.Model.Members;
using Prodromoi.Dto.Attendance;
using Prodromoi.Dto.Members;

namespace Prodromoi.Api.Controllers;

[ApiController]
[Route("/attendance")]
public class AttendanceController : Controller
{
    private readonly IReadOnlyRepository _readOnlyRepository;
    private readonly IReadWriteRepository _readWriteRepository;

    public AttendanceController(
        IReadOnlyRepository readOnlyRepository, 
        IReadWriteRepository readWriteRepository)
    {
        _readOnlyRepository = readOnlyRepository;
        _readWriteRepository = readWriteRepository;
    }

    [HttpPost("new")]
    public ActionResult<MemberDto> Create([FromBody]QuickAttendanceDto dto)
    {
        var searchResult = _readWriteRepository
            .Table<Member, int>()
            .Where(m
                => m.PhoneNumber != null &&
                   m.Name.Equals(dto.RecordingAdult.Name) &&
                   m.PhoneNumber.Equals(dto.RecordingAdult.PhoneNumber));

        Member recordingAdult;
        
        if (searchResult.Count() == 1)
        {
            recordingAdult = searchResult.Single();
        }
        else
        {
            recordingAdult = Member.Create(dto.RecordingAdult);
            _readWriteRepository.Create<Member, int>(recordingAdult);
        }
        
        var sectionRecordedAttendance = SectionRecordedAttendance.Create(recordingAdult);
        
        _readWriteRepository.Create<SectionRecordedAttendance, int>(sectionRecordedAttendance);
        
        return Ok(dto);
    }

}