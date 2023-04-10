using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Prodromoi.Core.Extensions;
using Prodromoi.Core.Interfaces;
using Prodromoi.DomainModel.Inclusions;
using Prodromoi.DomainModel.Model.Attendance;
using Prodromoi.DomainModel.Model.Formations;
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
    private readonly IHashIdTranslator _hashIdTranslator;

    public AttendanceController(
        IReadOnlyRepository readOnlyRepository, 
        IReadWriteRepository readWriteRepository, 
        IHashIdTranslator hashIdTranslator)
    {
        _readOnlyRepository = readOnlyRepository;
        _readWriteRepository = readWriteRepository;
        _hashIdTranslator = hashIdTranslator;
    }

    [HttpPost("{formationSectionHashId}/new")]
    public ActionResult<QuickAttendanceDto> Create(
        [FromBody]QuickAttendanceDto dto, 
        string formationSectionHashId)
    {
        var formationSectionIds = _hashIdTranslator.Decode(formationSectionHashId);
        if (formationSectionIds.Length == 0) return NotFound(dto);
        var formationSectionId = formationSectionIds[0];
        var formationSectionExists
            = _readOnlyRepository.Table<FormationSection, int>()
                .Any(fs => fs.Id == formationSectionId);

        if (!formationSectionExists) return NotFound(dto);
        
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
        
        var sectionRecordedAttendance = SectionRecordedAttendance.Create(recordingAdult, (int)formationSectionId);
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