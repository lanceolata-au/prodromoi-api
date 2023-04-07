using Microsoft.AspNetCore.Mvc;
using Prodromoi.Core.Interfaces;
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
        
        
        return Ok(dto);
    }

}