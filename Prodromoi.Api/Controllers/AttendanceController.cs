using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Prodromoi.Core.Extensions;
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
    public ActionResult<QuickAttendanceDto> Create([FromBody]QuickAttendanceDto dto)
    {
        var adultSearchResult 
            = _readOnlyRepository
            .Table<Member, int>()
            .Where(m =>
                m.PhoneNumber != null &&
                m.Name.Equals(dto.RecordingAdult.Name) &&
                m.PhoneNumber.Equals(dto.RecordingAdult.PhoneNumber.PhoneNumberString()));

        Member recordingAdult;
        
        if (adultSearchResult.Count() == 1)
        {
            recordingAdult = adultSearchResult.Single();
        }
        else
        {
            dto.RecordingAdult.MemberType = MemberType.AdultUnknown;
            recordingAdult = Member.Create(dto.RecordingAdult);
            _readWriteRepository.Create<Member, int>(recordingAdult);
            _readWriteRepository.Commit();
        }
        
        var sectionRecordedAttendance = SectionRecordedAttendance.Create(recordingAdult);
        sectionRecordedAttendance.Audit($"{recordingAdult.Name}", "Created from API");
        _readWriteRepository.Create<SectionRecordedAttendance, int>(sectionRecordedAttendance);
        _readWriteRepository.Commit();

        var recordingAdultRecordedAttendance = RecordedAttendance
            .Create(sectionRecordedAttendance, recordingAdult);
        _readWriteRepository.Create<RecordedAttendance, short>(recordingAdultRecordedAttendance);

        foreach (var memberAttendanceDto in dto.Attendances)
        {
            if (!memberAttendanceDto.Present) continue;
            
            var youthSearchResult
                = _readOnlyRepository
                    .Table<Member, int>()
                    .Where(m => 
                        m.PhoneNumber == string.Empty &&
                        m.Name.Equals(memberAttendanceDto.Member.Name));

            Member recordedYouth;
            
            if (youthSearchResult.Count() == 1)
            {
                recordedYouth = youthSearchResult.Single();
            } 
            else
            {
                var youthDto = memberAttendanceDto.Member;
                youthDto.MemberType = MemberType.YouthUnknown;
                recordedYouth = Member.Create(youthDto);
                _readWriteRepository.Create<Member, int>(recordedYouth);
                _readWriteRepository.Commit();
            }

            var youthRecordedAttendance = RecordedAttendance
                .Create(sectionRecordedAttendance, recordedYouth);
            
            _readWriteRepository.Create<RecordedAttendance, short>(youthRecordedAttendance);

        }
        
        
        return Ok(dto);
    }

}