using Autofac;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Prodromoi.Api.Controllers;
using Prodromoi.Architecture.Tests;
using Prodromoi.Core.Features;
using Prodromoi.Core.Interfaces;
using Prodromoi.DomainModel.Inclusions;
using Prodromoi.DomainModel.Model.Formations;
using Prodromoi.Dto.Formations;

#pragma warning disable CS8618

namespace Prodromoi.Api.Tests.Api;

public class FormationTests : TestWithDi
{

    private FormationController _formationController;
    private IHashIdTranslator _hashIdTranslator;

    [SetUp]
    public void LocalSetup()
    {
        _hashIdTranslator = Container.Resolve<IHashIdTranslator>();
        
        _formationController 
            = new FormationController(
                _hashIdTranslator, 
                ReadOnlyRepository);
    }
    
    [Test]
    public void CanGetFormationByHashId()
    {
        FormationOperations.TestFormationAndSectionIsCreated();
        
        var formationSection = ReadOnlyRepository
            .Table<FormationSection, int>()
            .BasicIncludes()
            .Single();

        var hashId = _hashIdTranslator.Encode(formationSection.Id);

        var getResult = _formationController.GetById(hashId);

        getResult.Result.Should().BeOfType<OkObjectResult>();
        ((OkObjectResult) getResult.Result!).Value.Should().BeOfType<FormationSectionDto>();
        
        var dto = (FormationSectionDto)((OkObjectResult) getResult.Result!).Value!;
        
        dto.Should().NotBeNull();

        dto.SectionType.Should().Be(SectionType.Scouts);
        dto.Formation.Name.Should().Be("Test");

    }

}