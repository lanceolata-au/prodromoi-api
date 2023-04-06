using Microsoft.AspNetCore.Mvc;
using Prodromoi.Core.Interfaces;
using Prodromoi.DomainModel.Model.Members;
using Prodromoi.Dto.Members;

namespace Prodromoi.Api.Controllers;

//[ApiController]
[Route("/members")]
public class MembersController : Controller
{
    private readonly IReadOnlyRepository _readOnlyRepository;
    private readonly IReadWriteRepository _readWriteRepository;

    public MembersController(
        IReadOnlyRepository readOnlyRepository, 
        IReadWriteRepository readWriteRepository)
    {
        _readOnlyRepository = readOnlyRepository;
        _readWriteRepository = readWriteRepository;
    }

    [HttpPost("new")]
    public ActionResult<MemberDto> Create([FromBody]MemberDto dto)
    {
        _readWriteRepository.Create<Member, int>(Member.Create(dto));
        return Ok(dto);
    }

}