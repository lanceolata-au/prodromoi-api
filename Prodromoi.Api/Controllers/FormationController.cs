using Microsoft.AspNetCore.Mvc;
using Prodromoi.Core.Features;
using Prodromoi.Core.Interfaces;
using Prodromoi.DomainModel.Inclusions;
using Prodromoi.DomainModel.Model.Formations;
using Prodromoi.Dto.Formations;

namespace Prodromoi.Api.Controllers;

[ApiController]
[Route("/formation")]
public class FormationController : Controller
{

    private readonly IHashIdTranslator _hashIdTranslator;
    private readonly IReadOnlyRepository _readOnlyRepository;

    public FormationController(
        IHashIdTranslator hashIdTranslator, 
        IReadOnlyRepository readOnlyRepository)
    {
        _hashIdTranslator = hashIdTranslator;
        _readOnlyRepository = readOnlyRepository;
    }

    [HttpGet("{sectionId}/id")]
    public ActionResult<FormationSectionDto> GetById(string hashId)
    {
        var ids = _hashIdTranslator.Decode(hashId);
        var id = ids[0];

        var formationSection = _readOnlyRepository
            .Table<FormationSection, int>()
            .Where(fm => fm.Id == id)
            .BasicIncludes()
            .Single();

        return Ok(formationSection.MapDto());
    }

}